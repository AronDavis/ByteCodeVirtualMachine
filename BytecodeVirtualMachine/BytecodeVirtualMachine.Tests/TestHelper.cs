using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BytecodeVirtualMachine.Tests
{
    internal static class TestHelper
    {
        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }
        
        internal static void AssertResultsEqual(IList<byte> expected, IList<byte> actual)
        {
            Assert.AreEqual(expected?.Count, actual?.Count);

            if (expected == null)
                return;

            for(int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

    }
}
