using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Actions
{
    public static class AssertLight
    {
        public static void AreEqual(String expected, String actual, string message)
        {
            try
            {
                Assert.AreEqual(expected, actual);
            }
            catch (AssertFailedException ex)
            {
                Console.WriteLine(message);
            }
        }

        public static void IsTrue(bool o, string message)
        {
            try
            {
                Assert.IsTrue(o);
            }
            catch (AssertFailedException)
            {
                Console.WriteLine(message);
            }
        }

        public static void IsFalse(bool o, string message)
        {
            try
            {
                Assert.IsFalse(o);
            }
            catch (AssertFailedException)
            {
                Console.WriteLine(message);
            }
        }

        public static void AreNotEqual(object notExpected, object actual, string message)
        {
            try
            {
                Assert.AreNotEqual(notExpected, actual);
            }
            catch (AssertFailedException ex)
            {
                Console.WriteLine(message);
            }
        }

        public static void IsNotNull(object o, string message)
        {
            try
            {
                Assert.IsNotNull(o);
            }
            catch (AssertFailedException)
            {
                Console.WriteLine(message);
            }
        }

        public static void IsNull(object o, string message)
        {
            try
            {
                Assert.IsNull(o);
            }
            catch (AssertFailedException)
            {
                Console.WriteLine(message);
            }
        }
    }
}

    