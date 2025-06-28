using System.Windows;
using System.Windows.Controls;

namespace SimulationApp.UI {
    public partial class StrategyPanel : UserControl {
        public StrategyPanel() {
            InitializeComponent();
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Simulation started with selected strategy.");
        }
    }
}