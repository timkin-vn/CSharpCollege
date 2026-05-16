using Ninject;
using System;
using System.Windows;

namespace FifteenGame.Wpf.Infrastructure
{
    public static class NinjectKernel
    {
        private static IKernel _instance;

        public static IKernel Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new StandardKernel(new FifteenGameModule());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("CRITICAL NINJECT ERROR: " + ex.ToString());
                        throw;
                    }
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
    }
}