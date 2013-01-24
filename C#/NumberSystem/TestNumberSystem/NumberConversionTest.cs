using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class NumberConversionTest : NumberSystemTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            base.Init();
        }

        [TestMethod]
        public void Base10ConversionTest()
        {
            Assert.AreEqual(numberSystem.numbers.Count, 6);
            numberSystem.numbers.ForEach(n => { var seq = ""; n.Sequence.ForEach(y => seq += y); Console.WriteLine(n.Radix + " " + seq); });
            Assert.AreEqual(8, numberSystem.NumberSystemBASE);
            Assert.AreEqual(numberSystem.NumberSystemBASE, numberSystem.SymbolNumberMap.Count());

            Assert.AreEqual(59,numberSystem.ConvertToBase10(numberSystem.numbers[0]));
            Assert.AreEqual(20, numberSystem.ConvertToBase10(numberSystem.numbers[1]));
            Assert.AreEqual(10, numberSystem.ConvertToBase10(numberSystem.numbers[2]));
            Assert.AreEqual(7, numberSystem.ConvertToBase10(numberSystem.numbers[3]));
            Assert.AreEqual(579, numberSystem.ConvertToBase10(numberSystem.numbers[4]));
            Assert.AreEqual(9, numberSystem.ConvertToBase10(numberSystem.numbers[5]));
            Console.WriteLine(numberSystem.ConvertToBase10(new Number(15, new List<char>(){ 'b', 'i', 'g', 'g', 'g', 'z' })));
        }

        [TestMethod]
        public void ConversionFromBase10Test()
        {
            var numberSystem = new NumberSystem();
            //numberSystem.InitializeBase(@"N:\Academics\BITS DLPD\DSA\Code\cs\NumberSystem\files\File1.txt");
            //numberSystem.CreateNumbers(@"N:\Academics\BITS DLPD\DSA\Code\cs\NumberSystem\files\File2.txt");
            //Assert.AreEqual(numberSystem.numbers.Count, 6);
            //numberSystem.numbers.ForEach(n => { var seq = ""; n.Sequence.ForEach(y => seq += y); Console.WriteLine(n.Radix + " " + seq); });
            //Assert.AreEqual(8, numberSystem.NumberSystemBASE);
            //Assert.AreEqual(numberSystem.NumberSystemBASE, numberSystem.SymbolNumberMap.Count());

            Assert.AreEqual("135",numberSystem.ConvertFromBase10(59,6));
        
        }
        
    }
}
