using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai.Dialogs
{
    public partial class ChooseKeyDialog : Form
    {
        public string Message {
            get => labMessage.Text;
            set => labMessage.Text = value;
        }

        public Keychain Keychain {
            get => userControlKeychain1.Keychain;
            set => userControlKeychain1.Keychain = value;
        }

        public KeyPair ChosenKeyPair => userControlKeychain1.SelectedItems.FirstOrDefault();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The message which will be shown in the dialog header</param>
        public ChooseKeyDialog(string message) {
            InitializeComponent();
            Message = message;
        }

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
