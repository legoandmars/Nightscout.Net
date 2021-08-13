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
    public class ProfileTests
    {
        [TestMethod]
        public async Task TestProfile()
        {
            var profiles = await API.Profiles();

            Assert.IsNotNull(profiles);
            Assert.IsNotNull(profiles.DefaultProfile);
        }
    }
}
