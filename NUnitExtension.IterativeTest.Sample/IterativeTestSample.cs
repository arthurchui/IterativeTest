// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license.
// *********************************************************************
using System.Collections;
using NUnit.Framework;

namespace NUnitExtension.IterativeTest.Sample
{
    [TestFixture]
    public class IterativeTestSample
    {
        private ArrayList list;
        public IEnumerable FileList
        {
            get
            {
                if (list == null)
                {
                    list = new ArrayList();
                    list.Add(@"C:\test\test1.xml");
                    list.Add(@"C:\test\test2.xml");
                    list.Add(@"C:\test\test3.xml");
                }
                return list;
            }
        }

        private ArrayList list2;
        public IEnumerable FileList2
        {
            get
            {
                if (list2 == null)
                {
                    list2 = new ArrayList();
                    list2.Add(1);
                    list2.Add(2);
                    list2.Add(3);
                    list2.Add(4);
                    list2.Add(5);
                }
                return list2;
            }
        }

        public int One
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// The parameter is the name of the property member of the TestFixture
        /// that returns the items to iterate over as IEnumerable. In this case, this
        /// test will be invoked three times, passing in the three strings in 
        /// FileList.
        /// </summary>
        /// <param name="current"></param>
        [IterativeTest("FileList")]
        public void FileNameTest(object current)
        {
            string curStr = (string)current;
            Assert.IsTrue(curStr.EndsWith("xml"));
        }

        [IterativeTest("FileList", "FileList2")]
        public void FileNameTest2(object current, object number)
        {
            string curStr = (string)current;
            int num = (int)number;
            Assert.IsTrue(curStr.EndsWith("xml"));
            Assert.Greater(num, 0);
        }

        [IterativeTest("FileList", "FileList2", "One")]
        public void FileNameTest3(string current, int num1, int num2)
        {
            Assert.IsTrue(current.EndsWith("xml"));
            Assert.Greater(num1, 0);
            Assert.Greater(num2, 0);
        }

        [IterativeTest("FileList", "One", "FileList2")]
        public void FileNameTest4(string current, int num1, int num2)
        {
            Assert.IsTrue(current.EndsWith("xml"));
            Assert.Greater(num1, 0);
            Assert.Greater(num2, 0);
        }

        [Test]
        public void RegularTest()
        {
            Assert.IsTrue(true, "here");
        }
    }
}
