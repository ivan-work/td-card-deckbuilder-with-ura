using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }

    EventManager.EndTurn.AddListener(() => turn++);
  }

  public List<Card> deck = new List<Card>();
  public List<Card> hand = new List<Card>();
  public List<Card> discard = new List<Card>();
  public int turn = 0;


  void Start() {

    Debug.Log("I'm ready");

    for (int i = 0; i < 5; i++) {
      deck.Add(new SlashCard());
      deck.Add(new PierceCard());
      deck.Add(new ArcherTowerCard());
      deck.Add(new SwordsmanTowerCard());
    }

    EventManager.OnDeckUpdate();

    Debug.Log($"{deck.Count} [{string.Join(",", deck)}]");

    DrawHand();

    EventManager.OnDeckUpdate();

    Debug.Log($"{deck.Count} [{string.Join(",", deck)}]");
  }

  void DrawHand() {
    for (int i = 0; i < 4; i++) {
      Card card = deck.First();
      deck.RemoveAt(0);
      hand.Add(card);
    }
  }
}