using CardFile.Business.Infrastructure;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Windows;

namespace CardFile.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MapperInitialization.PreRegister();
            Mapping.Initialize();
        }
    }
}

