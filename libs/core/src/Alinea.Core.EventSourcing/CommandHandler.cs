using System;
using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    /// <summary>
    /// Standard implementation of an commandhandler for an aggregate.
    /// </summary>
    public class CommandHandler : ICommandHandler
    {
        private readonly Dictionary<Type, Action<ICommand>> _handlers;

        /// <summary>
        /// Initializes a new command handler
        /// </summary>
        public CommandHandler() 
        {
            _handlers = new();
        }

        /// <inheritdoc/>
        public void ConfigureHandler(Type commandType, Action<ICommand> handler)
        {
            if (commandType is null) throw new ArgumentNullException(nameof(commandType));
            if (handler is null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(commandType, handler);
        }

        /// <inheritdoc/>
        public void ConfigureHandler<TCommand>(Action<TCommand> handler) where TCommand : ICommand
        {
            if (handler is null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(typeof(TCommand), command => handler((TCommand) command));
        }

        /// <inheritdoc/>
        public void Handle(ICommand command)
        {
            if (command is null) throw new ArgumentNullException(nameof(command));

            Action<ICommand> handler;
            if (_handlers.TryGetValue(command.GetType(), out handler))
            {
                handler(command);
            }
        }
    }
}