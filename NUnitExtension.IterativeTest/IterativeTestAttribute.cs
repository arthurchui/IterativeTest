// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System;

namespace NUnitExtension.IterativeTest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class IterativeTestAttribute : Attribute
    {
        private string[] _dataSetNames;
        private string _description;

        public IterativeTestAttribute(params string[] dataSetNames)
        {
            _dataSetNames = dataSetNames;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string[] DataSetNames
        {
            get { return _dataSetNames; }
            set { _dataSetNames = value; }
        }
    }
}