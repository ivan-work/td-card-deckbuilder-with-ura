using System;
using System.ComponentModel;
using System.Collections.Generic;
using Abilities;
using Components;
using GridSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType {
  Empty = 0,
  Road = 1,
  Base = 2,
  Spawner = 3,
}

[SelectionBase]
public class CellPrefab : MonoBehaviour {
  [SerializeField] public CellType CellType = CellType.Empty;
  [SerializeField] private TextMeshPro? _distanceText;

  public CellPrefab OnSpawn(CellType cellType) {
    CellType = cellType;
    this.GetAssertComponentInChildren<MeshRenderer>().material.color = getColor(this.CellType);
    return this;
  }

  private static Color getColor(CellType cell) => cell switch {
    CellType.Empty => new Color(.5f, .5f, .5f),
    CellType.Road => new Color(1, 1, 1),
    CellType.Base => new Color(0, 1, 0),
    CellType.Spawner => new Color(1, 0, 0),
    _ => throw new InvalidEnumArgumentException(nameof(cell)),
  };


  private void Start() {
    if (TryGetComponent(out GridComponent gridComponent) && _distanceText != null) {
      var loc = GridSystem.GridSystem.OffsetToAxial(gridComponent.GridLoc);
      _distanceText.text = $"{gridComponent.GridLoc.x}:{gridComponent.GridLoc.y}\n{loc.x}:{loc.y}";
    }
  }
}
