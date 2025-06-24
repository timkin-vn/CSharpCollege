using GpxDataShow.Business.Services;
using GpxDataShow.Data.Services;
using GpxDataShow.ViewModels;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GpxDataShow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        private TrackService _trackService = new TrackService();

        private TrackViewModel _viewModel = new TrackViewModel();
        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "*.gpx|*.gpx|*.*|*.*" };
            if (ofd.ShowDialog() != true)
            {
                return;
            }

            _viewModel.LoadFromFile(ofd.FileName);
        }

        private void TrackData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
