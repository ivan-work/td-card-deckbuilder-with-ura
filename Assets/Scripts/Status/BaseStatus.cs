using Components;
using UnityEngine;

namespace Status {
  public abstract class BaseStatus  {
    protected int count;
    
    public virtual void OnMove(StatusContext context){}
}
}
