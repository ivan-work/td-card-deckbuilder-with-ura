using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour, IPointerClickHandler {
  public void OnPointerClick(PointerEventData eventData) {
    EventManager.PhasePlayerIntent.Invoke();
  }
}
