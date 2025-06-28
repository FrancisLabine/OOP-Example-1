
namespace SimulationApp.Core.Models.Domain.Shared {
    /// <summary>
    /// Observer interface for receiving production control notifications.
    /// </summary>
    public interface IObserver {
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
