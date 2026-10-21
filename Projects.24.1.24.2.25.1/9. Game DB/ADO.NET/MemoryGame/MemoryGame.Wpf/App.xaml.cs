using MemoryGame.Common.Infrastructure;
using MemoryGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryGame.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CreateNinjectKernel();
        }

        private void CreateNinjectKernel()
        {
            NinjectKernel.Instance = new StandardKernel(new MemoryGameModule());
        }
    }
}
