using System.Collections;
using System.Linq;
using NUnit.Framework;
using Tests;
using UnityEngine;
using UnityEngine.TestTools;

namespace GridSystem.Tests {
  [TestFixture]
  public class TestGridComponent {
    [UnityTest]
    public IEnumerator TestSimpleCreate() {
      var gridSystem = new GameObject("gridSystemGo").AddComponent<GridSystem>();
      var mobGridComponent = (new GameObject("mobGo") { transform = { parent = gridSystem.transform } }).AddComponent<GridComponent>();
      mobGridComponent.MoveTo(new Vector2Int(0, 1));
      yield return null;
      Assert.AreEqual(gridSystem.GetGridEntities(new Vector2Int(0, 1)).Count(), 1);
      yield return null;
    }

    [UnityTest]
    public IEnumerator TestInstantiate() {
      var gridSystem = new GameObject("gridSystemGo").AddComponent<GridSystem>();
      var mobGridComponent = (new GameObject("mobGo") { transform = { parent = gridSystem.transform } }).AddComponent<GridComponent>();
      var instantiatedMob = Object.Instantiate(mobGridComponent.gameObject, gridSystem.transform);
      instantiatedMob.GetComponent<GridComponent>().MoveTo(new Vector2Int(0, 1));
      yield return null;
      Assert.AreEqual(gridSystem.GetGridEntities(new Vector2Int(0, 0)).Count(), 1);
      Assert.AreEqual(gridSystem.GetGridEntities(new Vector2Int(0, 1)).Count(), 1);
      yield return null;
    }

    [UnitySetUp]
    public IEnumerator UnitySetUp() {
      yield return TestFrameworkHelper.InitTestScene();
    }
  }
}
