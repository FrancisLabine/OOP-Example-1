namespace SimulationApp.Core.Controllers
{
    using SimulationApp.Core.Models;
    using SimulationApp.Core.Models.Domain;
    using SimulationApp.Core.Models.Utils;
    using SimulationApp.Core.Models.Utils.Xml;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SimulationController {
        public SimulationLoop Loop { get; }
        public EnvironmentModel Model { get; }

        public SimulationController(string configPath) {
            var reader = new XmlReaderService(configPath);
            var loader = new EnvironmentLoader(reader);
            Model = loader.Load();
            Loop = new SimulationLoop(Model.Buildings);
        }

        private CancellationTokenSource cts;
        private Task? loopTask;

        public Task RunCycleAsync() {
            try {
                if (loopTask is { IsCompleted: false }) {
                    return loopTask; // already running
                }

                cts = new CancellationTokenSource();
                loopTask = Loop.RunAsync(cts.Token);

                return Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error starting simulation: {ex.Message}");
                return Task.FromException(ex);
            }
        }

        public event Action? OnCycleCompleted {
            add => Loop.OnCycleCompleted += value;
            remove => Loop.OnCycleCompleted -= value;
        }


        public async Task StopCycleAsync() {
            if (cts != null) {
                cts.Cancel();
                try {
                    if (loopTask != null) {
                        await loopTask.ConfigureAwait(false); // wait for clean shutdown
                    }
                } catch (OperationCanceledException) {
                    // Expected on cancel
                } finally {
                    cts.Dispose();
                    cts = null;
                    loopTask = null;
                }
            }
        }

        public IEnumerable<string> GetStatus() =>
            Model.Buildings.Select(b => $"{b.Id}: {b.Inventory.Count} items");
    }
}
