using System.Windows;

namespace SimulationApp.UI {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void OnExitClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}