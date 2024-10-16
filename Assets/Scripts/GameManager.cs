using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
TODO:

*/

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }

    EventManager.EndTurn.AddListener(() => turn++);

    EventManager.OnDeckUpdate();

    // Debug.Log($"{deck.Count} [{string.Join(",", deck)}]");

    DrawHand();

    EventManager.OnDeckUpdate();

    // Debug.Log($"{deck.Count} [{string.Join(",", deck)}]");
  }

  [SerializeField] public List<Card> deck;
  public List<Card> hand = new List<Card>();
  public List<Card> discard = new List<Card>();
  public int turn = 0;
  private bool isBusy;

  private void DrawHand() {
    for (var i = 0; i < 4; i++) {
      var card = deck.First();
      deck.RemoveAt(0);
      hand.Add(card);
    }
  }

  public void EndTurn() {
    if (isBusy) StartCoroutine(EndTurnCoroutine());
  }

  private IEnumerator EndTurnCoroutine() {
    isBusy = true;
    EventManager.PhaseTowerAction.Invoke();
    yield return new WaitForSeconds(1);
    EventManager.PhaseMobAction.Invoke();
    yield return new WaitForSeconds(1);
    EventManager.EndTurn.Invoke();
    yield return new WaitForSeconds(1);
    isBusy = false;
  }
}