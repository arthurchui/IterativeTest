// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NUnitExtension.IterativeTest.AddIn.UnitTests
{
    [TestFixture]
    public class IteratorTestFrameworkTest
    {
        [Test]
        public void GetDataSetNames()
        {
            string[] functionName = new string[] { "TestFunction" };
            IterativeTestAttribute attrib = new IterativeTestAttribute(functionName);

            string[] extractedFunctionName = IterativeTestFramework.GetDataSetNames(attrib);

            Assert.That(extractedFunctionName, Is.SameAs(functionName));
        }
    }
}