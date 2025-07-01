using System;
using System.Threading;
using System.Threading.Tasks;
using SimulationApp.Core.Models.Domain;
using System.Diagnostics;

namespace SimulationApp.Core.Models {
    public class SimulationLoop {
        public EnvironmentModel Model { get; private set; }

        private const int DelayMillisesconds = 10;

        public event Action OnCycleCompleted;

        public SimulationLoop(EnvironmentModel model) {
            Model = model;
        }

        public async Task RunAsync(CancellationToken token) {
            var cycles = 0;
            while (!token.IsCancellationRequested) {
                foreach (var building in Model.Buildings) {
                    building.ExecuteRoutine();
                }

                OnCycleCompleted?.Invoke();

                await Task.Delay(DelayMillisesconds, token).ConfigureAwait(false);
                if (cycles > 2500) {
                    Debug.WriteLine("Garbage collection triggered after 2000 cycles.");
                    System.GC.Collect();
                    cycles = 0;
                }

                cycles++;
            }
        }
    }
}