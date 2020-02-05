using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Get Ready Entity
    /// </summary>
    public class GetReadyTest
    {
        [Fact]
        public void To_Check_Create()
        {
            //Arrange
            var countryCode = CountryCode.Create("Test1");
            var makeCode = MakeCode.Create("Test1", countryCode);
            var dealerId = DealerId.Create("1");

            //Act
            var getReady = GetReady.Create(countryCode, makeCode, dealerId);

            //Assert
            Assert.IsType<GetReady>(getReady);
        }

        [Fact]
        public void To_Check_Update()
        {
            //Arrange
            var countryCode = CountryCode.Create("Test1");
            var makeCode = MakeCode.Create("Test1", countryCode);
            var dealerId = DealerId.Create("1");

            //Act
            var getReady = GetReady.Create(countryCode, makeCode, dealerId);

            getReady.Update(countryCode, makeCode, dealerId);
        }

        [Fact]
        public void To_Check_Set_Get_Ready_Info()
        {
            //Arrange
            var countryCode = CountryCode.Create("Test1");
            var makeCode = MakeCode.Create("Test1", countryCode);
            var dealerId = DealerId.Create("1");
            var effectiveDate = EffectiveDate.Create(DateTime.Now);
            var user = User.Create("test");
            var lastProgram = Program.frmVendor;
            //Act
            var getReady = GetReady.Create(countryCode, makeCode, dealerId);

            getReady.SetGetReadyInfo("test", "test2", 0, effectiveDate, lastProgram, user,DateTime.Now);
        }       
    }
}
