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
            simController.RunCycleAsync(); // fire and forget
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
                    StrokeThickness = 2
                };
                SimulationCanvas.Children.Add(line);
            }

            foreach (var building in simController.Model.Buildings) {
                // Example: drawing building image
                var image = new Image {
                    Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../" + building.GetStatusIcon().ToString(), UriKind.Absolute)),
                    Width = 32,
                    Height = 32
                };
                Canvas.SetLeft(image, building.PosX);
                Canvas.SetTop(image, building.PosY);
                SimulationCanvas.Children.Add(image);
            }

            foreach (var building in simController.Model.Buildings) {
                // Example: drawing building image

                foreach (var comp in building.Transport) {
                    var image = new Image {
                        Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../SimulationApp.Core/Models/Ressources/" + comp.Type.ToString().ToLower() + ".png", UriKind.Absolute)),
                        Width = 32,
                        Height = 32
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




        //private void DrawSampleSimulation() {
        //    // Example: Draw two nodes connected by a line

        //    string corePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../../SimulationApp.Core/Models/Ressources");

        //    Image building1 = new Image {
        //        Source = new BitmapImage(new System.Uri(System.IO.Path.Combine(corePath, "E0_.png"))),
        //        Width = 40,
        //        Height = 40
        //    };
        //    Canvas.SetLeft(building1, 100);
        //    Canvas.SetTop(building1, 100);

        //    Image building2 = new Image {
        //        Source = new BitmapImage(new System.Uri(System.IO.Path.Combine(corePath, "UA0_.png"))),
        //        Width = 40,
        //        Height = 40
        //    };
        //    Canvas.SetLeft(building2, 300);
        //    Canvas.SetTop(building2, 100);

        //    Line path = new Line {
        //        X1 = 120,
        //        Y1 = 120,
        //        X2 = 320,
        //        Y2 = 120,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };

        //    SimulationCanvas.Children.Add(path);
        //    SimulationCanvas.Children.Add(building1);
        //    SimulationCanvas.Children.Add(building2);
        //}
    }
}