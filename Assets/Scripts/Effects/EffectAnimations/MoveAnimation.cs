using UnityEngine;

namespace Effects.EffectAnimations {
  public class MoveAnimation : BaseAnimation {
    private float time = 0;
    private float duration = 0.3f;
    private readonly MoveComponent component;
    private readonly Vector3 sourcePosition;
    private readonly Vector3 targetPosition;

    public MoveAnimation(MoveComponent component, Vector3 sourcePosition, Vector3 targetPosition) {
      this.sourcePosition = sourcePosition;
      this.targetPosition = targetPosition;
      this.component = component;
    }

    public override bool animate() {
      time += Time.deltaTime;
      float percents = time / duration;
      component.gameObject.transform.position = Vector3.Lerp(sourcePosition, targetPosition, percents);


      return percents < 1;
    }
  }
}
