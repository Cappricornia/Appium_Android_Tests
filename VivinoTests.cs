using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Globalization;


namespace AppiumVivinoAppTests
{
    public class VivinoTests
    {
        private const string UriString = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppLocation = @"C:\vivino_8.18.11-8181203.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;


        [OneTimeSetUp]
        public void PrepareApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", VivinoAppLocation);
            options.AddAdditionalCapability("appPackage", "vivino.web.app");
            options.AddAdditionalCapability("appActivity", "com.sphinx_solution.activities.SplashActivity");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(UriString), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            this.driver.Quit();
        }

        [Test]
        public void Test_Search_For_Wine_Verify_Rating_And_Name()
        {
            var linkAccount = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkAccount.Click();

            var inputEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputEmail.SendKeys("test_vino@vino.com");

            var inputPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPassword.SendKeys("12345678");

            var linkLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            linkLogin.Click();

            var searchButton = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            searchButton.Click();

            var searchHeaderText = driver.FindElementById("vivino.web.app:id/search_header_text");
            searchHeaderText.Click();

            var inputSearch = driver.FindElementById("vivino.web.app:id/editText_input");
            inputSearch.SendKeys("Katarzyna Reserve Red 2006");

            var wineResultElement = driver.FindElementById("vivino.web.app:id/winename_textView");
            wineResultElement.Click();

            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");

            var wineRatingText = driver.FindElementById("vivino.web.app:id/rating").Text;
            var rating = double.Parse(wineRatingText, CultureInfo.InvariantCulture);

            var wineHighlights = driver.FindElementById("vivino.web.app:id/highlight_description");


            Assert.That(wineName.Text, Is.EqualTo("Reserve Red 2006"));
            Assert.That(rating >= 1.00 && rating <= 5.00);
            Assert.That(wineHighlights.Text, Is.EqualTo("Among top 1% of all wines in the world"));


            var summaryTabs = driver.FindElementById("vivino.web.app:id/tabs");
            var wineFacts = summaryTabs.FindElementByXPath("//android.widget.TextView[2]");
            wineFacts.Click();

            var factTitle = driver.FindElementById("vivino.web.app:id/wine_fact_title");
            Assert.That(factTitle.Text, Is.EqualTo("Grapes"));

            var factDescription = driver.FindElementById("vivino.web.app:id/wine_fact_text");
            Assert.That(factDescription.Text, Is.EqualTo("Cabernet Sauvignon,Merlot"));
        }


        [Test]
        public void Test_Login_With_Invalid_Password()
        {
            var linkAccount = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkAccount.Click();

            var inputEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputEmail.SendKeys("test_vino@vino.com");

            var inputPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPassword.SendKeys("hithere");

            var linkLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            linkLogin.Click();

            var errorMsg = driver.FindElementById("vivino.web.app:id/txtEmailOrPasswordWasIncorrect");

            Assert.That(errorMsg.Text, Is.EqualTo("Email or password is incorrect"));

        }

        [Test]
        public void Test_Login_With_Invalid_Email()
        {
            var linkAccount = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkAccount.Click();

            var inputEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputEmail.SendKeys("wrong_email@ymail.com");

            var inputPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPassword.SendKeys("12345678");

            var linkLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            linkLogin.Click();

            var errorMsg = driver.FindElementById("vivino.web.app:id/txtEmailOrPasswordWasIncorrect");

            Assert.That(errorMsg.Text, Is.EqualTo("The email does not exist"));

        }

        [Test]
        public void Test_Register_A_New_User()
        {
            var getStarted = driver.FindElementById("vivino.web.app:id/getstarted_layout");
            getStarted.Click();

            var inputEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputEmail.SendKeys("wrong_email" + DateTime.Now.Ticks + "@ymail.com");

            var inputPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPassword.SendKeys("123" + DateTime.Now.Ticks);

            var nextButton = driver.FindElementById("vivino.web.app:id/action_next");

            nextButton.Click();

            var firstName = driver.FindElementById("vivino.web.app:id/edtUserName");
            firstName.SendKeys("Anne");

            var lastName = driver.FindElementById("vivino.web.app:id/edtUserSurname");
            lastName.SendKeys("Anderson");


            var acceptTerms = driver.FindElementById("vivino.web.app:id/new_profile_agree_terms");

            acceptTerms.Click();

            var doneButton = driver.FindElementById("vivino.web.app:id/action_done");

            doneButton.Click();

            var searchButton = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            searchButton.Click();

            var searchHeaderText = driver.FindElementById("vivino.web.app:id/search_header_text");
            searchHeaderText.Click();

            var inputSearch = driver.FindElementById("vivino.web.app:id/editText_input");
            inputSearch.SendKeys("Katarzyna Reserve Red 2006");

            var wineResultElement = driver.FindElementById("vivino.web.app:id/winename_textView");
            wineResultElement.Click();


            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");

            Assert.That(wineName.Text, Is.EqualTo("Reserve Red 2006"));


        }


    }
}
