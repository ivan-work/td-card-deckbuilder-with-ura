using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
  public void OnBeginDrag(PointerEventData eventData) {
    // gameObject.transform.localScale = new Vector3(.1f, .1f);
    var color = gameObject.GetComponentInChildren<SpriteRenderer>().color;
    color.a = 0.1f;
    gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
  }

  public void OnDrag(PointerEventData eventData) {
    var position = Camera.main.ScreenToWorldPoint(eventData.position);
    position.z = -2;
    gameObject.transform.position = position;

    Debug.Log($"{eventData.position} {position}");

    // var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero);
    // Debug.Log($"{raycast}, ${Input.mousePosition} ${eventData.position}");
    // if (raycast) {
    //   Debug.Log($"{raycast}");
    //   var target = raycast.collider.gameObject;
    //   target.GetComponent<SpriteRenderer>().color = Color.green;
    // }
  }

  public void OnEndDrag(PointerEventData eventData) {
    // gameObject.transform.localScale = new Vector3(1f, 1f);
    var color = gameObject.GetComponentInChildren<SpriteRenderer>().color;
    color.a = 1f;
    gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
  }

  public void OnPointerClick(PointerEventData eventData) {
    // Debug.Log("Nu choto rabotaet");
    // gameObject.transform.position = new Vector3(5, 5, 0);
  }
}