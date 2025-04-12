using System;
using System.Linq;
using System.Xml;
using B83;
using Components;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

[CreateAssetMenu(menuName = "Architecture/Targeting/Condition/ComponentTargetCondition")]
public class ComponentTargetCondition : AbstractTargetCondition {
  [CanBeNull] public SerializableMonoScript<ITargetableComponent> component;

  public override bool isValidTarget(GridSystem.GridSystem gridSystem, Vector2Int gridPos) {
    if (component is {Type: { }}) {
      return gridSystem.GetGridEntities(gridPos).Any(entity => entity.GetComponent(component.Type));
    }

    return false;
  }
}
