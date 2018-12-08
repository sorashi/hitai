using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai.Dialogs
{
    public partial class GenerateKeyPairDialog : Form
    {
        private IAsymmetricEncryptionProvider _aep;

        public GenerateKeyPairDialog() {
            InitializeComponent();
            comboBox_model.SelectedIndex = 0;
            dateTimePicker.MinDate = DateTime.Now;
            dateTimePicker.Value = DateTime.Now.AddYears(4);
        }

        public KeyPair Keypair { get; set; }

        private async void butGenerate_Click(object sender, EventArgs e) {
            // validate form
            textBox_name.Text = textBox_name.Text.Trim();
            bool nameValid = !string.IsNullOrWhiteSpace(textBox_name.Text);
            textBox_email.Text = textBox_email.Text.Trim();
            bool emailValid = !string.IsNullOrWhiteSpace(textBox_email.Text);
            if (emailValid)
                try {
                    new MailAddress(textBox_email.Text);
                }
                catch {
                    emailValid = false;
                }

            if (!nameValid) {
                MessageBox.Show("Jméno musí být vyplněno.");
                return;
            }

            if (!emailValid) {
                MessageBox.Show("E-mail není v pořádku.");
                return;
            }

            butGenerate.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;
            int selectedItem = comboBox_model.SelectedIndex;
            await Task.Factory.StartNew(() => {
                switch (selectedItem) {
                    case 0: // .NET RSA
                        _aep = new SystemAsymmetricEncryptionProvider();
                        break;
                    case 1: // Hitai RSA
                        var hiaep = new HitaiAsymmetricEncryptionProvider();
                        hiaep.GenerateNewKeypair();
                        _aep = hiaep;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
            progressBar.Style = ProgressBarStyle.Continuous;
            butGenerate.Enabled = true;
            var passwordCreationDialog = new CreatePasswordDialog();
            if (passwordCreationDialog.ShowDialog() != DialogResult.OK)
                return;
            Keypair = _aep.GetPrivateKey(passwordCreationDialog.Password);
            Keypair.CreationTime = DateTime.Now;
            Keypair.Expires = dateTimePicker.Value;
            Keypair.UserId = $"{textBox_name.Text} <{textBox_email.Text}>";
            Keypair.RsaProvider =
                Array.IndexOf(AsymmetricEncryptionController.ProviderList, _aep.GetType());
            textBox_id.Text = Keypair.ShortId;
            butSave.Enabled = true;
        }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void butSave_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
