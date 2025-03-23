using System;
using System.ComponentModel;
using System.Collections.Generic;
using Abilities;
using Components;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType {
  Empty = 0,
  Road = 1,
  Base = 2,
  Spawner = 3,
}

public class CellPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ITarget {
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
    // if (TryGetComponent(out PathComponent pathComponent) && _distanceText != null) {
    //   _distanceText.text = $"{pathComponent.distanceToBase}";
    // }
    if (TryGetComponent(out GridComponent gridComponent) && _distanceText != null) {
      var loc = GridSystem.OffsetToAxial(gridComponent.gridLoc);
      _distanceText.text = $"{gridComponent.gridLoc.x}:{gridComponent.gridLoc.y}\n{loc.x}:{loc.y}";
    }
  }

  public void OnPointerClick(PointerEventData eventData) {
    AbilityManager.Instance.ConfirmTarget(this);
  }

  public void OnPointerEnter(PointerEventData eventData) {
    this.GetAssertComponentInChildren<MeshRenderer>().material.color = new Color(.1f, .1f, 0);
    AbilityManager.Instance.OnHoverStart(this);
  }

  public void OnPointerExit(PointerEventData eventData) {
    AbilityManager.Instance.OnHoverStop(this);
  }

  public void Highlight(bool isEnable, bool isValid) {
    if (isEnable) {
      if (isValid) {
        this.GetAssertComponentInChildren<MeshRenderer>().material.color = new Color(.5f, 1, 0);
      } else {
        this.GetAssertComponentInChildren<MeshRenderer>().material.color = new Color(1, .5f, 0);
      }
    } else {
      this.GetAssertComponentInChildren<MeshRenderer>().material.color = getColor(cellType);
    }
  }

  public Vector2Int GetLoc() {
    return GetComponent<GridComponent>().gridLoc;
  }

  public GameObject GetGameObject() {
    return gameObject;
  }

  public bool IsCell => true;
}
