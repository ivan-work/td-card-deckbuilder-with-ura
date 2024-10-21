using System.Collections;
using UnityEngine;

public class BulletPrefab : MonoBehaviour {
  public int speed = 4;

  public IEnumerator Shoot(Vector3 targetPosition) {
    transform.up = gameObject.transform.position - targetPosition;
    // transform.LookAt(targetPosition, Vector3.back);
    
    yield return CorouTweens.LerpWithSpeed(
      gameObject.transform.position,
      targetPosition,
      speed,
      (value) => transform.position = value
    );
    Destroy(gameObject);
  }
}