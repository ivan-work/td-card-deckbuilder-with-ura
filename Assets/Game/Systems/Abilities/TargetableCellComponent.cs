using Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abilities {
  public class TargetableCellComponent : TargetableUnitComponent {
    public override bool IsCell => true;
  }
}
