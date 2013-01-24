using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class QuickSelectTest : NumberSystemTestBase
    {
        string destinationFilePath = @"..\..\..\files\File3.txt";           
        [TestInitialize]
        public void SetUp()
        {
            base.Init();
        }
        [TestMethod]
        public void TestQuickSelect()
        {
            numberSystem.numbers.ForEach(n => Console.WriteLine(numberSystem.ConvertToBase10(n)));

            var firstSmallest = numberSystem.QuickSelect(numberSystem.numbers.ToArray(),1,numberSystem.numbers.Count);
            numberSystem.PrintNumber(firstSmallest);
            numberSystem.PrintNumber(destinationFilePath, firstSmallest);
           

            var thirdSmallest = numberSystem.QuickSelect(numberSystem.numbers.ToArray(),2,numberSystem.numbers.Count);
            numberSystem.PrintNumber(thirdSmallest);
            Assert.AreEqual(thirdSmallest.Radix, 2);
            var areEqual = thirdSmallest.Sequence.OrderBy(x => x)
                      .SequenceEqual(new List<char> { 'i','b','i','b'}.OrderBy(x => x));
            Assert.AreEqual(true, areEqual);

            var secondSmallest = numberSystem.QuickSelect(numberSystem.numbers.ToArray(),3,numberSystem.numbers.Count);
            numberSystem.PrintNumber(secondSmallest);
            Assert.AreEqual(secondSmallest.Radix, 2);
             areEqual = secondSmallest.Sequence.OrderBy(x => x)
                      .SequenceEqual(new List<char> { 'i', 'b', 'b', 'i' }.OrderBy(x => x));
            Assert.AreEqual(true, areEqual);

            int k = 25;
            if (k > numberSystem.numbers.Count)
                k = numberSystem.numbers.Count / 2;
            var middleSmallest = numberSystem.QuickSelect(k);//numberSystem.QuickSelect(numberSystem.numbers.ToArray(), 25, numberSystem.numbers.Count);
            numberSystem.PrintNumber(middleSmallest);
            Assert.AreEqual(middleSmallest.Radix, 2);
            areEqual = middleSmallest.Sequence.OrderBy(x => x)
                      .SequenceEqual(new List<char> { 'i', 'b', 'i', 'b' }.OrderBy(x => x));
            Assert.AreEqual(true, areEqual);
        }
    }
}
