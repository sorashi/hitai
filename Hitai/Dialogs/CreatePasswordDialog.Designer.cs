namespace Hitai.Dialogs
{
    partial class CreatePasswordDialog
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
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_confirm = new System.Windows.Forms.TextBox();
            this.label_strength = new System.Windows.Forms.Label();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.label_confirmation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(13, 33);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(347, 20);
            this.textBox_password.TabIndex = 0;
            this.textBox_password.UseSystemPasswordChar = true;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // textBox_confirm
            // 
            this.textBox_confirm.Location = new System.Drawing.Point(13, 59);
            this.textBox_confirm.Name = "textBox_confirm";
            this.textBox_confirm.Size = new System.Drawing.Size(347, 20);
            this.textBox_confirm.TabIndex = 1;
            this.textBox_confirm.UseSystemPasswordChar = true;
            this.textBox_confirm.TextChanged += new System.EventHandler(this.textBox_confirm_TextChanged);
            // 
            // label_strength
            // 
            this.label_strength.AutoSize = true;
            this.label_strength.Location = new System.Drawing.Point(12, 82);
            this.label_strength.Name = "label_strength";
            this.label_strength.Size = new System.Drawing.Size(45, 13);
            this.label_strength.TabIndex = 2;
            this.label_strength.Text = "strength";
            this.label_strength.Click += new System.EventHandler(this.label_strength_Click);
            // 
            // butOk
            // 
            this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOk.Location = new System.Drawing.Point(222, 130);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(57, 23);
            this.butOk.TabIndex = 3;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(285, 130);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 4;
            this.butCancel.Text = "Zrušit";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label_confirmation
            // 
            this.label_confirmation.AutoSize = true;
            this.label_confirmation.Location = new System.Drawing.Point(12, 97);
            this.label_confirmation.Name = "label_confirmation";
            this.label_confirmation.Size = new System.Drawing.Size(79, 13);
            this.label_confirmation.TabIndex = 5;
            this.label_confirmation.Text = "confirmation ok";
            this.label_confirmation.Click += new System.EventHandler(this.label_confirmation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Zadejte nové heslo";
            // 
            // CreatePasswordDialog
            // 
            this.AcceptButton = this.butOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 162);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_confirmation);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.label_strength);
            this.Controls.Add(this.textBox_confirm);
            this.Controls.Add(this.textBox_password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreatePasswordDialog";
            this.Text = "Vytvořit heslo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_confirm;
        private System.Windows.Forms.Label label_strength;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label label_confirmation;
        private System.Windows.Forms.Label label1;
    }
}