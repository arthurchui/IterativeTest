// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NUnitExtension.IterativeTest.AddIn.UnitTests
{
  [TestFixture]
  public class IterativeTestCaseTest : BaseTestFixture
  {
    [Test]
    public void Initialize()
    {
      MethodInfo method = GetIterativeTestMethod();
      string[] current = new string[] { "My Current Parameter" };

      IterativeTestCase testCase = new IterativeTestCase(method, current);

      Assert.That(testCase.Arguments, Is.SameAs(current));
      Assert.That(testCase.Method, Is.SameAs(method));
      Assert.That(testCase.FixtureType, Is.SameAs(typeof(TestClass)));
    }

    [Test]
    public void Initialize_TestName()
    {
      MethodInfo method = GetIterativeTestMethod();
      string[] current = new string[] { "My Current Parameter" };

      IterativeTestCase testCase = new IterativeTestCase( method, current );

      string expectedTestName = Method_IterativeTestMethod + "(My Current Parameter)";
      string expectedFullTestName = typeof( TestClass ).FullName + "." + expectedTestName;
      Assert.That( testCase.TestName.Name, Is.EqualTo( expectedTestName ) );
      Assert.That( testCase.TestName.FullName, Is.EqualTo( expectedFullTestName ) );
    }
  }
}