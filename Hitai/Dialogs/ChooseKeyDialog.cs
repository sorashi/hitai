using System;
using System.Linq;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai.Dialogs
{
    public partial class ChooseKeyDialog : Form
    {
        /// <summary>
        /// </summary>
        /// <param name="message">The message which will be shown in the dialog header</param>
        public ChooseKeyDialog(string message) {
            InitializeComponent();
            Message = message;
        }

        public string Message {
            get => labMessage.Text;
            set => labMessage.Text = value;
        }

        public Keychain Keychain {
            get => userControlKeychain1.Keychain;
            set => userControlKeychain1.Keychain = value;
        }

        public Keypair ChosenKeypair => userControlKeychain1.SelectedItems.FirstOrDefault();

        private void ChooseKeyDialog_Load(object sender, EventArgs e) {
        }

        private void ButOk_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
