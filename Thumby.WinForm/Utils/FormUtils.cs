namespace Thumby.WinForm.Utils;

internal class FormUtils
{
    /// <summary>
    /// エラーメッセージを表示する
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    internal static void ShowError(string message, string title = "エラー")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>
    /// 情報メッセージを表示する
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    internal static void ShowInfo(string message, string title = "情報")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

    /// <summary>
    /// Yes / No 形式の確認メッセージを表示する
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    internal static bool ShowConfirm(string message, string title = "確認")
    {
        DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return result == DialogResult.Yes;
    }
}
