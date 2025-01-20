using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Effects;
using Status;
using Status.StatusData;
using UnityEditor;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/TestCard")]
  public class TestCard : Card {
    [SerializeField] public int count;

    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      // return Enumerable.Empty<BaseEffect>();
      return gridPoses
        .SelectMany(gridSystem.getGridEntitiesSpecial<StatusComponent>)
        .Select(component =>
          new ApplyStatusEffect(component, new StatusStruct(
            AssetDatabase.LoadAssetAtPath<BaseStatusData>("Assets/ScriptableObjects/Status/Bleed Status Data Object.asset"),
            count
          ))
        );
    }
  }
}
