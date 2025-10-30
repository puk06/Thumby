using Thumby.Core.Models;

namespace Thumby.WinForm.SubForms;

public partial class AddLayerForm : Form
{
    private LayerType _selectedLayerType = LayerType.None;

    public AddLayerForm()
    {
        InitializeComponent();
    }

    internal LayerType ShowSelectionDialog()
    {
        ShowDialog();
        return _selectedLayerType;
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        _selectedLayerType = LayerType.Text;
        Close();
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        _selectedLayerType = LayerType.Image;
        Close();
    }

    private void Button3_Click(object sender, EventArgs e)
    {
        _selectedLayerType = LayerType.Rectangle;
        Close();
    }

    private void Button4_Click(object sender, EventArgs e)
    {
        _selectedLayerType = LayerType.LayerEffect;
        Close();
    }

    private void Button5_Click(object sender, EventArgs e)
    {
        _selectedLayerType = LayerType.None;
        Close();
    }
}
