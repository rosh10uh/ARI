using ARI.EVOS.Dealer.Command.CommandHandlers;
using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Infra.Interface;
using AutoMapper;
using CSharpFunctionalExtensions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Xunit;

namespace ARI.EVOS.Dealer.Command.Tests.CommandHandlers
{
    public class ContactEmailCommandHandlerTest
    {
        private readonly Mock<IContactEmailUnitOfWork> _contactEmailUnitOfWork;
        private readonly Mock<IContactEmailRepository> _contactEmailRepository;
        private readonly Mock<IMessage> _message;

        public ContactEmailCommandHandlerTest()
        {
            _contactEmailUnitOfWork = new Mock<IContactEmailUnitOfWork>();
            _contactEmailRepository = new Mock<IContactEmailRepository>();
            _message = new Mock<IMessage>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task To_Check_Handle_Async(bool result)
        {
            ContactDto contactDto = new ContactDto();
            var handler = new ContactEmailCommandHandler(_contactEmailUnitOfWork.Object,_message.Object);
            _message.Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It
             .IsAny<MessageBoxImage>())).Returns(MessageBoxResult.OK);
            _contactEmailUnitOfWork.Setup(x => x.ContactEmailRepository).Returns(_contactEmailRepository.Object);
            _contactEmailRepository.Setup(x => x.CheckContactEmail(It.IsAny<CountryCode>(), It.IsAny<MakeCode>(), It.IsAny<DealerId>(), It.IsAny<ContactType>())).Returns(result ? Task.FromResult(Maybe<List<ContactEmail>>.From(new List<ContactEmail>())) : Task.FromResult(Maybe<List<ContactEmail>>.From(new List<ContactEmail>() { contactDto.GetContacts() })));
            _contactEmailRepository.Setup(x => x.AddOrUpdateEmail(It.IsAny<ContactEmail>())).Returns(Task.CompletedTask);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactEmailModel, ContactEmailCommand>();
            });

            IMapper mapper = config.CreateMapper();
            var contactEmail = new ContactEmailModel
            {
                CountryCode = "US",
                MakeCode = "MC",
                DealerId = "456",
                ContactType = "Stock",
                ContactEmail = "test@test.com",
                ContactName = "test",
                StockContactEmail= "test@test.com",
                StockContactName= "test"
            };
            var command = mapper.Map<ContactEmailModel, ContactEmailCommand>(contactEmail);
            var handle = handler.HandleAsync(command);
            Assert.NotNull(handle);
        }
    }
    public class ContactDto : ContactEmail
    {
        public ContactDto()
        {

        }

        public ContactEmail GetContacts()
        {
            var dealerId = DealerId.Create("test123");
            var countryCode = CountryCode.Create("CA");
            var makeCode = MakeCode.Create("CH", countryCode);
            var contactType = ContactType.Stock;
            var email = Email.Create("test@test.com");
            var contactDto = new ContactDto() { DealerId = dealerId, CountryCode = countryCode, MakeCode = makeCode, ContactType = contactType, Name = "Test", Email = email };
            return contactDto;
        }
    }
}
