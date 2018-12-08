using Hitai.AsymmetricEncryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitai.Dialogs
{
    public partial class GenerateKeyPairDialog : Form
    {
        public GenerateKeyPairDialog() {
            InitializeComponent();
            comboBox_model.SelectedIndex = 0;
            dateTimePicker.MinDate = DateTime.Now;
            dateTimePicker.Value = DateTime.Now.AddYears(4);
        }
        IAsymmetricEncryptionProvider aep;
        public KeyPair Keypair { get; set; }
        private async void butGenerate_Click(object sender, EventArgs e) {
            // validate form
            textBox_name.Text = textBox_name.Text.Trim();
            var nameValid = !string.IsNullOrWhiteSpace(textBox_name.Text);
            textBox_email.Text = textBox_email.Text.Trim();
            var emailValid = !string.IsNullOrWhiteSpace(textBox_email.Text);
            if(emailValid)
                try {
                    new System.Net.Mail.MailAddress(textBox_email.Text);
                }
                catch {
                    emailValid = false;
                }
            if(!nameValid) {
                MessageBox.Show("Jméno musí být vyplněno.");
                return;
            }
            if(!emailValid) {
                MessageBox.Show("E-mail není v pořádku.");
                return;
            }
            this.butGenerate.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;
            var selectedItem = comboBox_model.SelectedIndex;
            await Task.Factory.StartNew(() => {
                switch (selectedItem) {
                    case 0: // .NET RSA
                        aep = new SystemAsymmetricEncryptionProvider();
                        break;
                    case 1: // Hitai RSA
                        var hiaep = new HitaiAsymmetricEncryptionProvider();
                        hiaep.GenerateNewKeypair();
                        aep = hiaep;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
            progressBar.Style = ProgressBarStyle.Continuous;
            this.butGenerate.Enabled = true;
            var passwordCreationDialog = new CreatePasswordDialog();
            if(passwordCreationDialog.ShowDialog() != DialogResult.OK)
                return;
            Keypair = aep.GetPrivateKey(passwordCreationDialog.Password);
            Keypair.CreationTime = DateTime.Now;
            Keypair.Expires = dateTimePicker.Value;
            Keypair.UserId = $"{textBox_name.Text} <{textBox_email.Text}>";
            Keypair.RsaProvider = Array.IndexOf(AsymmetricEncryptionController.ProviderList, aep.GetType());
            textBox_id.Text = Keypair.ShortId;
            this.butSave.Enabled = true;
        }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void butSave_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
