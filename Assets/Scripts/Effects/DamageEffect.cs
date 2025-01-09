using System;
using System.Linq;
using Architecture;
using Components;
using Effects.EffectAnimations;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using Object = UnityEngine.Object;

public enum DamageType {
  Pierce,
  Slash,
  Blunt,
  Fire,
  Cold,
  Poison,
  Acid,
  Electric,
  Radioactive,
  Internal
}

namespace Effects {
  public class DamageEffect : BaseEffect {
    private readonly Vector2Int gridPos;
    public readonly DamageType damageType;
    public readonly int damage;
    private BaseAnimation animation;

    public DamageEffect(Vector2Int gridPos, DamageType damageType, int damage) {
      this.gridPos = gridPos;
      this.damageType = damageType;
      this.damage = damage;
    }

    public override void start(ActorManager am, GridSystem gridSystem) {
      
      gridSystem.getGridEntitiesSpecial<HealthComponent>(gridPos)
        .ToList()
        .ForEach((entityHealth) => {
          entityHealth.OnDamage(damage);
          entityHealth.GetComponents<StatusComponent>().ToList().ForEach(component => component.OnDamage(am, this));
        });

      // var sprite = Object.Instantiate(Resources.Load<GameObject>("Aseprite/slash"));
      isActive = true;
      animation = new DamageAnimation(gridSystem.gridPos2World(gridPos, -5), damageType);

    }

    protected override void animate() {
      isActive = animation.animate();
    }

    public override string ToString() {
      return $"DamageEffect({gridPos})";
    }
  }
}
