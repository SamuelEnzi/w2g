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

namespace w2g.desktop.View
{
    /// <summary>
    /// Interaktionslogik für Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        public UseMode UseMode { get; set; } = UseMode.Connect;

        public Setup()
        {
            InitializeComponent();
            UseMode = UseMode.Connect;
            UI_Content.Content = new Pages.Connect();
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

        public UseMode Display()
        {
            this.ShowDialog();
            return UseMode;
        }

        private void OnConnectClicked(object sender, MouseButtonEventArgs e)
        {
            UseMode = UseMode.Connect;
            UI_Content.Content = new Pages.Connect();
        }

        private void OnHostClicked(object sender, MouseButtonEventArgs e)
        {
            UseMode = UseMode.Host;
            UI_Content.Content = new Pages.Host();
        }

        private void OnNextClicked(object sender, MouseButtonEventArgs e) =>
            this.Close();
    }

    public enum UseMode
    {
        Connect,
        Host
    }
}
