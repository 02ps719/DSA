using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA;

namespace TestNumberSystem
{
    [TestClass]
    public class ComparisonTest : NumberSystemTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            base.Init();
        }
        
        [TestMethod]
        public void TestBasicComparison()
        {
            Number n1 = new Number(6, new List<char>() { 'i', 'g' }); //12
            Number n2 = new Number(6, new List<char>() { 'i', 's' }); //13
            Assert.AreEqual(Order.LESSER, numberSystem.Compare(n1, n2));

             n1 = new Number(6, new List<char>() { 'i', 's' }); //12
             n2 = new Number(6, new List<char>() { 'i', 'g' }); //13
            Assert.AreEqual(Order.GREATER, numberSystem.Compare(n1, n2));

            n1 = new Number(6, new List<char>() { 'i', 'g' }); //12
            n2 = new Number(6, new List<char>() { 'i', 'g' }); //13
            Assert.AreEqual(Order.EQUAL, numberSystem.Compare(n1, n2));

            n1 = new Number(6, new List<char>() { 'i', 'g', 's' }); //123
            n2 = new Number(6, new List<char>() { 'g', 'g' }); //22
            Assert.AreEqual(Order.GREATER, numberSystem.Compare(n1, n2));

            n1 = new Number(6, new List<char>() { 'i', 'o' }); //14 => 10 in base10
            n2 = new Number(10, new List<char>() { 'i', 'b' }); //10 in decimal 10
            Assert.AreEqual(Order.EQUAL, numberSystem.Compare(n1, n2));

            n1 = new Number(6, new List<char>() { 'g', 'i' }); //20 => 12 in base10 
            n2 = new Number(10, new List<char>() { 'i', 'b' }); //10 in decimal 10
            Assert.AreEqual(Order.GREATER, numberSystem.Compare(n1, n2));

            n1 = new Number(2, new List<char>() { 'i', 'b', 'b', 'b' }); //1000 in base2 => 8 in base10 
            n2 = new Number(10, new List<char>() { 'i', 'b' }); //10 in decimal 10
            Assert.AreEqual(Order.LESSER, numberSystem.Compare(n1, n2));

        }
    }
}
