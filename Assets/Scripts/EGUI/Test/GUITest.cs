using System.Collections;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace EGUI.Demo
{
    public class GUITest
    {
        // [Test]
        public void SimplePasses()
        {
            Assert.AreEqual(2, 1 + 1);
        }
    }
}