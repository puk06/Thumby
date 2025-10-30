namespace Thumby.WinForm.MainForms
{
    partial class LayerManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            canvasLayerList = new ListBox();
            label1 = new Label();
            layerLabel = new Label();
            addLayerButton = new Button();
            canvasSizeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            renderPreviewButton = new Button();
            propertyPanel = new Panel();
            menuStrip1 = new MenuStrip();
            ファイルToolStripMenuItem = new ToolStripMenuItem();
            loadSettingsData = new ToolStripMenuItem();
            saveSettingsData = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveResult = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // canvasLayerList
            // 
            canvasLayerList.Font = new Font("Yu Gothic UI", 12F);
            canvasLayerList.FormattingEnabled = true;
            canvasLayerList.HorizontalScrollbar = true;
            canvasLayerList.ItemHeight = 21;
            canvasLayerList.Location = new Point(12, 89);
            canvasLayerList.Name = "canvasLayerList";
            canvasLayerList.Size = new Size(286, 340);
            canvasLayerList.TabIndex = 0;
            canvasLayerList.SelectedIndexChanged += CanvasLayerList_SelectedIndexChanged;
            canvasLayerList.MouseDown += CanvasLayerList_MouseDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI Semibold", 16F, FontStyle.Bold);
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(178, 30);
            label1.TabIndex = 1;
            label1.Text = "レイヤー管理パネル";
            // 
            // layerLabel
            // 
            layerLabel.AutoSize = true;
            layerLabel.Font = new Font("Yu Gothic UI", 11F);
            layerLabel.Location = new Point(12, 61);
            layerLabel.Name = "layerLabel";
            layerLabel.Size = new Size(213, 20);
            layerLabel.TabIndex = 2;
            layerLabel.Text = "優先順位  |   タイプ   |   レイヤー名";
            // 
            // addLayerButton
            // 
            addLayerButton.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            addLayerButton.Location = new Point(12, 435);
            addLayerButton.Name = "addLayerButton";
            addLayerButton.Size = new Size(139, 40);
            addLayerButton.TabIndex = 3;
            addLayerButton.Text = "レイヤーを追加する";
            addLayerButton.UseVisualStyleBackColor = true;
            addLayerButton.Click += AddLayerButton_Click;
            // 
            // canvasSizeTextBox
            // 
            canvasSizeTextBox.Font = new Font("Yu Gothic UI", 12F);
            canvasSizeTextBox.Location = new Point(304, 50);
            canvasSizeTextBox.Name = "canvasSizeTextBox";
            canvasSizeTextBox.PlaceholderText = "1024";
            canvasSizeTextBox.Size = new Size(285, 29);
            canvasSizeTextBox.TabIndex = 4;
            canvasSizeTextBox.Text = "1024";
            canvasSizeTextBox.TextChanged += CanvasSizeTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label2.Location = new Point(304, 24);
            label2.Name = "label2";
            label2.Size = new Size(114, 23);
            label2.TabIndex = 5;
            label2.Text = "キャンバスサイズ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label3.Location = new Point(304, 89);
            label3.Name = "label3";
            label3.Size = new Size(72, 23);
            label3.TabIndex = 6;
            label3.Text = "プロパティ";
            // 
            // renderPreviewButton
            // 
            renderPreviewButton.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            renderPreviewButton.Location = new Point(159, 435);
            renderPreviewButton.Name = "renderPreviewButton";
            renderPreviewButton.Size = new Size(139, 40);
            renderPreviewButton.TabIndex = 8;
            renderPreviewButton.Text = "プレビューの生成";
            renderPreviewButton.UseVisualStyleBackColor = true;
            renderPreviewButton.Click += RenderPreviewButton_Click;
            // 
            // propertyPanel
            // 
            propertyPanel.BackColor = SystemColors.Control;
            propertyPanel.BorderStyle = BorderStyle.FixedSingle;
            propertyPanel.Location = new Point(304, 115);
            propertyPanel.Name = "propertyPanel";
            propertyPanel.Size = new Size(285, 360);
            propertyPanel.TabIndex = 9;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルToolStripMenuItem });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(600, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            ファイルToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadSettingsData, saveSettingsData, toolStripSeparator1, saveResult });
            ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            ファイルToolStripMenuItem.Size = new Size(53, 20);
            ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // loadSettingsData
            // 
            loadSettingsData.Name = "loadSettingsData";
            loadSettingsData.Size = new Size(184, 22);
            loadSettingsData.Text = "設定ファイルを開く";
            loadSettingsData.Click += LoadData_Click;
            // 
            // saveSettingsData
            // 
            saveSettingsData.Name = "saveSettingsData";
            saveSettingsData.Size = new Size(184, 22);
            saveSettingsData.Text = "設定ファイルを出力する";
            saveSettingsData.Click += SaveData_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(181, 6);
            // 
            // saveResult
            // 
            saveResult.Name = "saveResult";
            saveResult.Size = new Size(184, 22);
            saveResult.Text = "画像を出力する";
            saveResult.Click += SaveResult_Click;
            // 
            // LayerManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 485);
            Controls.Add(propertyPanel);
            Controls.Add(renderPreviewButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(canvasSizeTextBox);
            Controls.Add(addLayerButton);
            Controls.Add(layerLabel);
            Controls.Add(label1);
            Controls.Add(canvasLayerList);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "LayerManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thumby - Canvas Layer Manager";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox canvasLayerList;
        private Label label1;
        private Label layerLabel;
        private Button addLayerButton;
        private TextBox canvasSizeTextBox;
        private Label label2;
        private Label label3;
        private Button renderPreviewButton;
        private Panel propertyPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルToolStripMenuItem;
        private ToolStripMenuItem loadSettingsData;
        private ToolStripMenuItem saveSettingsData;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveResult;
    }
}
