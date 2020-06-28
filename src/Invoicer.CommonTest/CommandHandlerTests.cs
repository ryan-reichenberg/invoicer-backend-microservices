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
        public void TestCommandHandler()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var handler = new Mock<ICommandHandler<ICommand>>();
            handler.Setup(x => x.Handle(command.Object));

            using (var mock = AutoMock.GetLoose(cfg => cfg.RegisterMock(handler)))
            {
                // mockA will be injected into TestComponent as IServiceA
                var component = mock.Create<CommandBus>();
                component.Send(command.Object);

                handler.Verify(m => m.Handle(command.Object), Times.Once);
                // ...and the rest of the test
            }
        }
    }
}
