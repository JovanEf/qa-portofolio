using Reqnroll;
using Xunit;
using SauceDemo.Tests.PageObjects;
using SauceDemo.Tests.Hooks;
using System.Text;

namespace SauceDemo.Tests.StepDefinitions
{
    [Binding]
    public class LoginStepDef
    {
        private readonly DriverContext _driverContext;
        private LoginPage _loginPage;

        public LoginStepDef(DriverContext driverContext)
        {
            _driverContext = driverContext;
            _loginPage = new LoginPage(_driverContext.Page);
        }

        [Given(@"user navigates to the Login page")]
        public async Task GivenUserNavigatesToTheLoginPage()
        {
            await _loginPage.NavigateToLoginPage();
        }

        [When(@"user enters username ""(.*)""")]
        public async Task WhenUserEntersUsername(string username)
        {
            await _loginPage.EnterUsername(username);
        }

        [When(@"user enters password ""(.*)""")]
        public async Task WhenUserEntersPassword(string password)
        {
            await _loginPage.EnterPassword(password);
        }

        [When(@"user clicks the Login button")]
        public async Task WhenUserClicksTheLoginButton()
        {
            await _loginPage.ClickLoginButton();
        }

        [Then(@"user should sees error message ""(.*)"" on Login page")]
        public async Task ThenUserShouldSeesErrorMessageOnLoginPage(string expectedMessage)
        {
            var errorText = await _loginPage.GetErrorMessageText();
            if (!string.IsNullOrEmpty(expectedMessage))
            {
                Assert.Contains(expectedMessage, errorText);
            }
            else
            {
                var isErrorVisible = await _loginPage.IsErrorMessageVisible();
                Assert.True(isErrorVisible);
            }
        }

        [Then(@"user should remain on the Login page")]
        public void ThenUserShouldRemainOnTheLoginPage()
        {
            var currentUrl = _loginPage.GetCurrentUrl();
            Assert.DoesNotContain("inventory", currentUrl);
        }

        [When(@"user waits for (\d+) seconds")]
        public async Task WhenUserWaitsForSeconds(int seconds)
        {
            await Task.Delay(seconds * 1000);
        }

        [Then(@"page should load within (\d+) seconds")]
        public async Task ThenPageShouldLoadWithinSeconds(int maxSeconds)
        {
            var startTime = DateTime.Now;
            await _loginPage.WaitForPageLoad();
            var loadTime = DateTime.Now - startTime;

            Assert.True(loadTime.TotalSeconds <= maxSeconds,
                       $"Page took {loadTime.TotalSeconds} seconds to load, expected {maxSeconds} seconds or less");
        }
    }
}