using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
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
    /// Test cases for ContactEmailRepository
    /// </summary>
    public class ContactEmailRepositoryTest
    {
        private readonly Mock<IContext> _context;
        public ContactEmailRepositoryTest()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            _context = mockRepository.Create<IContext>();
        }

        [Fact]
        public void To_Check_Check_Contact_Email()
        {
            //Arrange
            var baseRepository = new Mock<BaseReadOnlyRepository<Entity>>(_context.Object);
            baseRepository.Setup(x => x.GetAllAsync(It.IsAny<Specification<Entity>>()));
            var contactEmailRepository = new ContactEmailRepository(_context.Object);

            //Act
            var result = contactEmailRepository.CheckContactEmail(It.IsAny<CountryCode>(), It.IsAny<MakeCode>(), It.IsAny<DealerId>(), It.IsAny<ContactType>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void To_Check_Add_Or_Update_Email()
        {
            //Arrange
            var baseRepositry = new Mock<BaseRepository<Entity>>(_context.Object);
            baseRepositry.Setup(x => x.SaveOrUpdateAsync(It.IsAny<Entity>())).Returns(Task.CompletedTask);
            var contactEmailRepository = new ContactEmailRepository(_context.Object);

            //Act
            var result = contactEmailRepository.AddOrUpdateEmail(It.IsAny<ContactEmail>());
            //Assert
            Assert.NotNull(result);
        }
    }
}
