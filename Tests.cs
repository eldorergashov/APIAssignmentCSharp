using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITesting.Resources;
using APITesting.Types;
using System.Text.Json;

namespace APITesting
{
    [TestClass]
    public class Tests
    {
        static string firstSku = "";

        [TestMethod]
        public void NoNegativePriceTest()
        {
           var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            var b = (JsonElement)a[0];
            b.TryGetProperty("price", out JsonElement price);
            Assert.IsTrue(!price.ToString().Contains("-"));
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void DescriptionIsNotEmpty()
        {
            var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            var b = (JsonElement)a[0];
            b.TryGetProperty("description", out JsonElement description);
            Assert.IsTrue(description.ToString().Length > 0);
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void CreatedAtIsNotEmpty()
        {
            var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            var b = (JsonElement)a[0];
            b.TryGetProperty("createdAt", out JsonElement createdAt);
            Assert.IsTrue(createdAt.ToString().Length > 0);
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void UpdatedAtIsNotEmpty()
        {
            var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            var b = (JsonElement)a[0];
            b.TryGetProperty("updatedAt", out JsonElement updatedAt);
            Assert.IsTrue(updatedAt.ToString().Length > 0);
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void SKUIsNotEmpty()
        {
            var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            var b = (JsonElement)a[0];
            b.TryGetProperty("sku", out JsonElement sku);
            Assert.IsTrue(sku.ToString().Length > 0);
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void SKUsArrayLengthIsNotZero()
        {
            var a = HTTPHelper.GetRestCall<object[]>(string.Empty, null);
            Assert.IsTrue(HTTPHelper.lastStatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(a.Length != 0);
        }
    }
}
