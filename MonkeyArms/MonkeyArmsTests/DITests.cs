using System;
using NUnit.Framework;
using MonkeyArms;

namespace MonkeyArmsTests
{
	[TestFixture()]
	public class DITests
	{


		[Test(Description="Assert DI does not return null for Get")]
		public void TestGetReturnsNotNull ()
		{
			Assert.NotNull (DI.Get<TestClass> ());
		}

		[Test(Description="Assert Get returns new instances of a class by default")]
		public void TestGetReturnsNewInstanceByDefault ()
		{
			Assert.AreNotEqual (DI.Get<TestClass> (), DI.Get<TestClass> ());
		}

		[Test(Description="Assert when a class is registered as a Singleton the same instance is returned")]
		public void TestGetReturnsSingletonCorrectly ()
		{
			DI.MapSingleton<TestClass> ();
			Assert.AreEqual (DI.Get<TestClass> (), DI.Get<TestClass> ());
		}

		[Test(Description="Assert when a class is registered via interface Get by interface returns correct class")]
		public void TestRegisterInterface ()
		{
			DI.MapClassToInterface<ITestClass, TestClass> ();
			Assert.IsNotNull (DI.Get<ITestClass> () as TestClass);
		}

		[Test(Description="Assert MapCommandToInvoker maps command to invoker and correctly executes command when invoker is invoked")]
		public void TestMapCommandToInvoker ()
		{
			DI.MapSingleton<TestPM> ();
			DI.MapCommandToInvoker<TestCommand1, TestInvoker> ();
			DI.Get<TestInvoker> ().Invoke ();
			Assert.True (DI.Get<TestPM> ().Executed);

		}

	


	}
	/*
		 * Test Classes
		 * 
		*/

	public class TestInvoker:Invoker
	{
		public TestInvoker ():base()
		{

		}
	}

	public class TestPM
	{

		public bool Executed = false;

		public TestPM ()
		{

		}
	}

	public class TestCommand1:Command
	{

		[Inject]
		public TestPM PM{ get; set; }

		public TestCommand1 ():base()
		{

		}

		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);
			PM.Executed = true;
		
		}
	}

	public class TestCommand2:Command
	{

		public bool Executed = false;

		public TestCommand2 ():base()
		{

		}

		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);
			Executed = true;
		}
	}

	public interface ITestClass
	{
		void DoSomething ();
	}

	public class TestClass:ITestClass
	{

		public TestClass ()
		{

		}

		public void DoSomething ()
		{

		}
	}
}

