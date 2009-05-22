// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System.Collections;

namespace NUnitExtension.IterativeTest.AddIn.UnitTests
{
  public class TestClass
  {
    public void MethodWithoutIterativeTestAttribute()
    {
    }

    [IterativeTest("ListGenerator")]
    public void MethodWithIterativeTestAttribute()
    {
    }

    public IEnumerable ListGenerator()
    {
      ArrayList rv = new ArrayList();
      rv.Add("firstValue");
      rv.Add("secondValue");
      rv.Add("thirdValue");
      return rv;
    }
  }
}