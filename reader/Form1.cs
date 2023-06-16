using GlobalLowLevelHooks;
using RestSharp;
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
        List<string> BarcodesToSend = new List<string>();
        private StringBuilder barcodeBuilder = new StringBuilder();
        string protocal = "http";
        string server = "localhost";
        string port = "5137";
        string endpoint = "/api/Barcode/SubmitBarcode/";
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
                string barcode = barcodeBuilder.ToString();
                barcodeBuilder.Clear();
                BarcodesToSend.Add(barcode);
                foreach (string code in BarcodesToSend)
                { 
                    var client = new RestClient($"{protocal}://{server}:{port}{endpoint}{barcode}");
                    var request = new RestRequest();
                    request.Method = Method.Get;
                    request.AddHeader("accept", "text/plain");
                    var response = client.Execute(request);
                    var success = response.Content == "OK";
                    if (success)
                    {
                        BarcodesToSend.Remove(code);
                    }
                    else
                    {
                        break;
                    }
                }
                //Console.WriteLine();
                //Console.WriteLine("GotBarcode");
            }
        }
    }
}
