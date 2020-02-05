using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Infrastructure.Repositories;
using Chassis.Repository;
using Chassis.Repository.Specification;
using FluentNHibernate.Data;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Repositories
{
    /// <summary>
    ///  Test cases for GetReadyRepository
    /// </summary>
    public class GetReadyRepositoryTest
    {
        private readonly Mock<IContext> _context;
        public GetReadyRepositoryTest()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            _context = mockRepository.Create<IContext>();
        }

        [Fact]
        public void To_Check_Add_Or_Update_Get_Ready()
        {
            //Arrange
            var baseRepositry = new Mock<BaseRepository<GetReady>>(_context.Object);
            baseRepositry.Setup(x => x.SaveOrUpdateAsync(It.IsAny<GetReady>())).Returns(Task.CompletedTask);
            var getReadyRepositry = new GetReadyRepository(_context.Object);

            //Act
            var result = getReadyRepositry.AddOrUpdateGetReady(It.IsAny<GetReady>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Get_All_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetAllAsync());
            var getReadyRepositry = new GetReadyRepository(_context.Object);

            //Act
            var result = getReadyRepositry.GetAllAsync();

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void To_Check_Get_By_Id_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()));
            var getReadyRepositry = new GetReadyRepository(_context.Object);

            //Act
            var result = getReadyRepositry.GetByIdAsync(It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Get_By_Get_Ready_Vehicle_Async()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetAllAsync(It.IsAny<Specification<Entity>>()));
            var getReadyRepositry = new GetReadyRepository(_context.Object);

            //Act
            var result = getReadyRepositry.GetByGetReadyVehicleAsync(It.IsAny<CountryCode>(), It.IsAny<MakeCode>(), It.IsAny<DealerId>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Delete_Get_Ready_Detail()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetByIdAsync(It.IsAny<GetReady>()));
            var getReadyRepositry = new GetReadyRepository(_context.Object);

            //Act
            var result = getReadyRepositry.DeleteGetReadyDetail(It.IsAny<GetReady>());

            //Assert
            Assert.NotNull(result);
        }
    }
}
