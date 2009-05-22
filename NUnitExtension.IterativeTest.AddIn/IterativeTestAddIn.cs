// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license.
// *********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace NUnitExtension.IterativeTest.AddIn
{
    [NUnitAddin(Name = "Iterative Test Extension")]
    public class IterativeTestAddIn : IAddin, ITestCaseBuilder
    {
        public const string IterativeTestAttribute = "NUnitExtension.IterativeTest.IterativeTestAttribute";

        #region IAddin Members

        public bool Install(IExtensionHost host)
        {
            IExtensionPoint testCaseBuilders = host.GetExtensionPoint("TestCaseBuilders");
            if (testCaseBuilders == null)
            {
                return false;
            }

            testCaseBuilders.Install(this);
            return true;
        }

        #endregion

        #region ITestCaseBuilder Members

        public bool CanBuildFrom(MethodInfo method)
        {
            return Reflect.HasAttribute(method, IterativeTestAttribute, false);
        }

        public Test BuildFrom(MethodInfo method)
        {
            Attribute attrib = Reflect.GetAttribute(method, IterativeTestAttribute, false);
            string[] functionNames = IterativeTestFramework.GetDataSetNames(attrib);
            TestSuite suite = new TestSuite(method.Name);

            object tester = Reflect.Construct(method.DeclaringType);
            List<object> dataset = new List<object>();
            foreach (string name in functionNames)
            {
                MethodInfo m = tester.GetType().GetMethod(name);
                if (m == null)
                    dataset.Add(tester.GetType().GetProperty(name).GetValue(tester, null));
                else
                    dataset.Add(m.Invoke(tester, null));
                
            }

            AddDataSet(ref suite, method, new List<object>(), 0, dataset);

            return suite;
        }

        private void AddDataSet(ref TestSuite suite, MethodInfo method, List<object> arguments, int counter, List<object> dataset)
        {
            if (counter >= dataset.Count)
                suite.Add(new IterativeTestCase(method, arguments.ToArray()));
            else
            {                
                if (dataset[counter].GetType().GetInterface("IEnumerable") != null)
                {
                    foreach (object arg in (IEnumerable)dataset[counter])
                    {
                        List<object> new_args = new List<object>(arguments);
                        new_args.Add(arg);
                        AddDataSet(ref suite, method, new_args, counter + 1, dataset);
                    }
                }
                else
                {
                    List<object> new_args = new List<object>(arguments);
                    new_args.Add(dataset[counter]);
                    AddDataSet(ref suite, method, new_args, counter + 1, dataset);
                }
            }
        }

        #endregion
    }
}