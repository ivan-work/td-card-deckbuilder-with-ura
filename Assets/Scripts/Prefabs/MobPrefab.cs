using System;
using System.Collections;
using System.Linq;
using Components;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobPrefab : MonoBehaviour {
  HealthComponent healthComponent;

  void Awake() {
    healthComponent = GetComponent<HealthComponent>();
  }

  private void OnDestroy() {
  }

  void Update() {
    if (healthComponent) {
      GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
    }
  }
}