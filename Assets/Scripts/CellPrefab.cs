using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellPrefab : MonoBehaviour, IPointerClickHandler, IDropHandler {
  public void OnDrop(PointerEventData eventData) {
    Debug.Log($"{eventData}");
  }

  public void OnPointerClick(PointerEventData eventData) {
    Debug.Log("CELL CLICKED");
  }
}