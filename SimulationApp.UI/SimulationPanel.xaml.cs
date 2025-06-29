using SimulationApp.Core.Controllers;
using SimulationApp.Core.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimulationApp.UI {
    public partial class SimulationPanel : UserControl {
        public SimulationPanel() {
            InitializeComponent();
        }

        private SimulationController simController;
        private void StartButton_Click(object sender, RoutedEventArgs e) {
            if (simController == null) {
                simController = new SimulationController($"{Directory.GetCurrentDirectory()}/../../../SimulationApp.Core/Models/Ressources/configurations/config2.xml");
                simController.OnCycleCompleted += () => {
                    Dispatcher.Invoke(UpdateDrawing); // UI-safe update
                };
            }

            DrawInitialState();
            try {
                simController.RunCycleAsync(); // fire and forget
            } catch (AggregateException ex) {
                if (ex.InnerException is OperationCanceledException) {
                    Debug.WriteLine("Simulation was cancelled.");
                } else {
                    Debug.WriteLine($"Error stopping simulation: {ex.InnerException?.Message}");
                }
            }
        }

        private void DrawInitialState() {
            SimulationCanvas.Children.Clear();

            foreach (var path in simController.Model.Paths) {
                var line = new Line {
                    X1 = path.X1 + 10,
                    Y1 = path.Y1 + 3,
                    X2 = path.X2 + 10,
                    Y2 = path.Y2 + 3,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                SimulationCanvas.Children.Add(line);
            }

            foreach (var building in simController.Model.Buildings) {
                var image = new Image {
                    Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../" + building.GetStatusIcon().ToString(), UriKind.Absolute)),
                    Width = 32,
                    Height = 32,
                };
                Canvas.SetLeft(image, building.PosX);
                Canvas.SetTop(image, building.PosY);
                SimulationCanvas.Children.Add(image);
            }

            foreach (var building in simController.Model.Buildings) {
                foreach (var comp in building.Transport) {
                    var image = new Image {
                        Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../SimulationApp.Core/Models/Ressources/" + comp.Type.ToString().ToLower() + ".png", UriKind.Absolute)),
                        Width = 32,
                        Height = 32,
                    };

                    Canvas.SetLeft(image, comp.X);
                    Canvas.SetTop(image, comp.Y);
                    SimulationCanvas.Children.Add(image);
                };

            }
        }

        private void UpdateDrawing() {
            DrawInitialState();
        }
    }
}