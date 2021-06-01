using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form2 start = new Form2();
            start.ShowDialog();

           /* Form1 main = new Form1();
            main.ShowDialog();*/

          //  if (!Form2.cerrado)
             Application.Run(new Form1());  
        }
    }
}
