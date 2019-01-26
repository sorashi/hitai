namespace Hitai
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.comboBox_actions = new System.Windows.Forms.ComboBox();
            this.butProvest = new System.Windows.Forms.Button();
            this.textBox_main = new System.Windows.Forms.TextBox();
            this.ucKeychain_mainTab = new Hitai.UserControlKeychain();
            this.tabPageKeychain = new System.Windows.Forms.TabPage();
            this.buttonDeleteKey = new System.Windows.Forms.Button();
            this.buttonGenerateNewPair = new System.Windows.Forms.Button();
            this.buttonAddPublicKey = new System.Windows.Forms.Button();
            this.ucKeychain_keychainTab = new Hitai.UserControlKeychain();
            this.tabPageInsight = new System.Windows.Forms.TabPage();
            this.textBox_backM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_c = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label_m = new System.Windows.Forms.Label();
            this.numeric_m = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_k = new System.Windows.Forms.TextBox();
            this.textBox_modulus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_d = new System.Windows.Forms.Label();
            this.textBox_d = new System.Windows.Forms.TextBox();
            this.label_e = new System.Windows.Forms.Label();
            this.textBox_e = new System.Windows.Forms.TextBox();
            this.label_errors = new System.Windows.Forms.Label();
            this.but_newPrimes = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numeric_q = new System.Windows.Forms.NumericUpDown();
            this.numeric_p = new System.Windows.Forms.NumericUpDown();
            this.but_exportPublic = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageKeychain.SuspendLayout();
            this.tabPageInsight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_q)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_p)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMain);
            this.tabControl1.Controls.Add(this.tabPageKeychain);
            this.tabControl1.Controls.Add(this.tabPageInsight);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.comboBox_actions);
            this.tabPageMain.Controls.Add(this.butProvest);
            this.tabPageMain.Controls.Add(this.textBox_main);
            this.tabPageMain.Controls.Add(this.ucKeychain_mainTab);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(459, 396);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Hlavní okno";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // comboBox_actions
            // 
            this.comboBox_actions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_actions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_actions.FormattingEnabled = true;
            this.comboBox_actions.Items.AddRange(new object[] {
            "Šifrovat",
            "Dešifrovat",
            "Podepsat",
            "Ověřit",
            "Šifrovat a podepsat",
            "Dešifrovat a ověřit"});
            this.comboBox_actions.Location = new System.Drawing.Point(8, 228);
            this.comboBox_actions.Name = "comboBox_actions";
            this.comboBox_actions.Size = new System.Drawing.Size(368, 21);
            this.comboBox_actions.TabIndex = 4;
            this.comboBox_actions.SelectedIndexChanged += new System.EventHandler(this.ComboBox_actions_SelectedIndexChanged);
            // 
            // butProvest
            // 
            this.butProvest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butProvest.Location = new System.Drawing.Point(382, 226);
            this.butProvest.Name = "butProvest";
            this.butProvest.Size = new System.Drawing.Size(75, 23);
            this.butProvest.TabIndex = 2;
            this.butProvest.Text = "Provést";
            this.butProvest.UseVisualStyleBackColor = true;
            this.butProvest.Click += new System.EventHandler(this.butProvest_Click);
            // 
            // textBox_main
            // 
            this.textBox_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_main.Location = new System.Drawing.Point(8, 6);
            this.textBox_main.Multiline = true;
            this.textBox_main.Name = "textBox_main";
            this.textBox_main.Size = new System.Drawing.Size(449, 214);
            this.textBox_main.TabIndex = 1;
            this.textBox_main.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_main_KeyDown);
            // 
            // ucKeychain_mainTab
            // 
            this.ucKeychain_mainTab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeychain_mainTab.Location = new System.Drawing.Point(3, 286);
            this.ucKeychain_mainTab.Name = "ucKeychain_mainTab";
            this.ucKeychain_mainTab.Size = new System.Drawing.Size(453, 107);
            this.ucKeychain_mainTab.TabIndex = 0;
            // 
            // tabPageKeychain
            // 
            this.tabPageKeychain.Controls.Add(this.but_exportPublic);
            this.tabPageKeychain.Controls.Add(this.buttonDeleteKey);
            this.tabPageKeychain.Controls.Add(this.buttonGenerateNewPair);
            this.tabPageKeychain.Controls.Add(this.buttonAddPublicKey);
            this.tabPageKeychain.Controls.Add(this.ucKeychain_keychainTab);
            this.tabPageKeychain.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeychain.Name = "tabPageKeychain";
            this.tabPageKeychain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeychain.Size = new System.Drawing.Size(459, 396);
            this.tabPageKeychain.TabIndex = 1;
            this.tabPageKeychain.Text = "Klíčenka";
            this.tabPageKeychain.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteKey
            // 
            this.buttonDeleteKey.Location = new System.Drawing.Point(253, 7);
            this.buttonDeleteKey.Name = "buttonDeleteKey";
            this.buttonDeleteKey.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteKey.TabIndex = 3;
            this.buttonDeleteKey.Text = "Smazat";
            this.buttonDeleteKey.UseVisualStyleBackColor = true;
            this.buttonDeleteKey.Click += new System.EventHandler(this.buttonDeleteKey_Click);
            // 
            // buttonGenerateNewPair
            // 
            this.buttonGenerateNewPair.Location = new System.Drawing.Point(121, 7);
            this.buttonGenerateNewPair.Name = "buttonGenerateNewPair";
            this.buttonGenerateNewPair.Size = new System.Drawing.Size(126, 23);
            this.buttonGenerateNewPair.TabIndex = 2;
            this.buttonGenerateNewPair.Text = "Vygenerovat nový pár";
            this.buttonGenerateNewPair.UseVisualStyleBackColor = true;
            this.buttonGenerateNewPair.Click += new System.EventHandler(this.buttonGenerateNewPair_Click);
            // 
            // buttonAddPublicKey
            // 
            this.buttonAddPublicKey.Location = new System.Drawing.Point(6, 7);
            this.buttonAddPublicKey.Name = "buttonAddPublicKey";
            this.buttonAddPublicKey.Size = new System.Drawing.Size(109, 23);
            this.buttonAddPublicKey.TabIndex = 1;
            this.buttonAddPublicKey.Text = "Přidat veřejný klíč";
            this.buttonAddPublicKey.UseVisualStyleBackColor = true;
            this.buttonAddPublicKey.Click += new System.EventHandler(this.buttonAddPublicKey_Click);
            // 
            // ucKeychain_keychainTab
            // 
            this.ucKeychain_keychainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucKeychain_keychainTab.Location = new System.Drawing.Point(0, 36);
            this.ucKeychain_keychainTab.Name = "ucKeychain_keychainTab";
            this.ucKeychain_keychainTab.Size = new System.Drawing.Size(465, 329);
            this.ucKeychain_keychainTab.TabIndex = 0;
            // 
            // tabPageInsight
            // 
            this.tabPageInsight.Controls.Add(this.textBox_backM);
            this.tabPageInsight.Controls.Add(this.label6);
            this.tabPageInsight.Controls.Add(this.textBox_c);
            this.tabPageInsight.Controls.Add(this.label5);
            this.tabPageInsight.Controls.Add(this.label_m);
            this.tabPageInsight.Controls.Add(this.numeric_m);
            this.tabPageInsight.Controls.Add(this.label4);
            this.tabPageInsight.Controls.Add(this.textBox_k);
            this.tabPageInsight.Controls.Add(this.textBox_modulus);
            this.tabPageInsight.Controls.Add(this.label3);
            this.tabPageInsight.Controls.Add(this.label_d);
            this.tabPageInsight.Controls.Add(this.textBox_d);
            this.tabPageInsight.Controls.Add(this.label_e);
            this.tabPageInsight.Controls.Add(this.textBox_e);
            this.tabPageInsight.Controls.Add(this.label_errors);
            this.tabPageInsight.Controls.Add(this.but_newPrimes);
            this.tabPageInsight.Controls.Add(this.label2);
            this.tabPageInsight.Controls.Add(this.label1);
            this.tabPageInsight.Controls.Add(this.numeric_q);
            this.tabPageInsight.Controls.Add(this.numeric_p);
            this.tabPageInsight.Location = new System.Drawing.Point(4, 22);
            this.tabPageInsight.Name = "tabPageInsight";
            this.tabPageInsight.Size = new System.Drawing.Size(459, 396);
            this.tabPageInsight.TabIndex = 2;
            this.tabPageInsight.Text = "Vhled";
            this.tabPageInsight.UseVisualStyleBackColor = true;
            // 
            // textBox_backM
            // 
            this.textBox_backM.Location = new System.Drawing.Point(263, 197);
            this.textBox_backM.Name = "textBox_backM";
            this.textBox_backM.ReadOnly = true;
            this.textBox_backM.Size = new System.Drawing.Size(120, 20);
            this.textBox_backM.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Zpětně vypočítané M = C^D mod N";
            // 
            // textBox_c
            // 
            this.textBox_c.Location = new System.Drawing.Point(145, 197);
            this.textBox_c.Name = "textBox_c";
            this.textBox_c.ReadOnly = true;
            this.textBox_c.Size = new System.Drawing.Size(109, 20);
            this.textBox_c.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Šifra C = M^E mod N";
            // 
            // label_m
            // 
            this.label_m.AutoSize = true;
            this.label_m.Location = new System.Drawing.Point(11, 181);
            this.label_m.Name = "label_m";
            this.label_m.Size = new System.Drawing.Size(101, 13);
            this.label_m.TabIndex = 37;
            this.label_m.Text = "Zpráva M (max N-2)";
            // 
            // numeric_m
            // 
            this.numeric_m.Location = new System.Drawing.Point(11, 197);
            this.numeric_m.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_m.Name = "numeric_m";
            this.numeric_m.Size = new System.Drawing.Size(120, 20);
            this.numeric_m.TabIndex = 36;
            this.numeric_m.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_m.ValueChanged += new System.EventHandler(this.Numeric_m_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "(Koeficient K)";
            // 
            // textBox_k
            // 
            this.textBox_k.Location = new System.Drawing.Point(344, 104);
            this.textBox_k.Name = "textBox_k";
            this.textBox_k.ReadOnly = true;
            this.textBox_k.Size = new System.Drawing.Size(100, 20);
            this.textBox_k.TabIndex = 34;
            // 
            // textBox_modulus
            // 
            this.textBox_modulus.Location = new System.Drawing.Point(11, 143);
            this.textBox_modulus.Name = "textBox_modulus";
            this.textBox_modulus.ReadOnly = true;
            this.textBox_modulus.Size = new System.Drawing.Size(327, 20);
            this.textBox_modulus.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Modulus N";
            // 
            // label_d
            // 
            this.label_d.AutoSize = true;
            this.label_d.Location = new System.Drawing.Point(175, 85);
            this.label_d.Name = "label_d";
            this.label_d.Size = new System.Drawing.Size(112, 13);
            this.label_d.TabIndex = 31;
            this.label_d.Text = "Soukromý exponent D";
            // 
            // textBox_d
            // 
            this.textBox_d.Location = new System.Drawing.Point(178, 104);
            this.textBox_d.Name = "textBox_d";
            this.textBox_d.ReadOnly = true;
            this.textBox_d.Size = new System.Drawing.Size(160, 20);
            this.textBox_d.TabIndex = 30;
            // 
            // label_e
            // 
            this.label_e.AutoSize = true;
            this.label_e.Location = new System.Drawing.Point(11, 85);
            this.label_e.Name = "label_e";
            this.label_e.Size = new System.Drawing.Size(100, 13);
            this.label_e.TabIndex = 29;
            this.label_e.Text = "Veřejný exponent E";
            // 
            // textBox_e
            // 
            this.textBox_e.Location = new System.Drawing.Point(11, 104);
            this.textBox_e.Name = "textBox_e";
            this.textBox_e.ReadOnly = true;
            this.textBox_e.Size = new System.Drawing.Size(160, 20);
            this.textBox_e.TabIndex = 28;
            // 
            // label_errors
            // 
            this.label_errors.ForeColor = System.Drawing.Color.Maroon;
            this.label_errors.Location = new System.Drawing.Point(260, 11);
            this.label_errors.Name = "label_errors";
            this.label_errors.Size = new System.Drawing.Size(193, 65);
            this.label_errors.TabIndex = 27;
            this.label_errors.Text = "{{chyby}}";
            // 
            // but_newPrimes
            // 
            this.but_newPrimes.Location = new System.Drawing.Point(11, 53);
            this.but_newPrimes.Name = "but_newPrimes";
            this.but_newPrimes.Size = new System.Drawing.Size(105, 23);
            this.but_newPrimes.TabIndex = 26;
            this.but_newPrimes.Text = "Nová prvočísla";
            this.but_newPrimes.UseVisualStyleBackColor = true;
            this.but_newPrimes.Click += new System.EventHandler(this.But_newPrimes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "prvočíslo Q";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "prvočíslo P";
            // 
            // numeric_q
            // 
            this.numeric_q.Location = new System.Drawing.Point(134, 27);
            this.numeric_q.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numeric_q.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_q.Name = "numeric_q";
            this.numeric_q.Size = new System.Drawing.Size(120, 20);
            this.numeric_q.TabIndex = 23;
            this.numeric_q.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_q.ValueChanged += new System.EventHandler(this.Numeric_q_ValueChanged);
            // 
            // numeric_p
            // 
            this.numeric_p.Location = new System.Drawing.Point(8, 27);
            this.numeric_p.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numeric_p.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_p.Name = "numeric_p";
            this.numeric_p.Size = new System.Drawing.Size(120, 20);
            this.numeric_p.TabIndex = 22;
            this.numeric_p.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_p.ValueChanged += new System.EventHandler(this.Numeric_p_ValueChanged);
            // 
            // but_exportPublic
            // 
            this.but_exportPublic.Location = new System.Drawing.Point(334, 7);
            this.but_exportPublic.Name = "but_exportPublic";
            this.but_exportPublic.Size = new System.Drawing.Size(117, 23);
            this.but_exportPublic.TabIndex = 4;
            this.but_exportPublic.Text = "Exportovat veřejný";
            this.but_exportPublic.UseVisualStyleBackColor = true;
            this.but_exportPublic.Click += new System.EventHandler(this.but_exportPublic_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(467, 422);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "FormMain";
            this.Text = "Hitai";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.tabPageKeychain.ResumeLayout(false);
            this.tabPageInsight.ResumeLayout(false);
            this.tabPageInsight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_q)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_p)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageKeychain;
        private System.Windows.Forms.TabPage tabPageInsight;
        private UserControlKeychain ucKeychain_keychainTab;
        private System.Windows.Forms.Button buttonDeleteKey;
        private System.Windows.Forms.Button buttonGenerateNewPair;
        private System.Windows.Forms.Button buttonAddPublicKey;
        private System.Windows.Forms.ComboBox comboBox_actions;
        private System.Windows.Forms.Button butProvest;
        private System.Windows.Forms.TextBox textBox_main;
        private UserControlKeychain ucKeychain_mainTab;
        private System.Windows.Forms.TextBox textBox_backM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_c;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_m;
        private System.Windows.Forms.NumericUpDown numeric_m;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_k;
        private System.Windows.Forms.TextBox textBox_modulus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_d;
        private System.Windows.Forms.TextBox textBox_d;
        private System.Windows.Forms.Label label_e;
        private System.Windows.Forms.TextBox textBox_e;
        private System.Windows.Forms.Label label_errors;
        private System.Windows.Forms.Button but_newPrimes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numeric_q;
        private System.Windows.Forms.NumericUpDown numeric_p;
        private System.Windows.Forms.Button but_exportPublic;
    }
}

