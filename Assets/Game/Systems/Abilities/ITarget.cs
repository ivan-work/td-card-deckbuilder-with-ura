using UnityEngine;

namespace Abilities {
  public interface ITarget {
    public void Highlight(bool isEnable, bool isValid);
    
    public Vector2Int GetLoc();
    
    public GameObject GetGameObject();

    public bool IsCell { get; }
  }
}
