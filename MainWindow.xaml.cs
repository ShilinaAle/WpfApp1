using System.Linq;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //ViewModel viewModel;
            InitializeComponent();
            //DataContext = viewModel = new ViewModel();
            ViewModel vm = new ViewModel(this);
            vm.MainViewModel();
        }

    }
}
