using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nightscout.Net.Tests
{
    public class TestHelper
    {
        public static NightscoutAPIOptions Options { get; } = new NightscoutAPIOptions("https://testsite.herokuapp.com", "Nightscout.NetTests", new Version(1, 0, 0), 1);
        public static NightscoutAPI API { get; } = new NightscoutAPI(Options);
    }
}
