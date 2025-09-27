using Reqnroll;
using Xunit;
using SauceDemo.Tests.PageObjects;
using SauceDemo.Tests.Hooks;

namespace SauceDemo.Tests.StepDefinitions
{
    [Binding]
    public class ProductStepDef
    {
        private readonly DriverContext _driverContext;
        private ProductPage _productPage;

        public ProductStepDef(DriverContext driverContext)
        {
            _driverContext = driverContext;
            _productPage = new ProductPage(_driverContext.Page);
        }

        [Then(@"user should sees the products header")]
        public async Task ThenUserShouldSeesTheProductsHeader()
        {
            var isHeaderVisible = await _productPage.IsProductsHeaderVisible();
            Assert.True(isHeaderVisible);
        }
    }
}
