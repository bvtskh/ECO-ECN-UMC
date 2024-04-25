
namespace ECO_DX_For_PUR.GUI
{
    partial class FormChat
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
            this.btnSend = new Sunny.UI.UIButton();
            this.txtInbox = new Sunny.UI.UIRichTextBox();
            this.txtContent = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSend.Location = new System.Drawing.Point(268, 496);
            this.btnSend.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(123, 32);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "send";
            this.btnSend.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtInbox
            // 
            this.txtInbox.FillColor = System.Drawing.Color.White;
            this.txtInbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtInbox.Location = new System.Drawing.Point(43, 18);
            this.txtInbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInbox.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtInbox.Name = "txtInbox";
            this.txtInbox.Padding = new System.Windows.Forms.Padding(2);
            this.txtInbox.ScrollBarStyleInherited = false;
            this.txtInbox.ShowText = false;
            this.txtInbox.Size = new System.Drawing.Size(348, 454);
            this.txtInbox.TabIndex = 1;
            this.txtInbox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtContent
            // 
            this.txtContent.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtContent.Location = new System.Drawing.Point(43, 496);
            this.txtContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtContent.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtContent.Name = "txtContent";
            this.txtContent.Padding = new System.Windows.Forms.Padding(5);
            this.txtContent.ShowText = false;
            this.txtContent.Size = new System.Drawing.Size(218, 32);
            this.txtContent.TabIndex = 2;
            this.txtContent.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtContent.Watermark = "";
            // 
            // FormChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 542);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtInbox);
            this.Controls.Add(this.btnSend);
            this.Name = "FormChat";
            this.Text = "FormChat";
            this.Load += new System.EventHandler(this.FormChat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton btnSend;
        private Sunny.UI.UIRichTextBox txtInbox;
        private Sunny.UI.UITextBox txtContent;
    }
}