// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System;
using NUnit.Core;

namespace NUnitExtension.IterativeTest.AddIn
{
  public class IterativeTestFramework
  {
    private IterativeTestFramework()
    {
    }

    public static string[] GetDataSetNames(Attribute attribute)
    {
        return Reflect.GetPropertyValue(attribute, "DataSetNames") as string[];
    }
  }
}