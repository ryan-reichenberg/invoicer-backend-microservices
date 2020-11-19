using System;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common.Busses;
using Invoicer.Common.Handlers;
using MediatR;
using StructureMap;
using Xunit;

namespace Invoicer.Common.Tests
{
    public class QueryHandlerTests
    {
        private QueryBus _bus;

        public QueryHandlerTests()
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(QueryHandlerTests));
                    scanner.IncludeNamespaceContainingType<TestQuery>();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(IQueryHandler<,>));
                });
                cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
                cfg.For<IMediator>().Use<Mediator>();
            });

            var mediator = container.GetInstance<IMediator>();
            _bus = new QueryBus(mediator);
        }
        

        [Fact]
        public void QueryShouldBeResolved()
        {
            int val = _bus.Query(new TestQuery()).Result;
            Assert.Equal(1, val);
        }

        [Fact]
        public async void ThrowWhenTooManyQueryHandlersRegistered()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _bus.Query(new MultipleQuery()));
        }
        [Fact]
        public async void ThrowWhenNoQueryHandlerToResolve()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _bus.Query(new NoQuery()));
        }

        public class TestQuery : IQuery<int> { }
        public class TestCommandHandler : IQueryHandler<TestQuery, int>
        {
            public Task<int> Handle(TestQuery query, CancellationToken cancellationToken)
            {
                return Task.FromResult(1);
            }
        }

        public class MultipleQuery : IQuery<int> { }
        public class MultipleQueryHandlerOne : IQueryHandler<MultipleQuery, int>
        {
            public Task<int> Handle(MultipleQuery query, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class MultipleQueryHandlerTwo : IQueryHandler<MultipleQuery, int>
        {
            public Task<int> Handle(MultipleQuery query, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class NoQuery : IQuery<int> { }
    }
}
