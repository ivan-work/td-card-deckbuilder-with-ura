﻿using System;
using System.Linq;
using System.Xml;
using B83;
using Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

[CreateAssetMenu(menuName = "Architecture/Targeting/Condition/ComponentTargetCondition")]
public class ComponentTargetCondition : AbstractTargetCondition {
  public SerializableMonoScript<ITargetableComponent> component;

  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return gridSystem.getGridEntities(gridPos).Any(entity => entity.GetComponent(component.Type));
  }
}
