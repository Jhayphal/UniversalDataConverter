using System;
using System.Windows.Forms;

namespace QueryRunner
{
	static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            if (argv == null || argv.Length != 1 || string.IsNullOrWhiteSpace(argv[0]))
            {
                if (argv != null && argv.Length != 1)
                    MessageBox.Show("Перетащите только ОДИН файл!");
                else
                    MessageBox.Show("Не указан скрипт. Перетащите файл скрипта на этот экзешник для запуска.");

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(argv[0]));
        }
    }
}
