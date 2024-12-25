using UnityEngine;

namespace Effects.EffectAnimations {
  public class MoveAttemptAnimation : BaseAnimation {
    private float time = 0;
    private float duration = 0.3f;
    private readonly Vector3 sourcePosition;
    private readonly Vector3 targetPosition;
    private readonly MoveComponent component;

    public MoveAttemptAnimation(MoveComponent component, Vector3 sourcePosition, Vector3 targetPosition) {
      this.component = component;
      this.sourcePosition = sourcePosition;
      this.targetPosition = targetPosition;
    }

    public override bool animate() {
      time += Time.deltaTime;
      var percents = time / duration;
      if (percents < .5f) {
        component.gameObject.transform.position = Vector3.Lerp(sourcePosition, targetPosition, percents);
      } else { 
        component.gameObject.transform.position = Vector3.Lerp(targetPosition, sourcePosition, percents);
      }

      return percents < 1;
    }
  }
}
