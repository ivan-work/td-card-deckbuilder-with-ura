using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/*
TODO:

*/

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }
  [SerializeField] public List<Card> deck;
  public List<Card> hand = new();
  public List<Card> discard = new(); // #TODO
  public int turn = 0;

  const int HandSize = 4;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }

    EventManager.Instance.CardDraw.AddListener(OnCardDraw);
    EventManager.Instance.CardDiscard.AddListener(OnCardDiscard);
    EventManager.Instance.PhaseCreateIntents.AddListener(onPhaseCreateIntents);
    EventManager.Instance.PhasePerformIntents.AddListener(OnPhasePerformIntents);
  }

  private void Start() {
    DrawHand();
    EventManager.Instance.PhaseCreateIntents.Invoke();
  }

  public void DrawHand() {
    foreach (var card in new List<Card>(hand)) {
      EventManager.Instance.CardDiscard.Invoke(card);
    }
    
    for (var i = 0; i < HandSize; i++) {
      var card = deck.First();
      EventManager.Instance.CardDraw.Invoke(card);
    }
  }

  private void OnCardDraw(Card card) {
    deck.RemoveAt(0);
    hand.Add(card);
  }

  private void OnCardDiscard(Card card) {
    hand.Remove(card);
    deck.Add(card);
  }

  private void onPhaseCreateIntents() {

  }
  
  private void OnPhasePerformIntents() {
    
  }
}











