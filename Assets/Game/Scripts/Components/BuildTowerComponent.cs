using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildTowerComponent : MonoBehaviour {
  [FormerlySerializedAs("turns")] [SerializeField] private int maxTurns = 3;
  [SerializeField] public TextMeshPro buildingLabel;
  
  private int currentTurn = 1;
  private void OnEnable() {
    Debug.Log("BuildTowerComponent.OnEnable()");
  }
  
  private void OnDisable() {
    Debug.Log("BuildTowerComponent.OnDisable");
  }

  private void Start() {
    updateBuildingLabel();
  }

  public void makeProgress() {
    currentTurn += 1;
    if (currentTurn >= maxTurns) {
      enabled = false;
      buildingLabel.enabled = false;
      GetComponent<TowerComponent>().enabled = true;
    } else {
      updateBuildingLabel();
    }
  }

  private void updateBuildingLabel() {
    buildingLabel.text = $"{currentTurn}/{maxTurns}";
  }
}
