using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Hand : MonoBehaviour {
  [SerializeField] CardPrefab prefab;

  // Start is called before the first frame update
  void Start() {
    // List<Card> cards = GameManager.Instance.hand;
    List<Card> cards = new List<Card>();
    cards.Add(new SlashCard());
    cards.Add(new PierceCard());
    cards.Add(new ArcherTowerCard());
    cards.Add(new SwordsmanTowerCard());
    List<GameObject> cardObjects = new List<GameObject>();
    var i = 0;
    foreach (var card in cards) {
      var instance = CardPrefab.Instantiate(prefab);
      instance.card = card;
      instance.transform.position = gameObject.transform.position + new Vector3(4 * ++i, 0);
      instance.OnInstantiate();
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
