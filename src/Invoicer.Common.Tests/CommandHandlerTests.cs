using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common.Exceptions;
using Invoicer.Common.Handlers;
using MediatR;
using Xunit;
using Container = StructureMap.Container;

namespace Invoicer.Common.Tests
{
    public class CommandHandlerTests
    {
        private CommandBus _bus;

        public CommandHandlerTests()
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(CommandHandlerTests));
                    scanner.IncludeNamespaceContainingType<TestCommand>();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(ICommandHandler<>));
                });
                cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
                cfg.For<IMediator>().Use<Mediator>();
            });

            var mediator = container.GetInstance<IMediator>();
            _bus = new CommandBus(mediator);
        }
        

        [Fact]
        public void CommandShouldBeResolved()
        {
            CommandResult result = _bus.Send(new TestCommand()).Result;
            Assert.True(result.Ok);
        }
        
        [Fact]
        public void CommandResultShouldFailCorrectly()
        {
            CommandResult result = _bus.Send(new TestFailCommand()).Result;
            Assert.True(((Failed)result).ResponseCode > 0);
            Assert.True(((Failed)result).Reasons.Count > 0);
        }

        [Fact]
        public async Task ThrowWhenTooManyCommandHandlersRegistered() {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _bus.Send(new MultipleCommand()));
        }
        [Fact]
        public async Task ThrowWhenNoCommandHandlerToResolve()
        {
             await Assert.ThrowsAsync<InvalidOperationException>(() => _bus.Send(new NoCommand()));
        }

        public class TestCommand : ICommand { }
        public class TestCommandHandler : ICommandHandler<TestCommand>
        {
            
            public Task<CommandResult> Handle(TestCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(CommandResult.Success());
            }
            
        }
        public class TestFailCommand : ICommand { }
        public class TestFailCommandHandler : ICommandHandler<TestFailCommand>
        {

            public async Task<CommandResult> Handle(TestFailCommand command , CancellationToken cancellationToken)
            {
                return CommandResult.Failure(HttpStatusCode.InternalServerError, "Test failure");
            }
        }
        public class  MultipleCommand : ICommand {}
        public class MultipleCommandHandlerOne : ICommandHandler<MultipleCommand>
        {
            public Task<CommandResult> Handle(MultipleCommand command, CancellationToken cancellationToken) 
            {
                throw new NotImplementedException();
            }
        }

        public class MultipleCommandHandlerTwo : ICommandHandler<MultipleCommand>
        {
            public Task<CommandResult> Handle(MultipleCommand command, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class NoCommand : ICommand { }
    }
}
