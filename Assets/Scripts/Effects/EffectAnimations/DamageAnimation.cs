using System;
using Effects.EffectAnimations;
using UnityEngine;
using Object = UnityEngine.Object;

public class DamageAnimation : BaseAnimation {
  private readonly Animator animator;

  public DamageAnimation(Vector3 position, DamageType damageType) {
    var sprite = Object.Instantiate(Resources.Load<GameObject>("Aseprite/slash4"), position, Quaternion.identity);
    animator = sprite.GetComponent<Animator>();
    sprite.GetComponent<SpriteRenderer>().color = damageTypeToColor(damageType);
  }

  public override bool animate() {
    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
      Object.Destroy(animator.gameObject);
      return false;
    }

    return true;
  }

  private Color damageTypeToColor(DamageType damageType) {
    return damageType switch {
      DamageType.Pierce => Color.white,
      DamageType.Slash => Color.gray,
      DamageType.Blunt => Color.black,
      DamageType.Fire => new Color(1f, .5f, 0f),
      DamageType.Cold => Color.blue,
      DamageType.Poison => new Color(0f, .5f, 0f),
      DamageType.Acid => new Color(.5f, 1f, 0f),
      DamageType.Electric => Color.cyan,
      DamageType.Radioactive => Color.magenta,
      DamageType.Internal => Color.red,
      _ => Color.gray,
    };
  }
}
