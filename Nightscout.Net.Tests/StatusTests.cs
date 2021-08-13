using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Nightscout.Net.Tests.TestHelper;

namespace Nightscout.Net.Tests
{
    [TestClass]
    public class StatusTests
    {
        [TestMethod]
        public async Task TestStatus()
        {
            var status = await API.GetStatus();

            Assert.AreEqual("ok", status.Status);
        }

        [TestMethod]
        public async Task TestSettings()
        {
            var settings = await API.GetSettings();
            Assert.IsNotNull(settings.Units);
            Assert.IsNotNull(settings.CustomTitle);
        }
    }
}
