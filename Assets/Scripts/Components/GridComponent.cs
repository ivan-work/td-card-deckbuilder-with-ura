using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class GridComponent : MonoBehaviour {
    public enum Layer
    {
        Ground,
        Entity,
        Mob
    };

  [SerializeField] public Layer layer;
  [SerializeField] public GridSystem gridSystem;

  [SerializeField] public Vector2Int gridPos = Vector2Int.zero;
  private Vector2Int prevGridPos = Vector2Int.zero;

  private void Awake() {
    if (GetComponentInParent<GridSystem>()) {
      gridSystem = GetComponentInParent<GridSystem>();
    } else {
      throw new NoComponentException($"No component ${typeof (GridSystem)}");
    }
  }

  private void OnEnable() {
    gridSystem.register(this, this.gridPos);
  }

  private void OnDisable() {
    gridSystem.unregister(this, this.gridPos);
  }

  public void moveTo(Vector2Int gridPos) {
    gridSystem.moveTo(this, gridPos);
  }

  private void Update() {
    if (prevGridPos != gridPos) {
      moveTo(gridPos);
      prevGridPos = gridPos;
    }
    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gridPos2World(gridPos), Time.deltaTime);
  }

  public Vector3 gridPos2World(Vector2Int vector) {
    Vector3 worldPosition = gridSystem.grid.GetCellCenterWorld(new Vector3Int(vector.x, vector.y));
    worldPosition.z = gameObject.transform.position.z;
    return worldPosition;
  }
}