using Effects.EffectAnimations;
using Unity.VisualScripting;
using UnityEngine;

namespace Effects {
  public abstract class SimpleComponentEffect : BaseEffect {
    private readonly MonoBehaviour component;
    private BaseAnimation _animation;

    protected BaseAnimation animation {
      get => _animation;
      set {
        isActive = true;
        _animation = value;
      }
    }

    protected SimpleComponentEffect(MonoBehaviour component) {
      this.component = component;
    }

    protected override void animate() {
      if (animation == null || component.IsDestroyed()) {
        isActive = false;
      } else {
        isActive = animation.animate();
      }
    }
  }
}
