using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour {
  [SerializeField] CardPrefab prefab;
  private List<CardPrefab> cardViews = new();

  private void Awake() {
    EventManager.CardDraw.AddListener(OnCardDraw);
    EventManager.CardDiscard.AddListener(OnCardDiscard);
  }

  private void OnCardDraw(Card card) {
    var instance = Instantiate(prefab, transform);
    instance.card = card;
    cardViews.Add(instance);
  }

  private void Update() {
    for (int i = 0; i < cardViews.Count; i++) {
      cardViews[i].transform.localPosition = new Vector3(2.2f * i, 0, cardViews[i].transform.localPosition.z);
    }
  }

  private void OnCardDiscard(Card card) {
    var cardView = cardViews.Find(instance => instance.card == card);
    if (cardView) {
      cardViews.Remove(cardView);
      Destroy(cardView.gameObject);
    }
  }
}
