using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/CountdownStatusData")]
  public class CountdownStatusData : BaseStatusData {
    public override void OnEndTurn(StatusContext context) {
      context.Component.updateStatus(context.StatusStruct, -1);
    }
  }
}
