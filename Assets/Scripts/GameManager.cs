using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card {
  public string name { get; }

  public Card(string _name) {
    name = _name;
  }
}

public class SlashCard : Card {
  public SlashCard() : base("SlashCard") { }
}

public class PierceCard : Card {
  public PierceCard() : base("PierceCard") { }
}

public class ArcherTowerCard : Card {
  public ArcherTowerCard() : base("ArcherTowerCard") { }
}

public class SwordsmanTowerCard : Card {
  public SwordsmanTowerCard() : base("SwordsmanTowerCard") { }
}

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }

  private void Awake() {
    // If there is an instance, and it's not me, delete myself.

    if (Instance != null && Instance != this) {
      Destroy(this);
    } else {
      Instance = this;
    }
  }

  public List<Card> deck = new List<Card>();
  public List<Card> hand = new List<Card>();
  public List<Card> discard = new List<Card>();

  void Start() {
    Debug.Log("I'm ready");

    for (int i = 0; i < 5; i++) {
      deck.Add(new SlashCard());
      deck.Add(new PierceCard());
      deck.Add(new ArcherTowerCard());
      deck.Add(new SwordsmanTowerCard());
    }


    Debug.Log($"[{string.Join(",", deck)}]");

    DrawHand();

    Debug.Log($"[{string.Join(",", deck)}]");
  }

  void DrawHand() {
    for (int i = 0; i < 4; i++) {
      Card card = deck.First();
      deck.RemoveAt(0);
      hand.Add(card);
    }
  }
}