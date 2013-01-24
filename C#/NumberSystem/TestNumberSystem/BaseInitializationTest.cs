using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class BaseInitializationTest 
    {
        [TestMethod]
        public void TestInitializationOfBase()
        {
            var numberSystem = new NumberSystem();
            numberSystem.InitializeBase(@"N:\Academics\BITS DLPD\DSA\Code\cs\NumberSystem\files\File1.txt");
            Assert.AreEqual(8, numberSystem.NumberSystemBASE);
            Assert.AreEqual(numberSystem.NumberSystemBASE, numberSystem.SymbolNumberMap.Count());
        }
    }
}
