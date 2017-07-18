using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarframeWorldStateTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //WorldStateData wsdata;
            //bool update = true;

            Form1 form;

            if (args.Length > 0)    // for debugging
            {
                form = new Form1(args[0]);
            }
            else
            {
                form = new Form1();
            }

            //Form1 form = new Form1(wsdata, update);
            //Form2 form2 = new Form2(wsdata);  // not needed since a Form2 is declared on Form1_Load

            Application.Run(form);
        }
    }
}
