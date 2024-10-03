using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
  SPELL, TOWER
}

public class Card {
  public string name {get; set; }
  public CardType type {get; set; }
  public Effect effect {get; set; }
}

public class Effect {
}

public class DamageEffect : Effect {
  int value;

  public DamageEffect(int _value) {
    value = _value;
  }
}

public class BuildEffect : Effect {
  string towerType;
}

public class CardPrefab : MonoBehaviour {
  [SerializeField] Card card;
}

public class CardStorage {
    private static CardStorage instance;

    public Dictionary<string, Card> cardDictionary = new Dictionary<string, Card> {
      { 
        "strike", new Card { 
          name = "Strike",
          type = CardType.SPELL,
          effect = new DamageEffect(3)
        }
      },
      { 
        "pierce", new Card { 
          name = "Pierce",
          type = CardType.SPELL,
          effect = new DamageEffect(1)
        }
      },
    };
 
    private CardStorage() {
    }
 
    public static CardStorage getInstance()
    {
        if (instance == null)
            instance = new CardStorage();
        return instance;
    }
}