using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hitai.ArmorProviders;
using Hitai.AsymmetricEncryption;
using Hitai.Dialogs;
using Hitai.Math;
using Hitai.Models;
using Hitai.Properties;
using MessagePack;
using Message = Hitai.Models.Message;
using Squirrel;
using System.Reflection;

namespace Hitai
{
    public partial class FormMain : Form
    {
        public FormMain() {
            InitializeComponent();
            Icon = Resources.logo;
            comboBox_actions.SelectedIndex = 0;
            label_errors.Text = "";
            NewPrimes();
            Recompute()
                .ContinueWith(x => {
                    if (x.Result)
                        numeric_m.Value = new Random().Next(System.Math.Min(100, N - 4),
                            System.Math.Min(300, N - 1));
                });
        }

        public Keychain Keychain { get; set; }

        private void buttonAddPublicKey_Click(object sender, EventArgs e) {
            var dialog = new AddPublicKeyDialog(Keychain);
            dialog.ShowDialog();
        }

        private async void FormMain_Load(object sender, EventArgs e) {
#if !DEBUG
            using (var mgr = new UpdateManager("http://prazak.xf.cz/projects/hitai")) {
                ReleaseEntry result = null;
                try {
                    result = await mgr.UpdateApp();
                }
                catch (Exception exception) {
                    MessageBox.Show(
                        "Nepodařilo se zkontrolovat novou verzi (buď chybí připojení k internetu, nebo tato verze již není podporována). " +
                        "Pokud není Hitai aktuální, nemůže zajistit bezpečnost.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (result != null) {
                    MessageBox.Show(
                        $"Došlo k aktualizaci na novou verzi: {result.Version}. Nyní bude Hitai restartován.", "Aktualizace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(Path.Combine(mgr.RootAppDirectory, "Hitai.exe"));
                    Application.Exit();
                }
            }
#endif
            Keychain = await Keychain.GetInstance();
            ucKeychain_keychainTab.Keychain = Keychain;
            ucKeychain_mainTab.Keychain = Keychain;
            label_version.Text = $"Hitai {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        private async void buttonDeleteKey_Click(object sender, EventArgs e) {
            foreach (Keypair keypair in ucKeychain_keychainTab.SelectedItems)
                await Keychain.RemoveKeyPair(keypair);
        }

        private async void buttonGenerateNewPair_Click(object sender, EventArgs e) {
            var dialog = new GenerateKeyPairDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                await Keychain.AddKeyPair(dialog.Keypair);
        }

        private void butProvest_Click(object sender, EventArgs e) {
            // TODO allow file import
            // todo make async
            byte[] content = Encoding.UTF8.GetBytes(textBox_main.Text);
            Keypair chosenKeypair = ucKeychain_mainTab.SelectedItems.FirstOrDefault();
            IArmorProvider armorProvider = new HitaiArmorProvider();
            Message message;
            PasswordInputDialog passwordDialog;
            Signature signature;
            bool correctSignature;
            switch (comboBox_actions.SelectedIndex) {
                case 0: // šifrovat
                    if (chosenKeypair == null) {
                        MessageBox.Show("Musíte vybrat klíč adresáta.");
                        return;
                    }

                    message = AsymmetricEncryptionController.Encrypt(chosenKeypair, content);
                    byte[] armor =
                        armorProvider.ToArmor(LZ4MessagePackSerializer.Serialize(message),
                            ArmorType.Message);
                    string result = Encoding.UTF8.GetString(armor);
                    Clipboard.SetText(result);
                    MessageBox.Show("Výsledek byl zkopírován do schránky.");
                    break;
                case 1: // dešifrovat
                    Type armorClass = ArmorRecognizer.RecognizeArmor(content);
                    armorProvider = (IArmorProvider) Activator.CreateInstance(armorClass);
                    (byte[] bytes, ArmorType armorType) = armorProvider.FromArmor(content);
                    // TODO nechat rozpoznat akci podle ArmorType
                    if (armorType != ArmorType.Message) {
                        MessageBox.Show("Obsah není zprávou.");
                        return;
                    }

                    message = LZ4MessagePackSerializer.Deserialize<Message>(bytes);
                    Keypair recipient =
                        Keychain.Keys.FirstOrDefault(x => x.ShortId == message.RecipientId);
                    if (recipient == null) {
                        MessageBox.Show("Nebyl nalezen odpovídající soukromý klíč.");
                        return;
                    }

                    passwordDialog = new PasswordInputDialog("Dešifrování");
                    if (passwordDialog.ShowDialog() != DialogResult.OK) return;
                    byte[] data;
                    try {
                        data = AsymmetricEncryptionController.Decrypt(message,
                            passwordDialog.Password,
                            recipient);
                    }
                    catch (Exception ex) {
                        while (ex is AggregateException) ex = ex.InnerException;
                        if (!(ex is CryptographicException)) throw;
                        MessageBox.Show("Nesprávné heslo.");
                        return;
                    }

                    string clearText = Encoding.UTF8.GetString(data);
                    textBox_main.Text = clearText;
                    break;
                case 2: // podepsat
                    if (!chosenKeypair.IsPrivate) {
                        MessageBox.Show("Podepisující klíč musí být soukromý.");
                        return;
                    }

                    passwordDialog = new PasswordInputDialog("Podepisování");
                    if (passwordDialog.ShowDialog() != DialogResult.OK) return;
                    try {

                        signature = AsymmetricEncryptionController.Sign(
                            Encoding.UTF8.GetBytes(textBox_main.Text),
                            passwordDialog.Password, chosenKeypair);
                    }
                    catch (Exception ex) {
                        while (ex is AggregateException) ex = ex.InnerException;
                        if (!(ex is CryptographicException)) throw;
                        MessageBox.Show("Nesprávné heslo.");
                        return;
                    }
                    Clipboard.SetText(Encoding.UTF8.GetString(armorProvider.ToArmor(
                        LZ4MessagePackSerializer.Serialize(signature),
                        ArmorType.Signature)));
                    MessageBox.Show("Výsledek byl zkopírován do schránky");
                    break;
                case 3: // ověřit
                    armorProvider =
                        (IArmorProvider) Activator.CreateInstance(
                            ArmorRecognizer.RecognizeArmor(content));
                    (byte[] bytes2, ArmorType armorType2) = armorProvider.FromArmor(content);
                    if (armorType2 != ArmorType.Signature) {
                        MessageBox.Show("Vstup neobsahuje podpis.");
                        return;
                    }

                    signature = LZ4MessagePackSerializer.Deserialize<Signature>(bytes2);
                    correctSignature = AsymmetricEncryptionController.Verify(signature, Keychain);
                    string clearText2;
                    try {
                        clearText2 = signature.GetCleartext();
                    }
                    catch {
                        clearText2 = null;
                    }

                    MessageBox.Show((correctSignature ? "Správný podpis" : "Nesprávný podpis") +
                                    (string.IsNullOrEmpty(clearText2)
                                        ? "."
                                        : " pro zprávu:\n" + clearText2));
                    break;
                case 4: // šifrovat a podepsat
                    var chooseKeyDialog =
                        new ChooseKeyDialog("Vyberte klíč, kterým chcete zprávu podepsat.")
                            {Keychain = Keychain};
                    if (chooseKeyDialog.ShowDialog() != DialogResult.OK)
                        return;
                    message = AsymmetricEncryptionController.Encrypt(chosenKeypair, content);
                    passwordDialog = new PasswordInputDialog("Podepisování");
                    if (passwordDialog.ShowDialog() != DialogResult.OK) return;
                    try {
                        signature = AsymmetricEncryptionController.Sign(message,
                            passwordDialog.Password, chooseKeyDialog.ChosenKeypair);
                    }
                    catch (Exception ex) {
                        while (ex is AggregateException) ex = ex.InnerException;
                        if (!(ex is CryptographicException)) throw;
                        MessageBox.Show("Nesprávné heslo.");
                        return;
                    }
                    result = Encoding.UTF8.GetString(armorProvider.ToArmor(
                        LZ4MessagePackSerializer.Serialize(signature),
                        ArmorType.SignedMessage));
                    Clipboard.SetText(result);
                    MessageBox.Show("Výsledek byl zkopírován do schránky");
                    break;
                case 5: // dešifrovat a ověřit
                    armorProvider =
                        (IArmorProvider) Activator.CreateInstance(
                            ArmorRecognizer.RecognizeArmor(content));
                    (byte[] bytes3, ArmorType armorType3) = armorProvider.FromArmor(content);
                    if (armorType3 != ArmorType.SignedMessage) {
                        MessageBox.Show("Vstup neobsahuje podepsanou zprávu.");
                        return;
                    }

                    signature = LZ4MessagePackSerializer.Deserialize<Signature>(bytes3);
                    correctSignature = AsymmetricEncryptionController.Verify(signature, Keychain);
                    if (!correctSignature) {
                        MessageBox.Show(
                            "Podpis není správný. Zpráva nebude dešifrována z bezpečnostních důvodů.");
                        return;
                    }

                    if (!signature.ContainsMessage()) {
                        MessageBox.Show(
                            "Podpis neobsahuje zašifrovanou zprávu. Chyba.");
                        return;
                    }

                    passwordDialog = new PasswordInputDialog("Dešifrování");
                    if (passwordDialog.ShowDialog() != DialogResult.OK) return;
                    try {
                        result = Encoding.UTF8.GetString(
                            AsymmetricEncryptionController.Decrypt(signature.GetMessage(),
                                passwordDialog.Password, Keychain));
                    }
                    catch (Exception ex) {
                        while (ex is AggregateException) ex = ex.InnerException;
                        if (!(ex is CryptographicException)) throw;
                        MessageBox.Show("Nesprávné heslo.");
                        return;
                    }
                    MessageBox.Show(
                        "Podpis byl úspěšně ověřen a dešifrovaná zpráva bude zobrazena.");
                    textBox_main.Text = result;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ComboBox_actions_SelectedIndexChanged(object sender, EventArgs e) {
        }

        #region insight

        public int P {
            get => (int) numeric_p.Value;
            set => numeric_p.Value = value;
        }

        public int Q {
            get => (int) numeric_q.Value;
            set => numeric_q.Value = value;
        }

        private int _d;

        public int D {
            get => _d;
            set {
                textBox_d.Text = value.ToString();
                _d = value;
            }
        }

        private int _e;

        public int E {
            get => _e;
            set {
                textBox_e.Text = value.ToString();
                _e = value;
            }
        }

        private int _n;

        public int N {
            get => _n;
            set {
                textBox_modulus.Text = value.ToString();
                _n = value;
            }
        }

        public int K {
            set => textBox_k.Text = value.ToString();
        }

        private void NewPrimes() {
            var r = new Random();
            int p = PrimeGenerator.Primes[r.Next(PrimeGenerator.Primes.Length)];
            int q;
            do {
                q = PrimeGenerator.Primes[r.Next(PrimeGenerator.Primes.Length)];
            } while (p == q);

            numeric_p.Value = p;
            numeric_q.Value = q;
        }

        private async Task<bool> Recompute() {
            var errors = new Queue<string>();
            if (!PrimeGenerator.IsPrime(P)) {
                errors.Enqueue("P není prvočíslo.");
                numeric_p.BackColor = Color.PaleVioletRed;
            }
            else {
                numeric_p.BackColor = SystemColors.Window;
            }

            if (!PrimeGenerator.IsPrime(Q)) {
                errors.Enqueue("Q není prvočíslo.");
                numeric_q.BackColor = Color.PaleVioletRed;
            }
            else {
                numeric_q.BackColor = SystemColors.Window;
            }

            if (P == Q) errors.Enqueue("P a Q musí být odlišná.");
            if (errors.Count > 0) {
                label_errors.Text = string.Join("\n", errors);
                return false;
            }

            label_errors.Text = "";

            int phiN = (P - 1) * (Q - 1);
            int n = P * Q;
            int e = -1;
            int[] exponents = {(1 << 4) | 1, (1 << 8) | 1, (1 << 16) | 1};
            foreach (int candidate in exponents)
                if (PrimeGenerator.GreatestCommonDivisor(phiN, candidate) == 1) {
                    e = candidate;
                    break;
                }

            if (e == -1) return false;

            int k =
                await Task.Factory.StartNew(() => {
                    var x = 0;
                    while (true) {
                        if ((1 + x * phiN) % e == 0) return x;
                        x++;
                    }
                });
            int d = (1 + k * phiN) / e;
            E = e;
            D = d;
            N = n;
            numeric_m.Maximum = n - 2;
            K = k;
            return true;
        }

        private void EncryptDecryptM() {
            var m = (int) numeric_m.Value;
            BigInteger c = BigInteger.ModPow(m, E, N);
            textBox_c.Text = c.ToString();
            BigInteger backM = BigInteger.ModPow(c, D, N);
            textBox_backM.Text = backM.ToString();
        }

        private async void But_newPrimes_Click(object sender, EventArgs e) {
            NewPrimes();
            if (await Recompute())
                EncryptDecryptM();
        }

        private async void Numeric_p_ValueChanged(object sender, EventArgs e) {
            if (await Recompute())
                EncryptDecryptM();
        }

        private async void Numeric_q_ValueChanged(object sender, EventArgs e) {
            if (await Recompute())
                EncryptDecryptM();
        }

        private void Numeric_m_ValueChanged(object sender, EventArgs e) {
            EncryptDecryptM();
        }

        private void TabPageInsightKeyGenerator_Click(object sender, EventArgs e) {
        }

        #endregion

        private void TextBox_main_KeyDown(object sender, KeyEventArgs e) {
            if (sender == null) return;
            if (sender is TextBox t && e.Control && e.KeyCode == Keys.A) {
                e.SuppressKeyPress = true;
                t.SelectAll();
            }
        }

        private void but_exportPublic_Click(object sender, EventArgs e) {
            Keypair chosenKeypair = ucKeychain_keychainTab.SelectedItems.FirstOrDefault();
            if (chosenKeypair == null) {
                MessageBox.Show("Vyberte nejdříve klíčový pár.");
                return;
            }

            IArmorProvider armor = new HitaiArmorProvider();
            var result = armor.ToArmor(chosenKeypair.ToPublic().ToMessagePack(), ArmorType.PublicKey);
            Clipboard.SetText(Encoding.UTF8.GetString(result));
            MessageBox.Show("Výsledek byl zkopírován do schránky.");
        }
        #region FormulaHint
        private void ClearFormulaImage() => pictureBox_formula.Image = null;
        private void Label_d_MouseHover(object sender, EventArgs e) => pictureBox_formula.Image = Resources.D;
        private void Label_d_MouseLeave(object sender, EventArgs e) => ClearFormulaImage();
        private void Label_e_MouseHover(object sender, EventArgs e) => pictureBox_formula.Image = Resources.E;
        private void Label_e_MouseLeave(object sender, EventArgs e) => ClearFormulaImage();
        private void Label_n_MouseHover(object sender, EventArgs e) => pictureBox_formula.Image = Resources.N;
        private void Label_n_MouseLeave(object sender, EventArgs e) => ClearFormulaImage();
        private void Label_c_MouseHover(object sender, EventArgs e) => pictureBox_formula.Image = Resources.C;
        private void Label_c_MouseLeave(object sender, EventArgs e) => ClearFormulaImage();
        private void Label_mback_MouseHover(object sender, EventArgs e) => pictureBox_formula.Image = Resources.M;
        private void Label_mback_MouseLeave(object sender, EventArgs e) => ClearFormulaImage();
        #endregion

        private void Link_repo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            Process.Start(@"https://github.com/sorashi/hitai");

        private void Link_readme_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            Process.Start(@"https://github.com/sorashi/hitai/blob/master/README.md");

        private void Link_license_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            Process.Start(@"https://github.com/sorashi/hitai/blob/master/LICENSE.txt");
    }
}
