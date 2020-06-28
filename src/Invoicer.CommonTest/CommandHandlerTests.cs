using System;
using Autofac.Extras.Moq;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using Moq;
using Xunit;

namespace Invoicer.CommonTest
{
    public class CommandHandlerTests
    {
        [Fact]
        public void TestCommandBus()
        {
            using (var mock = AutoMock.GetLoose()) {
                var bus = mock.Create<CommandBus>();
                bus.Send(new TestCommand());
                Assert.NotNull(bus);
                Assert.Equal(1, TestCommandHandler.Value);
            }
        }


        public class TestCommand : ICommand { }
        public class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public static int Value { get; set; }
            public void Handle(TestCommand command)
            {
                Value = 1;
            }
        }
    }
}
