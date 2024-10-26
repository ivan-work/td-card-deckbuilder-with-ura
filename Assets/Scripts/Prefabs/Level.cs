using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.IK;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Level : MonoBehaviour {
  [SerializeField] CellPrefab cellPrefab;
  [SerializeField] GameObject spawnerPrefab;
  [SerializeField] GameObject mobPrefab;

  [SerializeField] int width, height;

  List<GameObject> spawners = new List<GameObject>();

  private void Start() {
    // int[][] level = {
    //   new []{0,0,1,1,1,0,0,0,0,0},
    //   new []{0,0,1,0,1,0,0,0,0,0},
    //   new []{0,3,1,1,1,1,1,1,2,0},
    //   new []{0,0,0,0,0,1,0,0,0,0},
    //   new []{0,0,0,0,0,1,1,1,0,0},
    // };
    int[][] level = {
      new[] {1, 1, 1, 0, 2},
      new[] {1, 1, 3, 0, 1},
      new[] {1, 1, 0, 1, 1},
      new[] {1, 1, 1, 1, 1},
      new[] {3, 1, 1, 0, 2},
    };

    // List<CellPrefab> queue = new();
    Queue<PathComponent> pathfindingQueue = new();

    for (int y = 0; y < level.Length; y++) {
      for (int x = 0; x < level[y].Length; x++) {
        var cellType = (CellType) level[^(y + 1)][x];

        var instance = Instantiate(cellPrefab, transform).OnSpawn(cellType);
        instance.GetComponent<GridComponent>().moveTo(new Vector2Int(x, y));

        if (cellType != CellType.Empty) {
          instance.AddComponent<PathComponent>();
        }

        if (cellType == CellType.Base) {
          instance.GetComponent<PathComponent>().distanceToBase = 0;
          pathfindingQueue.Enqueue(instance.GetComponent<PathComponent>());
        }

        if (cellType == CellType.Spawner) {
          var spawner = Instantiate(spawnerPrefab, transform);
          spawner.GetComponent<GridComponent>().moveTo(new Vector2Int(x, y));
          spawners.Add(spawner);
        }
      }
    }

    while (pathfindingQueue.Any()) {
      var pathCell = pathfindingQueue.Dequeue();
      var neighbors = pathCell.getNeighbors();

      foreach (var neighbor in neighbors) {
        var localDistance = pathCell.distanceToBase + neighbor.moveCost;
        if (localDistance < neighbor.distanceToBase) {
          neighbor.distanceToBase = localDistance;
        }
        // писать код дальше
      }
    }

    EventManager.PhaseActionSlow.AddListener(OnEndTurn);
  }

  void SpawnMob(GameObject spawner) {
    GameObject mob = Instantiate(mobPrefab, transform);
    mob.GetComponent<GridComponent>().moveTo(
      spawner.GetComponent<GridComponent>().gridPos
    );
  }

  IEnumerator OnEndTurn() {
    int turn = GameManager.Instance.turn;
    if (turn % 3 == 0) {
      foreach (var spawner in spawners) {
        SpawnMob(spawner);
      }
    }

    yield break;
  }
}