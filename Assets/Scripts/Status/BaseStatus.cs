using Components;
using Effects;
using UnityEngine;

namespace Status {
  public abstract class BaseStatus  {
    protected int count;
    
    public virtual void OnMove(StatusContext context){}
    public virtual void OnEndTurn(StatusContext context){}
    public virtual void OnDamage(StatusContext context, DamageEffect damageEffect) {}
  }
}
