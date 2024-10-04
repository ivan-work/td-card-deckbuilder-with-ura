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
  [SerializeField] CardPrefab cardPrefab;

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
        CellPrefab instance = Instantiate(cellPrefab);
        instance.transform.position = new Vector3(x * 1.1f, y * 1.1f, 0);
        instance.GetComponent<SpriteRenderer>().color = getColor((CellType)level[y][x]);
      }
    }

  }

  Color getColor(CellType map) => map switch {
    CellType.EMPTY => new Color(.5f, .5f, .5f),
    CellType.ROAD => new Color(1, 1, 1),
    CellType.BASE => new Color(0, 1, 0),
    CellType.SPAWNER => new Color(1, 0, 0),
  };
}