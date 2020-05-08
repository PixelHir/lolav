using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace LolAV
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new IconPick());
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/PixelHir/lolav"))
                {
                    await mgr.Result.UpdateApp();
                }
        }
    }
}
