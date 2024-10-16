using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType {
  EMPTY = 0,
  ROAD = 1,
  BASE = 2,
  SPAWNER = 3,
}

public class CellPrefab : MonoBehaviour {
  [SerializeField] public CellType cellType = CellType.EMPTY;

  public CellPrefab OnSpawn(CellType _cellType) {
    cellType = _cellType;
    GetComponent<SpriteRenderer>().color = getColor(cellType);
    return this;
  }

  Color getColor(CellType cell) => cell switch {
    CellType.EMPTY => new Color(.5f, .5f, .5f),
    CellType.ROAD => new Color(1, 1, 1),
    CellType.BASE => new Color(0, 1, 0),
    CellType.SPAWNER => new Color(1, 0, 0),
    _ => throw new InvalidEnumArgumentException(nameof(cell)),
  };
}