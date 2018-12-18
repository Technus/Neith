using Neith.com.dgs.dapc.neith.mongo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleError);
            try
            {
                throw new Exception("I like cake!");
            }
            catch(Exception e)
            {
                HandleError(e, null);
            }
        }

        public static void HandleError(Object sender, UnhandledExceptionEventArgs e)
        {
            new ThrowableLog((Exception)sender);
        }
    }
}
