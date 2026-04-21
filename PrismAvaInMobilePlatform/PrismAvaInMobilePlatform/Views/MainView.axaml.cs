using Avalonia.Controls;

namespace PrismAvaInMobilePlatform.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainViewModel();
        }
    }
}