using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.UnitTests
{
    public static class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static IProductsRepository MockProductsRepository(params Product[] prods)
        {
            // Generate an implementer of IProductsRepository at runtime using Moq 
            var mockProductsRepos = new Mock<IProductsRepository>();
            mockProductsRepos.Setup(x => x.Products).Returns(prods.AsQueryable());
            return mockProductsRepos.Object;
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues)
        {
            var actualValues = ((RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);

            foreach (string key in expectedValues.Keys)
                actualValues[key].ShouldEqual(expectedValues[key]);
        }

        public static void ShouldBeDefaultView(this ActionResult actionResult)
        {
            actionResult.ShouldBeView(string.Empty);
        }

        public static void ShouldBeView(this ActionResult actionResult, string viewName)
        {
            Assert.IsInstanceOf<ViewResult>(actionResult);
            ((ViewResult)actionResult).ViewName.ShouldEqual(viewName);
        }
    }
}
