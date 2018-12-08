namespace Hitai.Dialogs
{
    partial class ChooseKeyDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.labMessage = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOk = new System.Windows.Forms.Button();
            this.userControlKeychain1 = new Hitai.UserControlKeychain();
            this.SuspendLayout();
            // 
            // labMessage
            // 
            this.labMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labMessage.Location = new System.Drawing.Point(12, 9);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(487, 57);
            this.labMessage.TabIndex = 0;
            this.labMessage.Text = "{{message}}";
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(424, 346);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 2;
            this.butCancel.Text = "Zrušit";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOk.Location = new System.Drawing.Point(343, 346);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 3;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.ButOk_Click);
            // 
            // userControlKeychain1
            // 
            this.userControlKeychain1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userControlKeychain1.Location = new System.Drawing.Point(12, 69);
            this.userControlKeychain1.Name = "userControlKeychain1";
            this.userControlKeychain1.Size = new System.Drawing.Size(487, 271);
            this.userControlKeychain1.TabIndex = 1;
            // 
            // ChooseKeyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 381);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.userControlKeychain1);
            this.Controls.Add(this.labMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(16, 420);
            this.Name = "ChooseKeyDialog";
            this.Text = "Vyberte klíčový pár";
            this.Load += new System.EventHandler(this.ChooseKeyDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labMessage;
        private UserControlKeychain userControlKeychain1;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOk;
    }
}