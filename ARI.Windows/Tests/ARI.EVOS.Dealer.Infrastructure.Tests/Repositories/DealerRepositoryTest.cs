using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Infrastructure.Repositories;
using Chassis.Repository;
using Chassis.Repository.Specification;
using FluentNHibernate.Data;
using Moq;
using NHibernate.Mapping;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Repositories
{
    /// <summary>
    /// Test cases for DealerRepository
    /// </summary>
    public class DealerRepositoryTest
    {
        private readonly Mock<IContext> _context;

        public DealerRepositoryTest()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            _context = mockRepository.Create<IContext>();
        }

        [Fact]
        public void To_Check_Get_By_Id_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetByIdAsync(It.IsAny<PrimaryKey>()));
            var dealerRepository = new DealerRepository(_context.Object);

            //Act
            var result = dealerRepository.GetByIdAsync(It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Get_By_Dealer_Id_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetAllAsync(It.IsAny<Specification<Entity>>()));
            var dealerRepository = new DealerRepository(_context.Object);

            //Act
            var result = dealerRepository.GetByDealerIdAsync(It.IsAny<CountryCode>(), It.IsAny<MakeCode>(), It.IsAny<DealerId>());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Add_Or_Update_Dealer()
        {
            //Arrange
            var baseRepositry = new Mock<BaseRepository<Entity>>(_context.Object);
            baseRepositry.Setup(x => x.SaveOrUpdateAsync(It.IsAny<Entity>())).Returns(Task.CompletedTask);
            var dealerRepository = new DealerRepository(_context.Object);

            //Act
            var result = dealerRepository.AddOrUpdateDealer(It.IsAny<Domain.Models.Dealer.Aggregate.Dealer>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Get_All_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetAllAsync());
            var dealerRepository = new DealerRepository(_context.Object);

            //Act
            var result = dealerRepository.GetAllAsync();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Delete_Dealer()
        {
            //Arrange
            var baseRepositry = new Mock<BaseRepository<Entity>>(_context.Object);
            baseRepositry.Setup(x => x.DeleteAsync(It.IsAny<Entity>())).Returns(Task.CompletedTask);
            var dealerRepository = new DealerRepository(_context.Object);

            //Act
            var result = dealerRepository.DeleteDealer(It.IsAny<Domain.Models.Dealer.Aggregate.Dealer>());

            //Assert
            Assert.NotNull(result);
        }
    }
}
