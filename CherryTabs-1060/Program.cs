using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BreakTheBankTabs1063
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Reset starting search directory
                GameCore.Utility.FileSearch.SetStartingSearchDirectory("");
                using (Bingo app = new Bingo(args))
                {
                    Application.DoEvents();
                    if (app._Load() == false)
                    {
                        MessageBox.Show("ERROR LOADING GAME\n\rAPPLICATION NOW CLOSING!!!\n\rCONTACT TECH", "ERROR");
                        return;
                    }

                    Bingo.singleton.logError(app.appTitle + " Loaded & Beginning to Run");
                    Application.Run(app);

                    app.Close();
                }
            }
            catch (Exception ex)
            {
                Bingo.singleton.logError("\n\r=====================\n\rUnhandled Error\n\r" + ex.ToString());
                MessageBox.Show("An error occured and this application needs to close: " + ex.ToString());
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}