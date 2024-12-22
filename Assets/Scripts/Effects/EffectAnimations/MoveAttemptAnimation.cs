using UnityEngine;

namespace Effects.EffectAnimations {
  public class MoveAttemptAnimation : BaseAnimation {
    private float time = 0;
    private float duration = 0.3f;
    public Vector3 sourcePosition;
    public Vector3 targetPosition;
    public MoveComponent component;

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
