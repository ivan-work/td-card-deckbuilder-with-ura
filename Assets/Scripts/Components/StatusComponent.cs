using Status;
using UnityEngine;

namespace Components {
  public class StatusComponent : MonoBehaviour {
    public GridComponent gridComponent;
    public BaseStatus status;
    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
    }

    public void OnMove(StatusContext context) {
      status.OnMove(context);
    }
  }
}
