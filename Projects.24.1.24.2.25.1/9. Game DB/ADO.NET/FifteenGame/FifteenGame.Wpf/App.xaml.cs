using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FifteenGame.DataAccess.DatabaseInitializer.Initialize();

            CreateNinjectKernel();
        }

        private void CreateNinjectKernel()
        {
            NinjectKernel.Instance = new StandardKernel(new FifteenGameModule());
        }
    }
}
