// *********************************************************************
// Copyright 2008, Kelly Anderson
// This is free software licensed under the MIT license. 
// *********************************************************************
using System;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Extensibility;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace NUnitExtension.IterativeTest.AddIn.UnitTests
{
  [TestFixture]
  public class IterativeTestAddinTest
  {
    private MockRepository _mocks;

    [SetUp]
    public void SetUp()
    {
      _mocks = new MockRepository();
    }

    [Test]
    public void Install_Successful()
    {
      IExtensionHost extensionHostMock = _mocks.CreateMock<IExtensionHost>();
      IExtensionPoint extensionPointMock = _mocks.CreateMock<IExtensionPoint>();
      IterativeTestAddIn addIn = new IterativeTestAddIn();

      Expect.Call( extensionHostMock.GetExtensionPoint( "TestCaseBuilders" ) ).Return( extensionPointMock );
      extensionPointMock.Install( addIn );

      _mocks.ReplayAll();
      bool installed = addIn.Install( extensionHostMock );

      _mocks.VerifyAll();
      Assert.That( installed, Is.True );
    }

    [Test]
    public void AddinHasCorrectAttribute()
    {
      IterativeTestAddIn addin = new IterativeTestAddIn();
      Type t = addin.GetType();
      object [] list = t.GetCustomAttributes( typeof( NUnitAddinAttribute ) , false);
      Assert.IsTrue(list.Length == 1);
      NUnitAddinAttribute attrib = (NUnitAddinAttribute) list[0];
      Assert.IsTrue( attrib.Name == "Iterative Test Extension" );
    }
    
    [Test]
    public void Install_Failure()
    {
      IExtensionHost extensionHostMock = _mocks.CreateMock<IExtensionHost>();
      IterativeTestAddIn addIn = new IterativeTestAddIn();

      Expect.Call( extensionHostMock.GetExtensionPoint( "TestCaseBuilders" ) ).Return( null );

      _mocks.ReplayAll();
      bool installed = addIn.Install( extensionHostMock );

      _mocks.VerifyAll();
      Assert.That( installed, Is.False );
    }

    [Test]
    public void CanBuildFrom_False()
    {
      IterativeTestAddIn addIn = new IterativeTestAddIn();
      MethodInfo method = GetTestClassMethod( "MethodWithoutIterativeTestAttribute" );

      bool canBuildFrom = addIn.CanBuildFrom( method );

      Assert.That( canBuildFrom, Is.False );
    }

    protected static MethodInfo GetTestClassMethod( string methodName )
    {
      Type testClass = typeof( TestClass );
      return testClass.GetMethod( methodName, BindingFlags.Public | BindingFlags.Instance );
    }

    [Test]
    public void CanBuildFrom_True()
    {
      IterativeTestAddIn addIn = new IterativeTestAddIn();
      MethodInfo method = GetTestClassMethod( "MethodWithIterativeTestAttribute" );

      bool canBuildFrom = addIn.CanBuildFrom( method );

      Assert.That( canBuildFrom, Is.True );
    }

    [Test]
    public void BuildFrom_WithParameter()
    {
      IterativeTestAddIn addIn = new IterativeTestAddIn();
      MethodInfo method = GetTestClassMethod( "MethodWithIterativeTestAttribute" );

      Test test = addIn.BuildFrom( method );

      Assert.That( test, Is.InstanceOfType( typeof( TestSuite ) ) );
      TestSuite suite = (TestSuite) test;

      Assert.That( suite.TestCount, Is.EqualTo( 3 ) );
    }
  }
}
