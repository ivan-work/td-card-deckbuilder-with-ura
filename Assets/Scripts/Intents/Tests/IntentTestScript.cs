using System.Collections;
using Intents2;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IntentTestScript {
// Define a test-specific IntentBehaviour to track if Perform is called
  public class TestIntentValues : BaseIntentValues {
    public string TestString;
  }

  public class TestIntentBehaviour : IntentBehaviour<TestIntentValues> {
    public TestIntentValues LastValues;
    public bool WasPerformed { get; private set; }

    public override void Perform(IntentContext<TestIntentValues> context) {
      var values = context.Intent.Values;
      LastValues = values;
      WasPerformed = true;
    }
  }

  public class IntentTester : MonoBehaviour {
    public IntentFactory IntentFactory;
    public IntentTargets Targets;

    public void Test() {
      var ims = gameObject.AddComponent<IntentManagementSystem>();
      var intent = IntentFactory.CreateIntent(Targets);
      ims.AddIntent(intent);
      ims.PerformIntents();
    }
  }

  [TestFixture]
  public class IntentTesterTests {
    [Test]
    public void Test_IntentConversion() {
      
    }
    
    [Test]
    public void Test_AddsIntentToSystemAndPerformsIt() {
      // Setup the test environment
      var gameObject = new GameObject();
      var intentTester = gameObject.AddComponent<IntentTester>();

      // Create and set up the IntentFactory
      var intentFactory = gameObject.AddComponent<IntentFactory>();
      intentTester.IntentFactory = intentFactory;

      // Create the test behaviour and assign it to the factory
      var testBehaviour = ScriptableObject.CreateInstance<TestIntentBehaviour>();
      intentFactory.BehaviourTest = testBehaviour;

      // Set up test values and targets
      intentFactory.ValuesTest = new TestIntentValues() {TestString = "Working"};
      intentTester.Targets = new IntentTargets();

      // Act: Trigger the test method
      intentTester.Test();

      // Assert: Verify that the behaviour's Perform method was called
      Assert.IsTrue(testBehaviour.WasPerformed, "The intent behaviour was not performed.");
      Assert.AreEqual(testBehaviour.LastValues.TestString, "Working");
    }
  }
}
