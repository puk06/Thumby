namespace Thumby.WinForm.SubForms
{
    partial class AddLayerForm
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
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 14F);
            button1.Location = new Point(12, 63);
            button1.Name = "button1";
            button1.Size = new Size(289, 51);
            button1.TabIndex = 0;
            button1.Text = "テキスト";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(229, 40);
            label1.TabIndex = 1;
            label1.Text = "レイヤーを追加する";
            // 
            // button2
            // 
            button2.Font = new Font("Yu Gothic UI", 14F);
            button2.Location = new Point(12, 120);
            button2.Name = "button2";
            button2.Size = new Size(289, 51);
            button2.TabIndex = 2;
            button2.Text = "画像";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Yu Gothic UI", 14F);
            button3.Location = new Point(12, 177);
            button3.Name = "button3";
            button3.Size = new Size(289, 51);
            button3.TabIndex = 3;
            button3.Text = "正方形";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Yu Gothic UI", 14F);
            button4.Location = new Point(12, 234);
            button4.Name = "button4";
            button4.Size = new Size(289, 51);
            button4.TabIndex = 4;
            button4.Text = "レイヤー効果";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button5.Location = new Point(12, 332);
            button5.Name = "button5";
            button5.Size = new Size(289, 51);
            button5.TabIndex = 5;
            button5.Text = "キャンセル";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // AddLayerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(313, 395);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AddLayerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thumby - Add Layer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}
