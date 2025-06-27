// <copyright file="IObserver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Domain.Shared
{
    /// <summary>
    /// Observer interface for receiving production control notifications.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Called when the observer should start/resume processing.
        /// </summary>
        void NotifyStart();

        /// <summary>
        /// Called when the observer should pause or stop processing.
        /// </summary>
        void NotifyStop();
    }
}
