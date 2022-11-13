using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using YoutubeExplode;
using w2g.core;

namespace w2g.desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private double? position;

        public double? PositionTime { get => position; set
            { 
                position = value; 
                NotifyPropertyChanged();
                UI_Video.Position = TimeSpan.FromSeconds(UI_Position.Value);
            }
        }

        public string Time
        {
            get
            {
                if (PositionTime == null)
                    return "00:00:00";
                var time = TimeSpan.FromSeconds((double)PositionTime!);
                return time.ToString();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Tick;
            Timer.Start();

            Action load = async () =>
            {
                var url = await Helper.ConvertToStream("https://www.youtube.com/watch?v=R_FQU4KzN7A");
                Dispatcher.Invoke(() =>
                {
                    UI_Video.Source = new Uri(url.Item1);
                    UI_Position.Minimum = 0;
                    UI_Position.Maximum = url.Item2.Value.TotalSeconds;
                });
            };
            load.Invoke();
        }

        private void Tick(object? sender, EventArgs e)
        {
            if (UI_Position.IsMouseOver)
                return;
            position = UI_Video.Position.TotalSeconds;
            UI_Position.Value = (double)position;
            NotifyPropertyChanged("Time");
        }

        private void UI_Play_Click(object sender, RoutedEventArgs e)
        {
            UI_Video.Play();
        }

        private void UI_Pause_Click(object sender, RoutedEventArgs e)
        {
            UI_Video.Pause();
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UI_Position_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.PositionTime = UI_Position.Value;

        }
    }
}
