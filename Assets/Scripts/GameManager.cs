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
  public bool watchPlayersActions;
  const int HandSize = 4;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }

    EventManager.CardDraw.AddListener(OnCardDraw);
    EventManager.CardDiscard.AddListener(OnCardDiscard);
    EventManager.PhaseGetIntents.AddListener(OnPhaseGetIntents);
    EventManager.PhasePlayerIntent.AddListener(OnPhasePlayerIntent);
    EventManager.PhaseApplyEffects.AddListener(OnPhaseApplyEffects);
  }

  private void Start() {
    DrawHand();
    EventManager.PhaseGetIntents.Invoke();
  }

  public void DrawHand() {
    foreach (var card in new List<Card>(hand)) {
      EventManager.CardDiscard.Invoke(card);
    }
    
    for (var i = 0; i < HandSize; i++) {
      var card = deck.First();
      EventManager.CardDraw.Invoke(card);
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

  private void OnPhaseGetIntents() {
    watchPlayersActions = true;
  }

  private void OnPhasePlayerIntent() {
    watchPlayersActions = false;
    EventManager.PhaseApplyEffects.Invoke();
  }

  private void OnPhaseApplyEffects() {
  }
}











