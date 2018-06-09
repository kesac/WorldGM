using System;
using WorldGM.Generation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace WorldGM.Test.Generator
{
    [TestClass]
    public class GenerationHelpersUnitTest
    {

        [TestMethod]
        public void BetweenHelper_WithOneElement()
        {
            var r = new Random();
            var generated = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                generated.Add(r.Between(0, 0));
            }

            Assert.IsTrue(generated.Contains(0));
        }

        [TestMethod]
        public void BetweenHelper_WithMultipleElements()
        {
            var r = new Random();
            var generated = new List<int>();
            for(int i = 0; i < 100; i++)
            {
                generated.Add(r.Between(0, 2));
            }

            Assert.IsTrue(generated.Contains(0));
            Assert.IsTrue(generated.Contains(1));
            Assert.IsTrue(generated.Contains(2));
        }

        [TestMethod]
        public void NormalizedBetweenHelper_CheckLowerBound()
        {
            var r = new Random();
            var generated = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                var value = r.NormalizedBetween(0, 100);
                generated.Add(value);
            }
            
            Assert.IsTrue(generated.Where(x => x < 0).Count() == 0);
        }

        [TestMethod]
        public void NormalizedBetweenHelper_CheckUpperBound()
        {
            
            var r = new Random();
            var generated = new List<int>();
            for (int i = 0; i < 5000; i++)
            {
                var value = r.NormalizedBetween(0, 100);
                System.Diagnostics.Debug.WriteLine(value);
                generated.Add(value);
            }

            Assert.IsTrue(generated.Where(x => x > 100).Count() == 0);
        }
    }
}
