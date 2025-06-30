using System;
using System.Windows;
using System.Windows.Controls;

namespace SimulationApp.UI {
    public partial class StrategyPanel : UserControl {

        public event EventHandler? StartRequested;

        public StrategyPanel() {
            InitializeComponent();
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e) {
            StartRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}