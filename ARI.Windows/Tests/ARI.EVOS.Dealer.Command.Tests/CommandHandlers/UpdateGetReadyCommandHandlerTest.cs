using ARI.EVOS.Dealer.Command.CommandHandlers;
using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealers.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace ARI.EVOS.Dealer.Command.Tests.CommandHandlers
{
    public class UpdateGetReadyCommandHandlerTest
    {
        private readonly Mock<IGetReadyValidation> _getReadyValidation;
        private readonly Mock<IDealerUnitOfWork> _dealerUnitOfWork;
        private readonly Mock<IGetReadyRepository> _getReadyRepository;
        private readonly Mock<IDealerRepository> _dealerRepository;



        public UpdateGetReadyCommandHandlerTest()
        {
            _dealerUnitOfWork = new Mock<IDealerUnitOfWork>();
            _getReadyValidation = new Mock<IGetReadyValidation>();
            _getReadyRepository = new Mock<IGetReadyRepository>();
            _dealerRepository = new Mock<IDealerRepository>();

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Check_Handle_Async(bool result)
        {
            DealerDto dealerDto = new DealerDto();
            _getReadyValidation.Setup(x => x.IsValid(It.IsAny<AddGetReadyCommand>())).Returns(true);
            _dealerUnitOfWork.Setup(x => x.DealerRepository).Returns(_dealerRepository.Object);
            _dealerUnitOfWork.Setup(x => x.GetReadyRepository).Returns(_getReadyRepository.Object);
            _dealerRepository.Setup(x => x.GetByDealerIdAsync(It.IsAny<CountryCode>(), It.IsAny<MakeCode>(), It.IsAny<DealerId>())).Returns(result ? Task.FromResult(Maybe<List<Domain.Models.Dealer.Aggregate.Dealer>>.From(new List<Domain.Models.Dealer.Aggregate.Dealer>())) : Task.FromResult(Maybe<List<Domain.Models.Dealer.Aggregate.Dealer>>.From(new List<Domain.Models.Dealer.Aggregate.Dealer>() { dealerDto.GetDealers() })));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetReadyModel, AddGetReadyCommand>();
            });

            IMapper mapper = config.CreateMapper();
            var getReady = new GetReadyModel
            {
                CountryCode = "CA",
                MakeCode = "GMC",
                DealerId = "1506",
                GetReadyCategories = new List<string>() {
            "Truck",
            "Car"}
            };
            var command = mapper.Map<GetReadyModel, AddGetReadyCommand>(getReady);
            var handler = new AddGetReadyCommandHandler(_dealerUnitOfWork.Object, _getReadyValidation.Object);
            var handle = handler.HandleAsync(command);
            Assert.NotNull(handle);
        }
    }
}
