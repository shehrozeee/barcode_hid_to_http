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

        void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key != KeyboardHook.VKeys.RETURN)
            {
                char key_code = ((char)key);
                Console.Write(key_code.ToString().Trim());
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("GotBarcode");
            }
        }
    }
}
