using NeithCore.iss;
using NeithCore.MongoDB.poco;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Neith
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CultureInfo locale=new CultureInfo("en-US");

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleError);
            Debug.Print(string.Join(" ",OpenLayers.Base.DeviceMgr.Get().GetDeviceNames()));

            var iss = UsbISS.GetAttachedISS();
            List<string> names = new List<string>();
            foreach(var entry in iss) {
                names.Add(entry.Key);
            }
            Debug.Print(string.Join(" ", names));
        }

        public static void HandleError(Object sender, UnhandledExceptionEventArgs e)
        {
            Debug.Print(sender.ToString());
            new ThrowableLog((Exception)sender);
        }
    }
}
