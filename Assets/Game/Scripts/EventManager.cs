using Abilities;
using Architecture;
using Intents;
using UnityEngine;
using UnityEngine.Events;

public class EventManager: Singleton<EventManager> {
  public readonly UnityEvent<GameObject, Ability> StartAbility = new();
  public readonly UnityEvent<Card> CardDraw = new();
  public readonly UnityEvent<Card> CardDiscard = new();

  public readonly UnityEvent PhaseCreateIntents = new();
  public readonly UnityEvent PhasePerformIntents = new();
  
  public readonly UnityEvent<IntentSystem> ImsStartPlayerTurn = new();
  public readonly UnityEvent<IntentSystem> ImsEndTurn = new();
  public readonly UnityEvent<IntentSystem> ImsWriteIntents = new();
  
  public readonly UnityEvent AbilityTargetingStart = new();
  public readonly UnityEvent AbilityTargetingStop = new();
}
