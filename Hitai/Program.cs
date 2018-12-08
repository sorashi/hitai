using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitai
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new FormMain());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            ShowExceptionDialog(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            ShowExceptionDialog(e.ExceptionObject as Exception); 
        }

        private static void ShowExceptionDialog(Exception ex) {
            while (ex is AggregateException) ex = ex.InnerException;
            // todo vlastní dialog
            MessageBox.Show(
                $"V aplikaci nastala chyba, a proto se nyní z této chyby pokusí zotavit. To může mít za následek neočekávané chování.\nZpráva chyby: {ex.Message}\nStack trace: {ex.StackTrace}",
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
