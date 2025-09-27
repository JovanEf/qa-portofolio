using Microsoft.Playwright;
using Reqnroll;

namespace SauceDemo.Tests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly DriverContext _driverContext;
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(DriverContext driverContext, ScenarioContext scenarioContext)
        {
            _driverContext = driverContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await _driverContext.InitializeAsync();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                await TakeScreenshotOnFailure();
            }

            await _driverContext.CleanupAsync();
        }

        private async Task TakeScreenshotOnFailure()
        {
            try
            {
                var projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
                var screenshotsDir = Path.Combine(projectRoot, "TestResults", "Failed");
                Directory.CreateDirectory(screenshotsDir);

                var scenarioName = _scenarioContext.ScenarioInfo.Title
                    .Replace(" ", "_")
                    .Replace("/", "_")
                    .Replace("\\", "_");
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var fileName = $"{scenarioName}_{timestamp}_FAILED.png";
                var filePath = Path.Combine(screenshotsDir, fileName);

                await _driverContext.Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = filePath,
                    FullPage = true
                });

                Console.WriteLine($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }
    }
}