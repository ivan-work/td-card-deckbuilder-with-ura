using Status.StatusData;
using UnityEngine.Events;

namespace Status {
  public class StatusStruct {
    public readonly BaseStatusData data;
    public int stacks;
    public UnityEvent<StatusStruct> OnChange = new();
    public UnityEvent OnRemove = new();

    public StatusStruct(BaseStatusData data, int stacks) {
      this.data = data;
      this.stacks = stacks;
    }
  }
}
