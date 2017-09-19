using System;
using System.Web.Mvc;
using LabaManageSys.WebUI.HtmlHelpers;
using LabaManageSys.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabaManageSys.Tests.HtmlHelpers
{
    [TestClass]
    public class PagingHelpersTest
    {
        [TestMethod]
        public void PageLinksTest()
        {
            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper pageHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = pageHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(
                   @"<a class=""btn btn-default"" href=""Page1"">Page 1</a>"
                 + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">Page 2</a>"
                 + @"<a class=""btn btn-default"" href=""Page3"">Page 3</a>",
                result.ToString());
        }
    }
}
