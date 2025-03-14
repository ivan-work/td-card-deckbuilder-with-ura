using System;
using System.ComponentModel;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType {
  Empty = 0,
  Road = 1,
  Base = 2,
  Spawner = 3,
}

public class CellPrefab : MonoBehaviour {
  [SerializeField] public CellType cellType = CellType.Empty;
  [SerializeField] private TextMeshPro? _distanceText;

  public CellPrefab OnSpawn(CellType _cellType) {
    cellType = _cellType;
    this.GetAssertComponentInChildren<MeshRenderer>().material.color = getColor(cellType);
    return this;
  }

  Color getColor(CellType cell) => cell switch {
    CellType.Empty => new Color(.5f, .5f, .5f),
    CellType.Road => new Color(1, 1, 1),
    CellType.Base => new Color(0, 1, 0),
    CellType.Spawner => new Color(1, 0, 0),
    _ => throw new InvalidEnumArgumentException(nameof(cell)),
  };
  

  private void Start() {
    if (TryGetComponent(out PathComponent pathComponent) && _distanceText != null) {
      _distanceText.text = $"{pathComponent.distanceToBase}";
    }
  }
}
