using NeithDevices.iss;
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
using System.Reflection;
using NeithDevices;

namespace NeithTester.ui
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
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleUnhandledError);
            TemporaryClass.Run();

            Application.Current.Shutdown();
        }

        public static void HandleUnhandledError(Object sender, UnhandledExceptionEventArgs e)
        {
            HandleError(sender, e);
            Application.Current.Shutdown();
        }

        public static void HandleError(Object sender, UnhandledExceptionEventArgs e)
        {
            Debug.Print(sender.ToString());
            new ThrowableLog((Exception)sender);
        }
    }
}
