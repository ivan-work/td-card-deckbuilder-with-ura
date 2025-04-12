using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Tests {
  // https://discussions.unity.com/t/play-mode-tests-scenehandling/759597
  public static class TestFrameworkHelper {
    private static GameObject? _testRunner;

    public static IEnumerator InitTestScene() {
      var activeScene = SceneManager.GetActiveScene();
      var rootGameObjects = activeScene.GetRootGameObjects();
      if (_testRunner == null) {
        _testRunner = rootGameObjects
          .FirstOrDefault(
            go => go
              .GetComponents(typeof(MonoBehaviour))
              .Any(c => c.GetType().Name == "PlaymodeTestsController")
          );
      }

      var newScene = SceneManager.CreateScene("TestScene" + DateTime.Now.Millisecond);
      SceneManager.MoveGameObjectToScene(_testRunner, newScene);
      Object.DontDestroyOnLoad(_testRunner);
      yield return SceneManager.UnloadSceneAsync(activeScene);
    }
  }
}
