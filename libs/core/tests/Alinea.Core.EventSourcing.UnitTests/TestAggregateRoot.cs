using System;
using Xunit;
using FluentAssertions;

namespace Alinea.Core.EventSourcing.UnitTests
{
    record NewUserCommand(string Name) : ICommand {}
    record CreateNewUser(string Name) : IEvent {}
    record UpdateExcistingUserCommand(Guid Id, string Name) : ICommand {}
    record UpdateExcistingUser(string Name) : IEvent {}


    public class User : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public User() : base()
        {
            RegisterCommand<NewUserCommand>(HandleNewUser);
            RegisterCommand<UpdateExcistingUserCommand>(HandleUpdateUser);

            RegisterEvent<CreateNewUser>(OnCreateNew);
            RegisterEvent<UpdateExcistingUser>(OnUpdateExcistingUser);
        }

        void OnCreateNew(CreateNewUser @event)
        {
            Id = Guid.NewGuid();
            Name = @event.Name;
        }

        void OnUpdateExcistingUser(UpdateExcistingUser @event)
        {
            Name = @event.Name;
        }

        void HandleNewUser(NewUserCommand command)
        {
            var newEvent = new CreateNewUser(command.Name);
            ApplyChange(newEvent);
        }

        void HandleUpdateUser(UpdateExcistingUserCommand command)
        {
            var updateEvent = new UpdateExcistingUser(command.Name);
            ApplyChange(updateEvent);
        }
    }

    public class TestAggregateRoot
    {
        [Fact]
        public void TestAggregateCreation()
        {
            const string testName = "Test";
            const string updateName = "Updated";

            var user = new User();

            user.Id.Should().BeEmpty();
            user.Name.Should().BeNullOrEmpty();

            user.Handle(new NewUserCommand(testName));

            user.Id.Should().NotBeEmpty();
            user.Name.Should().BeEquivalentTo(testName);

            var id = user.Id;
            user.Handle(new UpdateExcistingUserCommand(user.Id, updateName));

            user.Id.Should().Be(id);
            user.Name.Should().BeEquivalentTo(updateName);

            user.HasChange().Should().BeTrue();

            var changes = user.GetChanges();

            int i = 0;

            foreach(var _ in changes)
                i++;

            i.Should().Be(2);

            user.ClearChanges();
            user.GetChanges().Should().BeEmpty();
        }

        [Fact]
        public void TestAggregateThrowsArgumentNull()
        {
            var user = new User();

            user.Invoking(x => x.Handle(null))
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'command')");

            user.Invoking(x => x.Initialize(null))
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'events')");
        }
    }
}