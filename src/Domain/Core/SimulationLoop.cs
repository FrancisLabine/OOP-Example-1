// <copyright file="SimulationLoop.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SimulationApp.Domain.Shared;

    public class SimulationLoop
    {
        private readonly List<BuildingBase> buildings;
        private readonly int delayMilliseconds = 10;
        private bool active = true;

        public event Action? OnCycleCompleted;

        public SimulationLoop(List<BuildingBase> buildings)
        {
            this.buildings = buildings;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (this.active && !cancellationToken.IsCancellationRequested)
            {
                foreach (var building in this.buildings)
                {
                    building.ExecuteRoutine();
                }

                this.OnCycleCompleted?.Invoke();

                await Task.Delay(this.delayMilliseconds, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Stop() => this.active = false;
    }
}