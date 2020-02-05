using ARI.EVOS.Infra.Service;
using Moq;
using System.Windows;
using Xunit;

namespace ARI.EVOS.Infra.Tests.Service
{
    /// <summary>
    ///  Test cases for display messagebox
    /// </summary>
    public class MessageTest
    {
        [Fact]
        public void To_Check_Show()
        {
            //Arrange
            var message = new Mock<Message>();
            //Act
            var result = message.Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Asterisk));
            //Assert
            Assert.NotNull(result);
        }
    }
}
