namespace Hitai.Dialogs
{
    partial class ExceptionReportDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_stacktrace = new System.Windows.Forms.TextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.but_report = new System.Windows.Forms.Button();
            this.but_dontReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "V aplikaci nastala chyba, a proto se nyní z této chyby pokusí zotavit. To může mí" +
    "t za následek neočekávané chování.\r\n";
            // 
            // textBox_stacktrace
            // 
            this.textBox_stacktrace.Location = new System.Drawing.Point(12, 136);
            this.textBox_stacktrace.Multiline = true;
            this.textBox_stacktrace.Name = "textBox_stacktrace";
            this.textBox_stacktrace.ReadOnly = true;
            this.textBox_stacktrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_stacktrace.Size = new System.Drawing.Size(381, 290);
            this.textBox_stacktrace.TabIndex = 1;
            this.textBox_stacktrace.WordWrap = false;
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(12, 63);
            this.textBox_message.Multiline = true;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.ReadOnly = true;
            this.textBox_message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_message.Size = new System.Drawing.Size(381, 50);
            this.textBox_message.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zpráva chyby:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Stacktrace:";
            // 
            // but_report
            // 
            this.but_report.Location = new System.Drawing.Point(307, 432);
            this.but_report.Name = "but_report";
            this.but_report.Size = new System.Drawing.Size(86, 23);
            this.but_report.TabIndex = 5;
            this.but_report.Text = "Nahlásit chybu";
            this.but_report.UseVisualStyleBackColor = true;
            this.but_report.Click += new System.EventHandler(this.But_report_Click);
            // 
            // but_dontReport
            // 
            this.but_dontReport.Location = new System.Drawing.Point(217, 432);
            this.but_dontReport.Name = "but_dontReport";
            this.but_dontReport.Size = new System.Drawing.Size(84, 23);
            this.but_dontReport.TabIndex = 6;
            this.but_dontReport.Text = "Nenahlašovat";
            this.but_dontReport.UseVisualStyleBackColor = true;
            this.but_dontReport.Click += new System.EventHandler(this.But_dontReport_Click);
            // 
            // ExceptionReportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 467);
            this.Controls.Add(this.but_dontReport);
            this.Controls.Add(this.but_report);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.textBox_stacktrace);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ExceptionReportDialog";
            this.Text = "Hlášení chyby";
            this.Load += new System.EventHandler(this.ExceptionReportDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_stacktrace;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button but_report;
        private System.Windows.Forms.Button but_dontReport;
    }
}