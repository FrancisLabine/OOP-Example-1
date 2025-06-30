using SimulationApp.Core.Controllers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace SimulationApp.UI {
    public partial class MainWindow : Window {

        private SimulationController simulationController;

        public MainWindow() {
            InitializeComponent();
            StrategyPanelControl.StartRequested += OnStartRequested;
        }

        private async void OnStartRequested(object? sender, EventArgs e) {
            if (simulationController == null) {
                simulationController = new SimulationController($"{Directory.GetCurrentDirectory()}/../../../SimulationApp.Core/Models/Ressources/configurations/config2.xml");
                simulationController.Loop.Model.SetStrategy(StrategyPanelControl.StrategyDropdown.SelectedIndex);
                simulationController.OnCycleCompleted += () => {
                    Dispatcher.Invoke(() => {
                        SimulationPanelControl.UpdateDrawing(simulationController.Loop.Model);
                        });
                };
            }

            SimulationPanelControl.UpdateDrawing(simulationController.Loop.Model);
            try {
                simulationController.RunCycleAsync();
            } catch (AggregateException ex) {
                if (ex.InnerException is OperationCanceledException) {
                    Debug.WriteLine("Simulation was cancelled.");
                } else {
                    Debug.WriteLine($"Error stopping simulation: {ex.InnerException?.Message}");
                }
            }
            MessageBox.Show("Simulation started!");
        }

        private void OnExitClick(object sender, RoutedEventArgs e) {
            if (simulationController != null) {
                try {
                    simulationController.StopCycleAsync();
                } catch (Exception ex) {
                    Debug.WriteLine($"Error stopping simulation: {ex.Message}");
                }
            }
            Application.Current.Shutdown();
        }
    }
}