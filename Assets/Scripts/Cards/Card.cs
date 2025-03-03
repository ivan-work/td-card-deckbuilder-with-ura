using System;
using System.Collections.Generic;
using Effects;
using Intents.Engine;
using UnityEngine;

public abstract class Card : ScriptableObject {
  [SerializeField] public string cardName;
  [SerializeField] public TargetModesHelper.TargetMode targetMode;
  [SerializeField] public AbstractTargetCondition[] targetCondition;

  public abstract void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses);
}
