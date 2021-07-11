using System;
using Xunit;
using FluentAssertions;

namespace Alinea.Core.EventSourcing.UnitTests
{
    public class TestCommandHandler
    {
        record NewCommand(string Name) : ICommand {}

        void NewCommandHandler(NewCommand command)
        {
            command.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        void TestCommandHandlerWithCommand()
        {
            var handler = new CommandHandler();
            handler.ConfigureHandler<NewCommand>(NewCommandHandler);
            handler.Handle(new NewCommand("Test"));
        }

        [Fact]
        void TestThrowArgumentNull()
        {
            var handler = new CommandHandler();

            handler.Invoking(x => x.ConfigureHandler<NewCommand>(null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'handler')");

            handler.Invoking(x => x.ConfigureHandler(typeof(NewCommand), null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'handler')");

            handler.Invoking(x => x.ConfigureHandler(null, null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'commandType')");

            handler.Invoking(x => x.Handle(null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'command')");
        }
    }
}