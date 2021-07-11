using System;
using Xunit;
using FluentAssertions;

namespace Alinea.Core.EventSourcing.UnitTests
{
    public class TestEventRouter
    {
        record NewEvent(string Name, int Version) : IEvent {}

        void HandleNewEvent(NewEvent @event)
        {
            @event.Name.Should().NotBeNullOrWhiteSpace();
            @event.Version.Should().BeGreaterThan(0);
        }

        [Fact]
        public void TestRouter()
        {
            var router = new EventRouter();

            router.ConfigureRoute<NewEvent>(HandleNewEvent);
            router.Route(new NewEvent("Test", 2));
        }

        [Fact]
        public void TestThrowArgumentNull()
        {
            var router = new EventRouter();

            router.Invoking(x => x.ConfigureRoute(null, null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'event')");


            router.Invoking(x => x.ConfigureRoute(typeof(NewEvent), null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'handler')");


            router.Invoking(x => x.ConfigureRoute<NewEvent>(null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'handler')");


            router.Invoking(x => x.Route(null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'event')");
        }
    }
}