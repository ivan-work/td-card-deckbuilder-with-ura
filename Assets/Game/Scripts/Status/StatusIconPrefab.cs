using System;
using System.Security.Cryptography;
using Status;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusIconPrefab : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI textGameObject;
  [SerializeField] private Image imageGameObject;

  public bool isGoingToBeDestroyed;
  
  public void onChangeListener(StatusStruct status) {
    textGameObject.text = $"{status.stacks}";
    imageGameObject.sprite = status.data.icon;
  }
  
  public void onRemoveListener() {
    isGoingToBeDestroyed = true;
    Destroy(gameObject);
  }
}
