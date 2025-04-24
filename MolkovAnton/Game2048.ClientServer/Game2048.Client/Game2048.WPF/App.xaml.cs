using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Game2048.Common.Infrastructure;
using Ninject;

namespace Game2048.WPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeNinject();
        }

        private void InitializeNinject()
        {
            NinjectKernel.Instance = new StandardKernel(new Game2048Module());
        }
    }
}
