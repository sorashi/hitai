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
    public partial class CreatePasswordDialog : Form
    {
        public CreatePasswordDialog() {
            InitializeComponent();
            label_confirmation.Text = "";
            label_strength.Text = "";
        }
        public string Password { get; set; }
        private void textBox_password_TextChanged(object sender, EventArgs e) {
            if(RateStrength(textBox_password.Text)) {
                label_strength.Text = "Heslo je dostatečně silné.";
                label_strength.ForeColor = Color.Green;
            }else {
                label_strength.Text = "Heslo musí obsahovat číslici, velké a malé písmeno a musí být nejméně 8 znaků dlouhé.";
                label_strength.ForeColor = Color.Red;
            }
        }

        public bool RateStrength(string password) {
            if (string.IsNullOrEmpty(password)) return false;
            // must contain a letter, a capital letter and a number
            // must be at least 8 characters long
            bool containsLowerCase = password.Any(char.IsLower);
            bool containsUpperCase = password.Any(char.IsUpper);
            bool containsDigit = password.Any(char.IsDigit);
            bool longEnough = password.Length >= 8;
            return containsLowerCase && containsUpperCase && containsDigit && longEnough;
        }

        private void textBox_confirm_TextChanged(object sender, EventArgs e) {
            if(textBox_password.Text == textBox_confirm.Text) {
                label_confirmation.Text = "Hesla se shodují.";
                label_confirmation.ForeColor = Color.Green;
            }
            else {
                label_confirmation.Text = "Hesla se neshodují.";
                label_confirmation.ForeColor = Color.Red;
            }
        }

        private void butCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void butOk_Click(object sender, EventArgs e) {
            if(textBox_password.Text != textBox_confirm.Text || !RateStrength(textBox_password.Text)) {
                MessageBox.Show("Prosím zkontrolujte chybové hlášky. Něco s Vaším heslem není v pořádku.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            Password = textBox_password.Text;
            this.Close();
        }

        private void label_confirmation_Click(object sender, EventArgs e) {

        }

        private void label_strength_Click(object sender, EventArgs e) {

        }
    }
}
