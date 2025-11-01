using System.Text.Json;
using Thumby.Core.Interfaces;
using Thumby.Core.Models;
using Thumby.Core.Models.Layer;
using Thumby.WinForm.LayerProcessing;
using Thumby.WinForm.Services;
using Thumby.WinForm.SubForms;
using Thumby.WinForm.Utils;
using Timer = System.Windows.Forms.Timer;

namespace Thumby.WinForm.MainForms;

public partial class LayerManagerForm : Form
{
    private static readonly Font TitleFont = new Font("Yu Gothic UI", 11F);

    internal event Action? OnLayerChanged;

    internal int CanvasSize
        => int.TryParse(canvasSizeTextBox.Text, out int result) ? result : 0;

    private readonly List<ICanvasLayer> _canvasLayers = new();

    private static readonly ContextMenuStrip _contextMenu = new();
    private int _rightClickedIndex = -1;

    private readonly PreviewZoomForm _previewZoomForm = new();
    private readonly AddLayerForm _addLayerForm = new();

    private readonly Timer _autoPreviewTimer = new()
    {
        Interval = 500
    };

    private readonly ToolStripItem _itemTitleToolStripItem;

    public LayerManagerForm()
    {
        InitializeComponent();
        _previewZoomForm.Show();

        _itemTitleToolStripItem = _contextMenu.Items.Add("レイヤー: Null");
        _itemTitleToolStripItem.Enabled = false;
        _itemTitleToolStripItem.Font = TitleFont;
        _contextMenu.Items.Add(new ToolStripSeparator());
        
        _contextMenu.Items.Add("上へ移動").Click += MoveLayerUp;
        _contextMenu.Items.Add("下へ移動").Click += MoveLayerDown;
        _contextMenu.Items.Add("一番上へ移動").Click += MoveCheckedListBoxItemToTop;
        _contextMenu.Items.Add("一番下へ移動").Click += MoveCheckedListBoxItemToBottom;
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add("レイヤーを複製").Click += CopyLayer;
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add("レイヤーを削除").Click += RemoveLayer;

        canvasLayerList.ContextMenuStrip = _contextMenu;

        _autoPreviewTimer.Tick += (s, ev) =>
        {
            if (!autoPreview.Checked) return;

            _autoPreviewTimer.Stop();
            BeginInvoke(() => RenderPreview());
        };
    }

    private void RefleshPriorityText()
    {
        for (int i = 0; i < canvasLayerList.Items.Count; i++)
        {
            canvasLayerList.Items[i] = _canvasLayers[i].ToString(i + 1);
        }
    }

    #region 設定ファイル関連
    private void LoadSettingsFile(string path)
    {
        if (!File.Exists(path))
        {
            FormUtils.ShowError("ファイルが存在しません。");
            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            List<ICanvasLayer>? canvasLayers = JsonSerializer.Deserialize<List<ICanvasLayer>>(json);

            if (canvasLayers == null)
            {
                FormUtils.ShowError("設定ファイルの読み込みに失敗しました。");
            }
            else
            {
                _canvasLayers.Clear();
                _canvasLayers.AddRange(canvasLayers);

                foreach (var canvasLayer in _canvasLayers)
                {
                    canvasLayerList.Items.Insert(0, canvasLayer.ToString(-1));
                }

                RefleshPriorityText();
                FormUtils.ShowInfo("設定ファイルの読み込みに成功しました！");
            }
        }
        catch
        {
            FormUtils.ShowError("設定ファイルの読み込みに失敗しました。");
        }
    }

    private void ExportSettingsFile(string path)
    {
        try
        {
            string json = JsonSerializer.Serialize(_canvasLayers);
            File.WriteAllText(path, json);

            FormUtils.ShowInfo("ファイルの出力に成功しました！");
        }
        catch
        {
            FormUtils.ShowError("ファイルの出力に失敗しました。");
        }
    }
    #endregion

