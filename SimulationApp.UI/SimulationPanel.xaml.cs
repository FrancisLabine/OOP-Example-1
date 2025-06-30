using SimulationApp.Core.Controllers;
using SimulationApp.Core.Models;
using SimulationApp.Core.Models.Domain;
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

        public void UpdateDrawing(EnvironmentModel pModel) {
            SimulationCanvas.Children.Clear();

            foreach (var path in pModel.Paths) {
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

            foreach (var building in pModel.Buildings) {
                var image = new Image {
                    Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../" + building.GetStatusIcon().ToString(), UriKind.Absolute)),
                    Width = 32,
                    Height = 32,
                };
                Canvas.SetLeft(image, building.PosX);
                Canvas.SetTop(image, building.PosY);
                SimulationCanvas.Children.Add(image);
            }

            foreach (var building in pModel.Buildings) {
                foreach (var comp in building.Transport) {
                    var image = new Image {
                        Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/../../../SimulationApp.Core/Models/Ressources/" + comp.Type.ToString().ToLower() + ".png", UriKind.Absolute)),
                        Width = 32,
                        Height = 32,
                    };

                    Canvas.SetLeft(image, comp.X);
                    Canvas.SetTop(image, comp.Y);
                    SimulationCanvas.Children.Add(image);
                }
                ;

            }
        }
    }
}