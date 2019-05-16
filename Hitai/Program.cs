using System;
using System.Threading;
using System.Windows.Forms;

namespace Hitai
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new FormMain());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            ShowExceptionDialog(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender,
            UnhandledExceptionEventArgs e) {
            ShowExceptionDialog(e.ExceptionObject as Exception);
        }

        private static void ShowExceptionDialog(Exception ex) {
            if (ex == null) throw new ArgumentNullException(nameof(ex));
            while (ex is AggregateException) ex = ex.InnerException;
            var erd = new Dialogs.ExceptionReportDialog(ex);
            erd.ShowDialog();
        }
    }
}
