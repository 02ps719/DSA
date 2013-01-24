using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class PartitionTest : NumberSystemTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            base.Init();
        }
        [TestMethod]
        public void TestPartition()
        {
            numberSystem.Partition(numberSystem.numbers.ToArray(), 0, numberSystem.numbers.Count-1);            
        }
    }
}
