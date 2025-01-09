using Effects;
using UnityEngine;
namespace Status {
  public class BurningStatus: BaseStatus {
    
    public BurningStatus(int count) {
      this.count = count;
    }
    
    
    public override void OnEndTurn (StatusContext context) {
      context.actorManager.addImmediateEffects(new DamageEffect(context.component.gridComponent.gridPos, DamageType.Fire, count));
      count--;
      if (count <= 0) {
        Object.DestroyImmediate(context.component);
      }
    }
    
    private void onApply(){}
  }
}
