using Microsoft.Playwright;

namespace SauceDemo.Tests.Hooks
{
    public class DriverContext
    {
        private IBrowser? _browser;
        private IBrowserContext? _browserContext;
        private IPage? _page;
        private IPlaywright? _playwright;

        public IPage Page => _page ?? throw new InvalidOperationException("Page is not initialized");
        public IBrowser Browser => _browser ?? throw new InvalidOperationException("Browser is not initialized");

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            _browserContext = await _browser.NewContextAsync();
            _page = await _browserContext.NewPageAsync();
        }

        public async Task CleanupAsync()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
                _page = null;
            }

            if (_browserContext != null)
            {
                await _browserContext.CloseAsync();
                _browserContext = null;
            }

            if (_browser != null)
            {
                await _browser.CloseAsync();
                _browser = null;
            }

            if (_playwright != null)
            {
                _playwright.Dispose();
                _playwright = null;
            }
        }
    }
}