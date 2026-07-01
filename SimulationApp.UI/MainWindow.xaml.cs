using SimulationApp.Core.Controllers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SimulationApp.UI {
    public partial class MainWindow : Window {

        private SimulationController? simulationController;

        public MainWindow() {
            InitializeComponent();
            StrategyPanelControl.StartRequested += OnStartRequested;
        }

        private async void OnStartRequested(object? sender, EventArgs e) {
            if (simulationController == null) {
                var configPath = Path.Combine(AppContext.BaseDirectory, "Models", "Resources", "configurations", "config2.xml");
                simulationController = new SimulationController(configPath);
                simulationController.Loop.Model.SetStrategy(StrategyPanelControl.StrategyDropdown.SelectedIndex);
                simulationController.OnCycleCompleted += () => {
                    Dispatcher.Invoke(() => {
                        SimulationPanelControl.UpdateDrawing(simulationController.Loop.Model);
                    });
                };
            }

            SimulationPanelControl.UpdateDrawing(simulationController.Loop.Model);
            try {
                await simulationController.RunCycleAsync().ConfigureAwait(true);
            } catch (AggregateException ex) {
                if (ex.InnerException is OperationCanceledException) {
                    Debug.WriteLine("Simulation was cancelled.");
                } else {
                    Debug.WriteLine($"Error stopping simulation: {ex.InnerException?.Message}");
                }
            }

            MessageBox.Show("Simulation started!");
        }

        private async void OnExitClick(object sender, RoutedEventArgs e) {
            if (simulationController != null) {
                try {
                    await simulationController.StopCycleAsync().ConfigureAwait(true);
                } catch (Exception ex) {
                    Debug.WriteLine($"Error stopping simulation: {ex.Message}");
                }
            }

            Application.Current.Shutdown();
        }
    }
}
