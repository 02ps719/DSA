using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class NumbersInitializationTest : NumberSystemTestBase
    {
        string invalidBase = @"..\..\..\files\File2_Invalid base.txt";
        string invalidChar = @"..\..\..\files\File2_Invalid characters.txt";
        [TestInitialize]
        public void SetUp()
        {
           // base.Init();
        }
        [TestMethod]
        public void NumbersInitializationPositiveTest()
        {
            base.Init();
            Assert.AreEqual(numberSystem.numbers.Count, 6);
            numberSystem.numbers.ForEach(n => { var seq = ""; n.Sequence.ForEach(y => seq += y); Console.WriteLine(n.Radix +" " + seq); });
            
        }

        [TestMethod]
        public void NumbersInitializationNegativeTestInValidBase()
        {
            try
            {
                base.Init(invalidBase);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception e)
            {
                if (!(e.GetType() == typeof(AssertFailedException)))
                    Assert.IsNotNull(e);
                else
                    Assert.Fail("Should have thrown exception");
            }
            

        }

        [TestMethod]
        public void NumbersInitializationNegativeTestInValidChar()
        {
            try
            {
                base.Init(invalidChar);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception e)
            {
                if(!(e.GetType() == typeof(AssertFailedException)))
                    Assert.IsNotNull(e);
                else
                    Assert.Fail("Should have thrown exception");
            }
            

        }
    }
}
