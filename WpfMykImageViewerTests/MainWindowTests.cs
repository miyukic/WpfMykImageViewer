using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfMykImageViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMykImageViewer.Tests {
    [TestClass()]
    public class MainWindowTests {
        [TestMethod()]
        public void TestXUnitTest() {
            var Window = new UnitTest();
            var result = Window.TestXUnit();
            Assert.AreEqual(result, 0);
        }
    }
}