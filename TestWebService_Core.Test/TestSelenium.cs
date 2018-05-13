using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestWebService_Core.Test
{
    [TestClass]
    public class TestSelenium
    {

        private IWebDriver firefox;

        [TestInitialize()]
        public void Initialize()
        {
            firefox = new FirefoxDriver();
        }

        [TestMethod]
        public void FirefoxWebServiceTest()
        {
            var url = "http://localhost:51922/api/values";

            firefox.Navigate().GoToUrl(url);
            firefox.Manage().Window.Maximize();
            firefox.FindElement(By.Id("rawdata-tab")).Click();

            var response = firefox.FindElement(By.TagName("pre"));
            var resultjson = response.Text;

            Assert.IsTrue(resultjson.Contains("idAlumno"));
            Assert.IsTrue(resultjson.Contains("nombre"));
            Assert.IsTrue(resultjson.Contains("apellido"));
            Assert.IsTrue(resultjson.Contains("dni"));
            Assert.IsTrue(resultjson.Contains("edad"));
            Assert.IsTrue(resultjson.Contains("horaRegistro"));
            Assert.IsTrue(resultjson.Contains("student_Guid"));

            Assert.IsTrue(resultjson.Split('{').Length > 2);
        }

        [TestMethod()]
        public void FirefoxClientCreateTest()
        {
            var url = "http://localhost:51010/Home/Create";

            firefox.Navigate().GoToUrl(url);
            firefox.Manage().Window.Maximize();

            // Insertamos datos en la vista create
            firefox.FindElement(By.Id("Nombre")).SendKeys("Joan Maria");
            firefox.FindElement(By.Id("Apellido")).SendKeys("Castellroig");
            firefox.FindElement(By.Id("Dni")).SendKeys("88888876A");
            firefox.FindElement(By.Id("FechaNacimiento")).SendKeys("10/01/1998");
            firefox.FindElement(By.Id("Edad")).SendKeys("20");
            firefox.FindElement(By.Id("HoraRegistro")).SendKeys("1/10/2017");
            firefox.FindElement(By.Id("Student_Guid")).SendKeys("a");
            firefox.FindElement(By.ClassName("btn-default")).Click();

            var result = firefox.FindElement(By.ClassName("table")).Text;
            Assert.IsTrue(result.Length > 1);
        }

        [TestCleanup()]
        public void CleanUp()
        {
            firefox.Close();
            firefox.Quit();
        }
    }
}
