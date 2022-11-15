using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace w2g.desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var setup = new View.Setup();

            View.UseMode useMode = setup.Display();

            switch (useMode)
            {
                case View.UseMode.Connect:
                    new View.Mode.Connect().ShowDialog();
                    break;
                case View.UseMode.Host:
                    new View.Mode.Host().ShowDialog();
                    break;
                default:
                    break;
            }

            Environment.Exit(0);
        }
    }
}