    #region 右クリックメニュー処理
    private void MoveLayerUp(object? sender, EventArgs e)
    {
        if (_rightClickedIndex <= 0) return;

        SwapLayers(_rightClickedIndex, _rightClickedIndex - 1);
        _rightClickedIndex--;

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void MoveLayerDown(object? sender, EventArgs e)
    {
        if (_rightClickedIndex == -1 || _rightClickedIndex >= _canvasLayers.Count - 1) return;

        SwapLayers(_rightClickedIndex, _rightClickedIndex + 1);
        _rightClickedIndex++;

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void MoveCheckedListBoxItemToTop(object? sender, EventArgs e)
    {
        if (_rightClickedIndex <= 0) return;

        for (int i = _rightClickedIndex; i > 0; i--)
        {
            SwapLayers(i, i - 1);
        }

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void MoveCheckedListBoxItemToBottom(object? sender, EventArgs e)
    {
        int last = _canvasLayers.Count - 1;
        if (_rightClickedIndex == -1 || _rightClickedIndex >= last) return;

        for (int i = _rightClickedIndex; i < last; i++)
        {
            SwapLayers(i, i + 1);
        }

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void SwapLayers(int index1, int index2)
    {
        (_canvasLayers[index1], _canvasLayers[index2]) = (_canvasLayers[index2], _canvasLayers[index1]);
        (canvasLayerList.Items[index2], canvasLayerList.Items[index1]) = (canvasLayerList.Items[index1], canvasLayerList.Items[index2]);
    }

    private void CopyLayer(object? sender, EventArgs e)
    {
        if (_rightClickedIndex == -1) return;

        ICanvasLayer canvasLayer = _canvasLayers[_rightClickedIndex].Clone();

        canvasLayerList.Items.Insert(0, canvasLayer.ToString(-1));
        _canvasLayers.Insert(0, canvasLayer);

        int index = 1;
        while (_canvasLayers.Any(canvasLayer => canvasLayer.Index == index))
        {
            index++;
        }

        canvasLayer.Index = index;

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void RemoveLayer(object? sender, EventArgs e)
    {
        if (_rightClickedIndex == -1) return;

        ICanvasLayer canvasLayer = _canvasLayers[_rightClickedIndex];
        bool result = FormUtils.ShowConfirm($"このレイヤーを削除しますか？\n\nレイヤー名: {canvasLayer.GetLayerName()}");
        if (!result) return;

        _canvasLayers.RemoveAt(_rightClickedIndex);
        canvasLayerList.Items.RemoveAt(_rightClickedIndex);

        RefleshPriorityText();
        TriggerCheckedChanged();
    }

    private void OnLayerPropertyChanged()
    {
        _autoPreviewTimer.Stop();
        _autoPreviewTimer.Start();
    }
    #endregion

    #region イベントハンドラー
    private void CanvasLayerList_MouseDown(object sender, MouseEventArgs e)
    {
        int clickedIndex = canvasLayerList.IndexFromPoint(e.Location);

        if (e.Button == MouseButtons.Right)
        {
            _rightClickedIndex = clickedIndex;
            if (_rightClickedIndex != -1) _itemTitleToolStripItem.Text = $"レイヤー: {_canvasLayers[_rightClickedIndex].GetLayerName()}";
        }
        else if (e.Button == MouseButtons.Left && clickedIndex != -1)
        {
            UIBuilder.BuildUI(propertyPanel, _canvasLayers[clickedIndex], [RefleshPriorityText, OnLayerPropertyChanged]);
        }
    }

    private void CanvasLayerList_SelectedIndexChanged(object sender, EventArgs e)
        => canvasLayerList.SelectedIndex = -1;

    private void AddLayerButton_Click(object sender, EventArgs e)
    {
        LayerType layerType = _addLayerForm.ShowSelectionDialog();

        ICanvasLayer? canvasLayer = null;
        switch (layerType)
        {
            case LayerType.Text: canvasLayer = new ThumbnailText(); break;
            case LayerType.Image: canvasLayer = new ThumbnailImage(); break;
            case LayerType.Rectangle: canvasLayer = new ThumbnailRectangle(); break;
            case LayerType.Line: canvasLayer = new ThumbnailLine(); break;
            case LayerType.LayerEffect: canvasLayer = new LayerEffect(); break;
        }

        if (canvasLayer == null) return;

        int index = 1;
        while (_canvasLayers.Any(canvasLayer => canvasLayer.Index == index))
        {
            index++;
        }

        canvasLayer.Index = index;

        canvasLayerList.Items.Insert(0, canvasLayer.ToString(-1));
        _canvasLayers.Insert(0, canvasLayer);

        UIBuilder.BuildUI(propertyPanel, canvasLayer, [RefleshPriorityText, OnLayerPropertyChanged]);

        RefleshPriorityText();
    }

    private void CanvasSizeTextBox_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(canvasSizeTextBox.Text)) return;
        canvasSizeTextBox.Text = CanvasSize.ToString();
    }

    private void RenderPreviewButton_Click(object sender, EventArgs e)
    {
        try
        {
            RenderPreview();
        }
        catch
        {
            FormUtils.ShowError("プレビューが作成できませんでした");
        }
    }

    private void LoadData_Click(object sender, EventArgs e)
    {
        OpenFileDialog dialog = new()
        {
            Filter = "設定ファイル|*.tmb",
            Title = "設定ファイルを選択してください"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;
        LoadSettingsFile(dialog.FileName);
    }

    private void SaveData_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = new()
        {
            Filter = "設定ファイル|*.tmb",
            Title = "設定ファイルの保存先を選択してください",
            FileName = $"MyThumbnail_{DateTime.Now:MM-dd_HH-mm-ss}.tmb",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;
        ExportSettingsFile(dialog.FileName);
    }

    private void SaveResult_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = FormUtils.ShowConfirm("画像を作成しますか？");
            if (!result) return;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "PNGファイル|*.png;",
                Title = "出力画像の保存先を選択してください",
                FileName = "New Thumbnail.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;
            string newFilePath = dialog.FileName;

            if (newFilePath == string.Empty)
            {
                FormUtils.ShowError("ファイルの保存先が選択されていません。");
                return;
            }

            Bitmap bitmap = new(CanvasSize, CanvasSize);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                LayerProcessor.ProcessLayers(bitmap, graphics, _canvasLayers, false);
            }

            bitmap.Save(newFilePath);

            FormUtils.ShowInfo("ファイルの出力に成功しました！");
        }
        catch
        {
            FormUtils.ShowError("ファイルの出力に失敗しました。");
        }
    }

    private void TriggerCheckedChanged()
        => OnLayerChanged?.Invoke();
    #endregion

    #region プレビュー生成
    private void RenderPreview()
    {
        var bitmap = new Bitmap(CanvasSize, CanvasSize);

        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            LayerProcessor.ProcessLayers(bitmap, graphics, _canvasLayers, true);
        }

        _previewZoomForm.SetImage(bitmap, true);
        if (!_previewZoomForm.Visible) _previewZoomForm.Show();
    }
    #endregion
}
