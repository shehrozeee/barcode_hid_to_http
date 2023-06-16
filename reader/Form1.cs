using GlobalLowLevelHooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GlobalLowLevelHooks.KeyboardHook keyboardHook = new GlobalLowLevelHooks.KeyboardHook();
            keyboardHook.Install();

            keyboardHook.KeyDown += KeyboardHook_KeyDown;

        }

        private StringBuilder barcodeBuilder = new StringBuilder();
        void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key != KeyboardHook.VKeys.RETURN)
            {
                char key_code = ((char)key);
                Console.Write(key_code.ToString().Trim());
                barcodeBuilder.Append(key_code.ToString().Trim());
            }
            else
            {

                barcodeBuilder.Clear();
                //Console.WriteLine();
                //Console.WriteLine("GotBarcode");
            }
        }
    }
}
