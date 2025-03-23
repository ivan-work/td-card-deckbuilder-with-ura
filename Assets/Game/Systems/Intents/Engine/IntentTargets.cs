using System;
using UnityEngine;

namespace Intents.Engine {
  [Serializable]
  public class IntentTargets {
    public readonly GameObject[] GameObjects;
    public readonly Vector2Int[] Positions;

    public IntentTargets(GameObject[] gameObjects, Vector2Int[] positions) {
      GameObjects = gameObjects;
      Positions = positions;
    }

    public static IntentTargets Create(GameObject[] gameObjects, Vector2Int[] positions) {
      return new IntentTargets(gameObjects, positions);
    }

    public static IntentTargets Create(params GameObject[] gameObjects) {
      return new IntentTargets(
        gameObjects,
        new Vector2Int[] { }
      );
    }

    public static IntentTargets Create(params Vector2Int[] positions) {
      return new IntentTargets(new GameObject[] { }, positions);
    }

    public static IntentTargets Create(GameObject gameObject, Vector2Int position) {
      return new IntentTargets(new[] { gameObject }, new[] { position });
    }
  }
}
