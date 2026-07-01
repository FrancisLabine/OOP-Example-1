using SimulationApp.Core.Models.Domain;
using System;
using System.IO;
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
                    Source = new BitmapImage(new Uri(GetResourcePath(GetIconPath(building)), UriKind.Absolute)),
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
                        Source = new BitmapImage(new Uri(GetResourcePath($"Models/Resources/{comp.Type.ToString().ToLower()}.png"), UriKind.Absolute)),
                        Width = 32,
                        Height = 32,
                    };

                    Canvas.SetLeft(image, comp.X);
                    Canvas.SetTop(image, comp.Y);
                    SimulationCanvas.Children.Add(image);
                }
            }
        }

        private static string GetResourcePath(string resourcePath) {
            var normalizedPath = resourcePath.Replace("SimulationApp.Core/", string.Empty)
                                             .Replace('/', System.IO.Path.DirectorySeparatorChar);
            return System.IO.Path.Combine(AppContext.BaseDirectory, normalizedPath);
        }

        private static string GetIconPath(SimulationApp.Core.Models.Domain.Buildings.BuildingBase building) {
            return building.GetStatus() switch {
                SimulationApp.Core.Models.Domain.Buildings.BuildingStatus.Empty => building.BuildingMetadata.IconEmpty,
                SimulationApp.Core.Models.Domain.Buildings.BuildingStatus.Low => building.BuildingMetadata.IconLow,
                SimulationApp.Core.Models.Domain.Buildings.BuildingStatus.Medium => building.BuildingMetadata.IconMedium,
                SimulationApp.Core.Models.Domain.Buildings.BuildingStatus.Full => building.BuildingMetadata.IconFull,
                _ => building.BuildingMetadata.IconEmpty,
            };
        }
    }
}
