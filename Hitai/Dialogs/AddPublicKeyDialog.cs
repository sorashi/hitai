using Hitai.ArmorProviders;
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
    public partial class AddPublicKeyDialog : Form
    {
        public Keychain Keychain { get; set; }
        public AddPublicKeyDialog(Keychain keychain) {
            InitializeComponent();
            this.Keychain = keychain;
        }

        private void butCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void butOk_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            var bytes = Encoding.UTF8.GetBytes(textBox.Text);
            var armorType = ArmorRecognizer.RecognizeArmor(bytes);
            var armor = Activator.CreateInstance(armorType) as IArmorProvider;
            var (rawData, armorDataType) = armor.FromArmor(bytes);
            if(armorDataType != ArmorType.PublicKey) {
                MessageBox.Show("Vložený text není veřejný klíč.");
                this.Close();
                return;
            }
            await Keychain.AddKeyPair(KeyPair.FromMessagePack(rawData));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
