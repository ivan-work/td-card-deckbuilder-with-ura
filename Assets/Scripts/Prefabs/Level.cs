using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Level : MonoBehaviour {
  [SerializeField] CellPrefab cellPrefab;
  [SerializeField] GameObject spawnerPrefab;
  [SerializeField] GameObject mobPrefab;

  [SerializeField] int width, height;

  List<GameObject> spawners = new List<GameObject>();

  void Start() {
    int[][] level = {
      new []{0,0,0,0,0,0,0,0,0,0},
      new []{0,0,0,0,0,0,0,0,0,0},
      new []{0,3,1,1,1,1,1,1,2,0},
      new []{0,0,0,0,0,0,0,0,0,0},
      new []{0,0,0,0,0,0,0,0,0,0},
    };

    for (int y = 0; y < level.Length; y++) {
      for (int x = 0; x < level[y].Length; x++) {
        CellType cellType = (CellType)level[y][x];

        CellPrefab instance = Instantiate(cellPrefab, transform).OnSpawn(cellType);
        instance.GetComponent<GridComponent>().moveTo(new Vector2Int(x, y));

        if (cellType == CellType.SPAWNER) {
          GameObject spawner = Instantiate(spawnerPrefab, transform);
          spawner.GetComponent<GridComponent>().moveTo(new Vector2Int(x, y));
          spawners.Add(spawner);
        }
      }
    }

    EventManager.EndTurn.AddListener(OnEndTurn);
  }

  void SpawnMob(GameObject spawner) {
    GameObject mob = Instantiate(mobPrefab, transform);
    mob.GetComponent<GridComponent>().moveTo(
      spawner.GetComponent<GridComponent>().gridPos
    );
  }

  void OnEndTurn() {
    int turn = GameManager.Instance.turn;
    if (turn % 2 == 0) {
      foreach (var spawner in spawners) {
        SpawnMob(spawner);
      }
    }
  }
}