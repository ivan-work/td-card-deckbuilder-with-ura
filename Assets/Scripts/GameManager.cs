using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
TODO:
Валидация целей - опознавание кто на клетке, подсветочка

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

    Debug.Log("I'm ready");

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


  void Start() {
  }

  void DrawHand() {
    for (int i = 0; i < 4; i++) {
      Card card = deck.First();
      deck.RemoveAt(0);
      hand.Add(card);
    }
  }

  public void EndTurn() {
    EventManager.PhaseTowerAction.Invoke();
    EventManager.PhaseMobAction.Invoke();
   
    EventManager.EndTurn.Invoke();
  }
}