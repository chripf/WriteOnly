using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WriteOnly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _Path;
        private Timer _TopMostTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _Path = OpenFileDialog();

            _TopMostTimer = new Timer(100);
            _TopMostTimer.Elapsed += Timer_Elapsed;
            _TopMostTimer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _TopMostTimer.Stop();
            _TopMostTimer.Dispose();
            _TopMostTimer = null;

            base.OnClosing(e);
        }


        private string OpenFileDialog()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = Environment.UserName; // Default file name
            dlg.DefaultExt = ".test.txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();


            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => this.Activate());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText(_Path, tbText.Text);

            Application.Current.Shutdown();
        }

    }
}
