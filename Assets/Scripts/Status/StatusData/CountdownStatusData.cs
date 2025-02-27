using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/CountdownStatusData")]
  public class CountdownStatusData : BaseStatusData {
    public override void OnEndTurn(StatusContext context) {
      context.component.updateStatus(context.statusStruct, -1);
    }
  }
}
