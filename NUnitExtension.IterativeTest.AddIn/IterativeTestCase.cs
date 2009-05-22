// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license.
// *********************************************************************
using System.Reflection;
using NUnit.Core;

namespace NUnitExtension.IterativeTest.AddIn
{
    public class IterativeTestCase : NUnitTestMethod
    {
        private readonly object[] _arguments;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="current">The "current" object in the iteration.</param>
        public IterativeTestCase(MethodInfo method, object[] current) : base(method)
        {
            _arguments = current;

            IterativeTestNameBuilder testNameBuilder = new IterativeTestNameBuilder(method, "", current);
            TestName.Name = testNameBuilder.TestName;
            TestName.FullName = testNameBuilder.FullTestName;
        }

        public object Arguments
        {
            get { return _arguments; }
        }

        public override void RunTestMethod(TestCaseResult testResult)
        {
            Reflect.InvokeMethod(Method, Fixture, _arguments);
        }
    }
}