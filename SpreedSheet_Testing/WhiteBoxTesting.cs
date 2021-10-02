using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDOOCP_Assignment_Ramesh;

namespace SpreedSheet_Testing
{
    [TestClass]
    public class NumericalTesting
    {
        [TestMethod]
        public void TestSum()
        {
            IDictionary<String, String> s = new Dictionary<string, string>();
            s.Add("1", "2");
            s.Add("2", "5");
            s.Add("3", "10");

            double expected_result = 17;
            double actual_result;
            Formula n = new Formula();
            
            actual_result = n.Sum(s);
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestMultiply()
        {
            IDictionary<String, String> s = new Dictionary<string, string>();
            s.Add("1", "2");
            s.Add("2", "5");
            s.Add("3", "10");

            double expected_result = 100;
            double actual_result;
            Formula n = new Formula();

            actual_result = n.multiply(s);
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestMean()
        {
            List<double> m = new List<double>();
            m.Add(8);
            m.Add(6);
            m.Add(4);

            double expected_result = 6;
            double actual_result;
            Formula n = new Formula();

            actual_result = n.mean(m);
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestMedian()
        {
            List<double> m = new List<double>();
            m.Add(8);
            m.Add(6);
            m.Add(5);
            m.Add(3);


            double expected_result = 5.5;
            double actual_result;
            Formula n = new Formula();

            actual_result = n.median(m);
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestMode()
        {
            List<double> m = new List<double>();
            m.Add(8);
            m.Add(5);
            m.Add(5);
            m.Add(3);


            double expected_result = 5;
            double actual_result;
            Formula n = new Formula();

            actual_result = n.mode(m);
            Assert.AreEqual(expected_result, actual_result);
        }
    }
}
