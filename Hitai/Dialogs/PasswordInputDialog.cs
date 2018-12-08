using System;
using System.Windows.Forms;

namespace Hitai.Dialogs
{
    public partial class PasswordInputDialog : Form
    {
        public PasswordInputDialog(string reason = null) {
            InitializeComponent();
            Reason = string.IsNullOrEmpty(reason) ? "neznámý" : reason;
        }

        public string Password { get; set; }

        public string Reason {
            get => label_reason.Text.Substring("Důvod: ".Length);
            set => label_reason.Text = "Důvod: " + value;
        }

        private void PasswordInputDialog_Shown(object sender, EventArgs e) {
            textBox_password.Focus();
        }

        private void butOk_Click(object sender, EventArgs e) {
            Password = textBox_password.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
