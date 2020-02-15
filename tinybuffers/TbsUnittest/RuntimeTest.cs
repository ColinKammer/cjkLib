using NUnit.Framework;
using System;
using System.Collections.Generic;
using TbsLib;
using TbsLib.outputGenerators;
using Tinybuffers;

namespace Tests
{
    public class RuntimenTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestBasicBuilder()
        {
            byte[] buffer = new byte[8];

            buffer.SetPartialBuffer((Byte)123, 0);
            Assert.AreEqual(new byte[] { 123, 0, 0, 0, 0, 0, 0, 0 }, buffer);

            buffer.SetPartialBuffer((UInt16)12345, 0);
            Assert.AreEqual(new byte[] { 0x30, 0x39, 0, 0, 0, 0, 0, 0 }, buffer);

            buffer.SetPartialBuffer((Int16)(-12345), 0);
            Assert.AreEqual(new byte[] { 0xCF, 0xC7, 0, 0, 0, 0, 0, 0 }, buffer);
        }



    }
}