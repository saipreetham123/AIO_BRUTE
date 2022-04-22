using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    class AutoSave
    {
        public static void save()
        {
            Task.Factory.StartNew(delegate
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        Task.Factory.StartNew(delegate
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.S)
                            {
                                Program.lines_in_use = false;
                                
                                File.AppendAllLines(Program.PathResults + "Remaining.txt", Program.cloneCombo);
                                int num1 = (int)System.Windows.MessageBox.Show("Saved Remaining", "DIVINE AIO", (MessageBoxButton)(MessageBoxButtons)MessageBoxButton.OK, (MessageBoxImage)(MessageBoxIcon)MessageBoxImage.Asterisk);
                                Process.GetCurrentProcess().Kill();
                            }
                        });
                    }
                }
            });
        }
        public static void manualsave()
        {
            Program.lines_in_use = false;
            File.AppendAllLines(Program.PathResults + "Remaining.txt", Program.cloneCombo);
            Process.GetCurrentProcess().Kill();
        }
    }
}
