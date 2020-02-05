using ARI.EVOS.Dealers.Models;
using AutoMapper;
using Chassis.Command.CommandBus;
using Chassis.Command.Interfaces;
using Chassis.Query.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealer.AppServices.Tests
{
    public class BaseAppServiceTest
    {
        [Fact]
        public void To_Check_Dispatch_Command()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var mapper = new Mock<IMapper>();
            var commandBus = new CommandBus(serviceProvider.Object);
            var testCommandHandler = new TestCommandHandler();
            var testDto = new DealerNetworkModel();
            Type type = typeof(ICommandHandler<,>);
            Type[] typeArgs = { typeof(TestCommand), typeof(int) };
            Type handlerType = type.MakeGenericType(typeArgs);
            serviceProvider.Setup(x => x.GetService(handlerType)).Returns(testCommandHandler);
            serviceProvider.Setup(x => x.GetService(typeof(IMapper))).Returns(mapper.Object);
            var result = commandBus.DispatchAsync<TestCommand, int>(testDto);
            Assert.NotNull(result);
        }
    }

    public class TestCommand : ICommand<int>
    {
        public string DealerId { get; set; }
        public string CountryCode { get; set; }
        public string MakeCode { get; set; }
    }

    public class TestQuery : IQuery<int>
    {
        public string DealerId { get; set; }
        public string CountryCode { get; set; }
        public string MakeCode { get; set; }
    }

    public class TestCommandHandler : ICommandHandler<TestCommand, int>
    {
        public TestCommandHandler()
        {

        }

        public Task<Result<int>> HandleAsync(TestCommand command)
        {
            return Task.FromResult<Result<int>>(Result.Ok(1));
        }
    }
}
