using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Hitai.Dialogs
{
    public partial class ExceptionReportDialog : Form
    {
        public Exception Exception { get; }
        public ExceptionReportDialog(Exception ex) {
            InitializeComponent();
            while (ex is AggregateException) ex = ex.InnerException;
            Exception = ex;
        }

        private void ExceptionReportDialog_Load(object sender, EventArgs e) {
            Icon = SystemIcons.Error;
            textBox_message.Text = Exception.Message;
            textBox_stacktrace.Text = Exception.StackTrace;
        }

        private void But_report_Click(object sender, EventArgs e) {
            Process.Start(BuildReportUrl());
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void But_dontReport_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string BuildReportUrl() {
            var baseUrl = "https://github.com/sorashi/hitai/issues/new";
            var body = new StringBuilder();
            body.AppendLine("### Informace");
            body.AppendLine($"- **Exception type:** `{Exception.GetType().FullName}`");
            body.AppendLine($"- **Message:** {Exception.Message}");
            body.AppendLine("### Stacktrace");
            body.AppendLine("```");
            body.AppendLine(Exception.StackTrace.Replace('`', '\''));
            body.AppendLine("```");
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("### Kroky k reprodukci chyby");
            body.AppendLine("Zde uveďte kroky, které vedly k výskytu chyby (pokud je to možné).");
            body.AppendLine("1. kliknout na tlačítko");
            body.AppendLine("2. chyba");
            body.AppendLine();
            body.AppendLine("Prosím poskytněte co nejvíce informací. Každá maličkost " +
                "může urychlit nalezení a opravu chyby. Zároveň tím zkrátíte případnou korespondenci, " +
                "kdy se zde vývojáři budou ptát na potřebné informace.");
            body.AppendLine();
            body.AppendLine("Před odesláním prosím zkontrolujte, že automaticky " +
                "vygenerované hlášení neobsahuje žádné citlivé informace, které byste neradi " +
                "sdíleli veřejně na internetu, a případně je cenzurujte.");
            var labels = HttpUtility.UrlEncode("exception report");
            var title = HttpUtility.UrlEncode($"{Exception.GetType().Name}: {Exception.Message}");
            var bodyEncoded = HttpUtility.UrlEncode(body.ToString());
            return $"{baseUrl}?labels={labels}&title={title}&body={bodyEncoded}";
        }
    }
}
