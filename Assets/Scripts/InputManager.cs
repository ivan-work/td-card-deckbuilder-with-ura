using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour {
  [SerializeField] Camera sceneCamera;

  private Vector3 lastPosition = new Vector3(5, 5, -1);

  [SerializeField] private LayerMask placementLayermask;

  public Vector3 GetSelectedMapPosition(Vector3 mousePosition) {
    mousePosition.z = sceneCamera.nearClipPlane;
    Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayermask)) {
      lastPosition = hit.point;
    }
    return lastPosition;
  }
}
