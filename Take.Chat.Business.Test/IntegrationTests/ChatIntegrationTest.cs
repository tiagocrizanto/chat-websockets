using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Take.Chat.Business.Test.Helpers;
using Xunit;

namespace Take.Chat.Business.Test.IntegrationTests
{
    public class ChatIntegrationTest
    {

        public WebDriverWait Wait;

        [Theory(DisplayName = "Test chat authentication")]
        [InlineData("chrome")]
        public void TestUserLogin(string browserName)
        {
            using var driver = SeleniumHelper.CreateWebDriver(browserName, typeof(ChatIntegrationTest));
            driver.Navigate().GoToUrl("http://localhost/Take.Chat.Web");

            driver.FindElement(By.Id("userName")).SendKeys("Usuario001" + Keys.Enter);
            driver.Url.ToLowerInvariant().Contains("http://localhost/Take.Chat.Web/Home/Chat?username=Usuario001");
        }

        [Theory(DisplayName = "Test send message")]
        [InlineData("chrome")]
        public void TestChatSendMessage(string browserName)
        {
            using var driver = SeleniumHelper.CreateWebDriver(browserName, typeof(ChatIntegrationTest));
            driver.Navigate().GoToUrl("http://localhost/Take.Chat.Web");

            driver.FindElement(By.Id("MessageField")).SendKeys("Mensagem de teste" + Keys.Enter);
            Assert.Equal("Mensagem de teste", driver.FindElement(By.XPath("//*[@id='msgs']/div/div/span")).Text);
        }
    }
}
