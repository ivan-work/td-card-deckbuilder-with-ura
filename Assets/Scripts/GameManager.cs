using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*
TODO:

*/

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }
  [SerializeField] public List<Card> deck;
  public List<Card> hand = new();
  public List<Card> discard = new(); // #TODO
  public int turn = 0;
  public bool isBusy;
  const int HandSize = 4;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }

    EventManager.EndTurn.AddListener(() => turn++);
    EventManager.CardDraw.AddListener(OnCardDraw);
    EventManager.CardDiscard.AddListener(OnCardDiscard);
  }

  private void Start() {
    DrawHand();
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

  public void EndTurn() {
    Debug.Log("GameManager.EndTurn()");
    if (!isBusy) StartCoroutine(EndTurnCoroutine());
  }

  private IEnumerator EndTurnCoroutine() {
    isBusy = true;
    Debug.Log($"Turn({turn}): Fast");
    yield return StartCoroutine(EventManager.PhaseActionFast.Invoke(this));
    Debug.Log($"Turn({turn}): Move");
    yield return StartCoroutine(EventManager.PhaseMove.Invoke(this));
    Debug.Log($"Turn({turn}): Slow");
    yield return StartCoroutine(EventManager.PhaseActionSlow.Invoke(this));
    isBusy = false;
    turn++;
    yield return null;
  }
}











