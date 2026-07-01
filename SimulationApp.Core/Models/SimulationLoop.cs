using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using SimulationApp.Core.Models.Domain;

namespace SimulationApp.Core.Models {
    public class SimulationLoop {
        private const int DelayMilliseconds = 10;

        public SimulationLoop(EnvironmentModel model) {
            Model = model;
        }

        public event Action? OnCycleCompleted;

        public EnvironmentModel Model { get; }

        public async Task RunAsync(CancellationToken token) {
            var cycles = 0;
            while (!token.IsCancellationRequested) {
                foreach (var building in Model.Buildings) {
                    building.ExecuteRoutine();
                }

                OnCycleCompleted?.Invoke();

                await Task.Delay(DelayMilliseconds, token).ConfigureAwait(false);
                if (cycles > 2500) {
                    Debug.WriteLine("Garbage collection triggered after 2500 cycles.");
                    GC.Collect();
                    cycles = 0;
                }

                cycles++;
            }
        }
    }
}
