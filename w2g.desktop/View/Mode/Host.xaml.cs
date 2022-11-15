using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace w2g.desktop.View.Mode
{
    /// <summary>
    /// Interaktionslogik für Host.xaml
    /// </summary>
    public partial class Host : Window
    {
        public Host()
        {
            InitializeComponent();
        }

        private void OnApplicationExit(object sender, MouseButtonEventArgs e) =>
            Environment.Exit(0);

        private void OnWindowDragged(object sender, MouseEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}
