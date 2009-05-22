// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System;
using System.Reflection;

namespace NUnitExtension.IterativeTest.AddIn.UnitTests
{
  public class BaseTestFixture
  {
    protected const string Method_IterativeTestMethod = "MethodWithIterativeTestAttribute";

    protected MethodInfo GetIterativeTestMethod()
    {
      return GetTestClassMethod(Method_IterativeTestMethod);
    }

    protected MethodInfo GetTestClassMethod(string methodName)
    {
      Type testClass = typeof (TestClass);
      return testClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
    }
  }
}