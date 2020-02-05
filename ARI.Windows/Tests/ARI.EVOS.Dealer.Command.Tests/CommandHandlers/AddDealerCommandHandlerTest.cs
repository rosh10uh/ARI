using ARI.EVOS.Dealer.Command.CommandHandlers;
using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using CSharpFunctionalExtensions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealer.Command.Tests.CommandHandlers
{
    public class AddDealerCommandHandlerTest
    {
        private readonly Mock<IDealerValidation> _dealerValidation;
        private readonly Mock<IDealerUnitOfWork> _dealerUnitOfWork;
        private readonly Mock<IDealerRepository> _dealerRepository;


        public AddDealerCommandHandlerTest()
        {
            _dealerUnitOfWork = new Mock<IDealerUnitOfWork>();
            _dealerValidation = new Mock<IDealerValidation>();
            _dealerRepository = new Mock<IDealerRepository>();
        }

        [Fact]
        public void To_Check_Handle_Async()
        {
            _dealerValidation.Setup(x => x.IsValid(It.IsAny<DealerCommand>())).Returns(true);
            _dealerUnitOfWork.Setup(x => x.DealerRepository).Returns(_dealerRepository.Object);
            var handler = new AddDealerCommandHandler(_dealerUnitOfWork.Object, _dealerValidation.Object);
            var command = new AddDealerCommand();
            _dealerRepository.Setup(x=>x.GetByDealerIdAsync(It.IsAny<CountryCode>(),It.IsAny<MakeCode>(),It.IsAny<DealerId>())).Returns(Task.FromResult(Maybe<List<Domain.Models.Dealer.Aggregate.Dealer>>.From(new List<Domain.Models.Dealer.Aggregate.Dealer>())));
            var handle = handler.HandleAsync(command);
            Assert.True(handle.IsCompletedSuccessfully);
        }

    }
}
