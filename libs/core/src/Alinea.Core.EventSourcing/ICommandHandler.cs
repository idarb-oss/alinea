using System;

namespace Alinea.Core.EventSourcing
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Find the correct handler to be invoked with the given command argument.
        /// </summary>
        /// <param name="command">Command to handle</param>
         void Handle(ICommand command);

        /// <summary>
        /// Configure a new handler for a specific command type
        /// </summary>
        /// <param name="commandType">Type of the command to handle</param>
        /// <param name="handler">The handler action to be invoked</param>
         void ConfigureHandler(Type commandType, Action<ICommand> handler);

        /// <summary>
        /// Configure a new handler for a specific command type
        /// </summary>
        /// <param name="handler">The handler action to be invoked</param>
        /// <typeparam name="TCommand">Type of command to handle</typeparam>
         void ConfigureHandler<TCommand>(Action<TCommand> handler) where TCommand : ICommand;
    }
}