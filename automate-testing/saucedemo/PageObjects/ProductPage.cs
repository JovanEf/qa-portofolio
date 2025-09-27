using Microsoft.Playwright;

namespace SauceDemo.Tests.PageObjects
{
    public class ProductPage : BasePage
    {
        private ILocator ProductsHeader => _page.Locator("#header_container");

        public ProductPage(IPage page) : base(page)
        {
        }

        public async Task<bool> IsProductsHeaderVisible()
        {
            return await ProductsHeader.IsVisibleAsync();
        }
    }
}
