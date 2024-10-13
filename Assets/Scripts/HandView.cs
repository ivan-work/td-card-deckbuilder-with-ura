using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class HandView : MonoBehaviour {
  [SerializeField] CardPrefab prefab;

  // Start is called before the first frame update
  void Start() {
    Debug.Log("Hand.Start()");
    List<Card> cards = GameManager.Instance.hand;
    // List<GameObject> cardObjects = new List<GameObject>();
    var i = 0;
    foreach (var card in cards) {
      var instance = Instantiate(prefab);
      instance.card = card;
      instance.transform.position = gameObject.transform.position + new Vector3(4 * ++i, 0);
      instance.OnInstantiate();
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
