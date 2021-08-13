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
    public class EntryTests
    {
        [TestMethod]
        public async Task TestSGVValues()
        {
            var entry = await API.FetchLatestEntry();

            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.SGV);
        }
    }
}
