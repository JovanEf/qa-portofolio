using Microsoft.Playwright;

namespace SauceDemo.Tests.PageObjects
{
    public abstract class BasePage
    {
        protected readonly IPage _page;

        protected BasePage(IPage page)
        {
            _page = page;
        }

        public async Task NavigateToUrl(string url)
        {
            await _page.GotoAsync(url);
        }

        public string GetCurrentUrl()
        {
            return _page.Url;
        }

        public async Task<string> GetPageTitle()
        {
            return await _page.TitleAsync();
        }

        public async Task WaitForPageLoad()
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task<bool> LocatorIsVisible(string selector)
        {
            return await _page.Locator(selector).IsVisibleAsync();
        }
    }
}