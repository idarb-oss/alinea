using System;
using Xunit;
using FluentAssertions;

namespace Alinea.Core.EventSourcing.UnitTests
{
    public class TestChangeTracker
    {
        record ChangeEvent(string Name) : IEvent {}

        [Fact]
        void TestChangeTrackerCreation()
        {
            var tracker = new ChangeTracker();

            tracker.Should().BeEmpty();
        }

        [Fact]
        void TestTrackChangeAndReset()
        { 
            var tracker = new ChangeTracker();

            tracker.TrackChange(new ChangeEvent("Test"));

            tracker.Should().NotBeEmpty();
            
            foreach (var @event in tracker)
            {
                @event.Should().BeOfType<ChangeEvent>();
                var change = @event as ChangeEvent;
                change.Name.Should().BeEquivalentTo("Test");
            }

            tracker.Reset();
            tracker.Should().BeEmpty();
        }

        [Fact]
        void TestThrowArgumentNull()
        {
            var tracker = new ChangeTracker();

            tracker.Invoking(x => x.TrackChange(null))
            .Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'event')");
        }
    }
}