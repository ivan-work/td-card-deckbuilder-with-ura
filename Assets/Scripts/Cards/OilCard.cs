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
  [CreateAssetMenu(menuName = "Card/OilCard")]
  public class OilCard : Card {
    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      return gridPoses
        .SelectMany(gridSystem.getGridEntitiesSpecial<StatusComponent>)
        .Select(component =>
          new ApplyStatusEffect(component, new StatusStruct(
            AssetDatabase.LoadAssetAtPath<BaseStatusData>("Assets/ScriptableObjects/Status/Oiled Status Data Object.asset"),
            5
          ))
        );
    }
  }
}
