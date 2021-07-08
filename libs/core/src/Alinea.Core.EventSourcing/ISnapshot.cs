namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Represents operations around snapshoting an aggregate root.
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// Resores a snapshot using the specified <paramref name="state"/> object.
        /// </summary>
        /// <param name="state">The state object to restore the snapshot from.</param>
         void RestoreSnapshot(object state);

        /// <summary>
        /// Takes a snapshot of the aggregate root.
        /// </summary>
        /// <returns>The state object that represents the snapshot.</returns>
         object TakeSnapshot();
    }
}