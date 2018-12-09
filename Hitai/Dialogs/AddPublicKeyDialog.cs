using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Hitai.ArmorProviders;
using Hitai.AsymmetricEncryption;

namespace Hitai.Dialogs
{
    public partial class AddPublicKeyDialog : Form
    {
        public AddPublicKeyDialog(Keychain keychain) {
            InitializeComponent();
            Keychain = keychain;
        }

        public Keychain Keychain { get; set; }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void butOk_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            byte[] bytes = Encoding.UTF8.GetBytes(textBox.Text);
            Type armorType = ArmorRecognizer.RecognizeArmor(bytes);
            var armor = Activator.CreateInstance(armorType) as IArmorProvider;
            Debug.Assert(armor != null, nameof(armor) + " != null");
            (byte[] rawData, ArmorType armorDataType) = armor.FromArmor(bytes);
            if (armorDataType != ArmorType.PublicKey) {
                MessageBox.Show("Vložený text není veřejný klíč.");
                Close();
                return;
            }

            await Keychain.AddKeyPair(Keypair.FromMessagePack(rawData));
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
