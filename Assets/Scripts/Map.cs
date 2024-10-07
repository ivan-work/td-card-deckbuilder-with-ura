using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

enum CellType {
  EMPTY = 0,
  ROAD = 1,
  BASE = 2,
  SPAWNER = 3,
}

public class Map : MonoBehaviour {
  [SerializeField] CellPrefab cellPrefab;
  [SerializeField] GameObject spawnerPrefab;
  [SerializeField] GameObject mobPrefab;

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
        CellPrefab instance = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
        CellType cellType = (CellType)level[y][x];
        instance.GetComponent<SpriteRenderer>().color = getColor(cellType);

        if (cellType == CellType.SPAWNER) {
          GameObject spawner = Instantiate(spawnerPrefab, new Vector3(x, y, -1), Quaternion.identity);
          spawners.Add(spawner);
        }
      }
    }

    EventManager.EndTurn.AddListener(OnEndTurn);
  }

  void OnEndTurn() {
    int turn = GameManager.Instance.turn;
    if (turn % 2 == 0) {
      foreach (var spawner in spawners) {
        SpawnMob(spawner);
      }
    }
  }

  void SpawnMob(GameObject spawner) {
    GameObject mob = Instantiate(mobPrefab, spawner.transform.position + Vector3.right, Quaternion.identity);
  }

  Color getColor(CellType map) => map switch {
    CellType.EMPTY => new Color(.5f, .5f, .5f),
    CellType.ROAD => new Color(1, 1, 1),
    CellType.BASE => new Color(0, 1, 0),
    CellType.SPAWNER => new Color(1, 0, 0),
  };
}