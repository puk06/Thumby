using Thumby.Core.Attributes;
using Thumby.Core.Models;

namespace Thumby.WinForm.Services;

internal static class UIBuilder
{
    private static readonly Font TitleFont = new Font("Yu Gothic UI", 11F, FontStyle.Bold);

    internal static void BuildUI(Control parent, object target, Action[] OnPropertyChanged)
    {
        parent.Controls.Clear();

        int y = 5;
        var type = target.GetType();

        foreach (var prop in type.GetProperties())
        {
            if (prop.GetCustomAttributes(typeof(UIFieldAttribute), false).FirstOrDefault() is not UIFieldAttribute attr)
                continue;

            if (prop.GetCustomAttributes(typeof(SpaceAttribute), false).FirstOrDefault() is SpaceAttribute spaceAttr)
                y += spaceAttr.Space;

            if (prop.GetCustomAttributes(typeof(TitleAttribute), false).FirstOrDefault() is TitleAttribute titleAttr)
            {
                Label titleLabel = new()
                {
                    Font = TitleFont,
                    Text = titleAttr.Title,
                    Location = new Point(10, y),
                    AutoSize = true
                };
                parent.Controls.Add(titleLabel);

                y += TitleFont.Height + 5;

                Label titleLineLabel = new()
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(10, y),
                    Size = new Size(parent.Width - 30, 1)
                };
                parent.Controls.Add(titleLineLabel);

                y += 10;
            }

            Label label = new()
            {
                Text = attr.Label != "" ? attr.Label : prop.Name,
                Location = new Point(10, y + 3),
                AutoSize = true
            };
            parent.Controls.Add(label);

            Control input;

            if (prop.PropertyType == typeof(bool))
            {
                input = new CheckBox()
                {
                    Checked = (bool)prop.GetValue(target)!,
                    Location = new Point(150, y)
                };

                ((CheckBox)input).CheckedChanged += (s, e) =>
                {
                    prop.SetValue(target, ((CheckBox)input).Checked);
                    InvokeEvents(OnPropertyChanged);
                };
            }
            else
            {
                input = new TextBox()
                {
                    Text = ConvertToString(prop.GetValue(target), prop.PropertyType),
                    Location = new Point(150, y),
                    Width = parent.Width - 180
                };

                if (Attribute.IsDefined(prop, typeof(MultiLineAttribute)))
                {
                    ((TextBox)input).Multiline = true;
                    input.Height = 60;
                }

                ((TextBox)input).TextChanged += (s, e) =>
                {
                    try
                    {
                        switch (prop.PropertyType)
                        {
                            case Type t when t == typeof(SerializablePoint):
                                {
                                    prop.SetValue(target, StringToPoint(((TextBox)input).Text));
                                    InvokeEvents(OnPropertyChanged);
                                    return;
                                }
                            case Type t when t == typeof(SerializableRectangle):
                                {
                                    prop.SetValue(target, StringToRectangle(((TextBox)input).Text));
                                    InvokeEvents(OnPropertyChanged);
                                    return;
                                }
                            case Type t when t == typeof(SerializableColor):
                                {
                                    prop.SetValue(target, StringToColor(((TextBox)input).Text));
                                    InvokeEvents(OnPropertyChanged);
                                    return;
                                }
                        }

                        object value = Convert.ChangeType(((TextBox)input).Text, prop.PropertyType);
                        prop.SetValue(target, value);
                        InvokeEvents(OnPropertyChanged);
                    }
                    catch { }
                };
            }

            parent.Controls.Add(input);

            if (Attribute.IsDefined(prop, typeof(MultiLineAttribute)))
            {
                y += 60;
            }
            else
            {
                y += 30;
            }
        }
    }
    
    private static void InvokeEvents(Action[] actions)
    {
        foreach (var action in actions)
        {
            action.Invoke();
        }
    }
    
    #region 変換用の関数
    private static string ConvertToString(object? value, Type type)
    {
        if (value == null) return string.Empty;

        switch (type)
        {
            case Type t when t == typeof(SerializablePoint):
                {
                    return PointToString((SerializablePoint)value);
                }
            case Type t when t == typeof(SerializableRectangle):
                {
                    return RectangleToString((SerializableRectangle)value);
                }
            case Type t when t == typeof(SerializableColor):
                {
                    return ColorToString((SerializableColor)value);
                }
        }

        return value.ToString() ?? string.Empty;
    }

    private static string PointToString(SerializablePoint point)
    {
        return $"{point.X}, {point.Y}";
    }
    private static SerializablePoint StringToPoint(string inputText)
    {
        try
        {
            var splitted = inputText.Replace(" ", "").Split(',');
            if (splitted.Length != 2) return new SerializablePoint();

            return new SerializablePoint(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }
        catch
        {
            return new SerializablePoint();
        }
    }

    private static string RectangleToString(SerializableRectangle rectangle)
    {
        return $"{rectangle.X}, {rectangle.Y}, {rectangle.Width}, {rectangle.Height}";
    }
    private static SerializableRectangle StringToRectangle(string inputText)
    {
        try
        {
            var splitted = inputText.Replace(" ", "").Split(',');
            if (splitted.Length != 4) return new SerializableRectangle();

            return new SerializableRectangle(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), int.Parse(splitted[3]));
        }
        catch
        {
            return new SerializableRectangle();
        }
    }

    private static string ColorToString(SerializableColor color)
    {
        return $"{color.R}, {color.G}, {color.B}, {color.A}";
    }
    private static SerializableColor StringToColor(string inputText)
    {
        try
        {
            var splitted = inputText.Replace(" ", "").Split(',');
            if (splitted.Length != 4) return new SerializableColor();

            return new SerializableColor(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), int.Parse(splitted[3]));
        }
        catch
        {
            return new SerializableColor();
        }
    }
    #endregion
}
