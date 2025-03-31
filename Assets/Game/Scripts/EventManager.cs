using Abilities;
using Architecture;
using Intents;
using UnityEngine;
using UnityEngine.Events;

public class EventManager: Singleton<EventManager> {
  public readonly UnityEvent<GameObject, Ability> StartAbility = new();
  public readonly UnityEvent<Card> CardDraw = new();
  public readonly UnityEvent<Card> CardDiscard = new();
  
  public readonly CoroutineList PhaseActionFast = new ();
  public readonly CoroutineList PhaseMove = new ();
  public readonly CoroutineList PhaseActionSlow = new ();

  public readonly UnityEvent PhaseGetIntents = new();
  public readonly UnityEvent PhasePlayerIntent = new();
  public readonly UnityEvent PhaseApplyEffects = new();
  
  public readonly UnityEvent<IntentSystem> ImsStartRequestIntent = new();
  public readonly UnityEvent<IntentSystem> ImsEndTurn = new();

  public readonly UnityEvent AbilityTargetingStart = new();
  public readonly UnityEvent AbilityTargetingStop = new();
}
