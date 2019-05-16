using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai.Dialogs
{
    public partial class GenerateKeyPairDialog : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();

        private IAsymmetricEncryptionProvider _aep;

        public GenerateKeyPairDialog() {
            InitializeComponent();
            comboBox_model.SelectedIndex = 0;
            dateTimePicker.MinDate = DateTime.Now;
            dateTimePicker.Value = DateTime.Now.AddYears(4);
        }

        public Keypair Keypair { get; set; }

        public bool Cancelled { get; set; }

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
                        var sym = new SystemAsymmetricEncryptionProvider();
                        sym.EnsurePositiveModulus();
                        _aep = sym;
                        break;
                    case 1: // Hitai RSA
                        var hiaep = new HitaiAsymmetricEncryptionProvider();
                        try {
                            hiaep.GenerateNewKeypair(_cancellationTokenSource.Token);
                        }
                        catch (OperationCanceledException) {
                            Cancelled = true;
                            return;
                        }

                        _aep = hiaep;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
            if (Cancelled) return;
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
            _cancellationTokenSource.Cancel();
            Cancelled = true;
            Close();
        }

        private void butSave_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
