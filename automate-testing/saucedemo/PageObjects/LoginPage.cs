using Microsoft.Playwright;

namespace SauceDemo.Tests.PageObjects
{
    public class LoginPage : BasePage
    {
        private readonly string _baseUrl = "https://www.saucedemo.com";

        private ILocator UsernameInput => _page.Locator("#user-name");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#login-button");
        private ILocator ErrorMessage => _page.Locator("[data-test='error']");

        public LoginPage(IPage page) : base(page)
        {
        }

        public async Task NavigateToLoginPage()
        {
            await NavigateToUrl(_baseUrl);
            await UsernameInput.WaitForAsync();
        }

        public async Task EnterUsername(string username)
        {
            await UsernameInput.FillAsync(username);
        }

        public async Task EnterPassword(string password)
        {
            await PasswordInput.FillAsync(password);
        }

        public async Task ClickLoginButton()
        {
            await LoginButton.ClickAsync();
        }

        public async Task<string> GetErrorMessageText()
        {
            return await ErrorMessage.TextContentAsync();
        }
    }
}