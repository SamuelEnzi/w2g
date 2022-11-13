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

namespace w2g.desktop
{
    /// <summary>
    /// Interaktionslogik für InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        Action<string> callback;
        public InputWindow(Action<string> callback)
        {
            InitializeComponent();
            this.Show();
            this.callback = callback;
        }

        private void UI_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            UI_Submit.IsEnabled = UI_Input.Text != "";
        }

        private void UI_Submit_Click(object sender, RoutedEventArgs e)
        {
            callback?.Invoke(UI_Input.Text);
            Close();
        }
    }
}
