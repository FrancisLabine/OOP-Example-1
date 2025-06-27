// <copyright file="SimulationLoop.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SimulationApp.Core.Models.Domain.Shared;

    public class SimulationLoop
    {
        private readonly List<BuildingBase> buildings;
        private readonly int delayMilliseconds = 10;
        private bool active = true;

        public event Action OnCycleCompleted;

        public SimulationLoop(List<BuildingBase> buildings)
        {
            this.buildings = buildings;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (active && !cancellationToken.IsCancellationRequested)
            {
                foreach (var building in buildings)
                {
                    building.ExecuteRoutine();
                }

                OnCycleCompleted?.Invoke();

                await Task.Delay(delayMilliseconds, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Stop() => active = false;
    }
}