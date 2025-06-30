using System;
using System.Threading;
using System.Threading.Tasks;
using SimulationApp.Core.Models;
using SimulationApp.Core.Models.Utils;
using SimulationApp.Core.Models.Utils.Xml;

namespace SimulationApp.Core.Controllers {
    public class SimulationController {
        public SimulationLoop Loop { get; }

        public SimulationController(string configPath) {
            var reader = new XmlReaderService(configPath);
            var loader = new EnvironmentLoader(reader);
            Loop = new SimulationLoop(loader.Load());
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
    }
}
