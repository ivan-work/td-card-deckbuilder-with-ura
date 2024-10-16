using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using UnityEngine;

enum CellType {
  EMPTY = 0,
  ROAD = 1,
  BASE = 2,
  SPAWNER = 3,
}

[RequireComponent(typeof(Grid))]
public class Map : MonoBehaviour {
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

        CellPrefab instance = Instantiate(cellPrefab, transform);
        instance.GetComponent<GridComponent>().moveTo(new Vector2Int(x, y));
        instance.GetComponent<SpriteRenderer>().color = getColor(cellType);

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

  Color getColor(CellType cell) => cell switch {
    CellType.EMPTY => new Color(.5f, .5f, .5f),
    CellType.ROAD => new Color(1, 1, 1),
    CellType.BASE => new Color(0, 1, 0),
    CellType.SPAWNER => new Color(1, 0, 0),
    _        => throw new InvalidEnumArgumentException (nameof(cell)),
  };
}