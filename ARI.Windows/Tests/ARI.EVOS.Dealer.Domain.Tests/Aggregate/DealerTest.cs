using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Aggregate
{
    /// <summary>
    /// Test cases for behaviour of Dealer Aggregate Root
    /// </summary>
    public class DealerTest
    {
        private CountryCode _countryCode;
        private MakeCode _makeCode;
        private DealerId _dealerId;

        [Fact]
        public void To_Check_Create_Dealer()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            Assert.IsType<ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer>(dealer);
        }

        [Fact]
        public void To_Check_Set_User()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var creationUser = User.Create("Test1");
            var lastUser = User.Create("Test2");
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetUser(creationUser, lastUser);

            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Vendor()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var vendor = Vendor.Create("1", "Test");
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetVendor(vendor);

            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Contact_Information()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var phoneNumber1 = PhoneNumber.Create("982-508-7875", _countryCode);
            var phoneNumber2 = PhoneNumber.Create("982-508-7875", _countryCode);
            var fax = Fax.Create("009825087875", _countryCode);
            var email = Email.Create("test@tests.com");
            var contactInformation = ContactInformation.Create("contact1", "contact2", phoneNumber1, phoneNumber2, "phoneExchg1", "phoneExchg2");
            contactInformation.SetOtherContactInformation(fax, email);
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetContactInformation(contactInformation);
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Address()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var address = Address.Create("city", "396321", "gujrat", "address1", "address2", "address3", "address4");
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetAddress(address);
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Additional_Information()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var dealerRating = DealerRating.Create("3");
            var paymentVia = PaymentVia.Create("D");
            var dealerOtherInformation = DealerOtherInformation.Create(dealerRating, paymentVia);
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetAdditionalInformation(dealerOtherInformation);
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Bank_Account()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            var bankAccount = BankAccount.Create("A123");
            var bankCity = BankCity.Create("city", _makeCode, _dealerId);
            var bankDetail = BankDetail.Create("123", bankAccount, "name", bankCity);
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetBankAccount(bankDetail);
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Dealer_Info()
        {
            //Arrange
            SetDealerDetails(out _countryCode, out _makeCode, out _dealerId);
            //Act
            var dealer = ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate.Dealer.Create(_countryCode, _makeCode, _dealerId);
            //Assert
            dealer.SetDealerInfo("comment", Program.frmDealerNetwork, DateTime.UtcNow, DateTime.UtcNow,
                Program.frmDealerNetwork, DateTime.UtcNow);

            dealer.SetDealerOtherInfo("client", "div", 30, 40, 30.30f);
            Assert.NotNull(dealer);
        }

        /// <summary>
        ///  Set Dealer's Details (Composite Key)
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        private void SetDealerDetails(out CountryCode countryCode, out MakeCode makeCode, out DealerId dealerId)
        {
            //Arrange
            countryCode = CountryCode.Create("US");
            makeCode = MakeCode.Create("/L", countryCode);
            //Act
            dealerId = DealerId.Create("1234");
            Assert.NotNull(dealerId);
        }
    }
}
