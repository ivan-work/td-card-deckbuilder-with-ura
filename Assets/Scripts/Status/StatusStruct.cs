using Status.StatusData;

namespace Status {
  public class StatusStruct {
    public readonly BaseStatusData data;
    public int stacks;

    public StatusStruct(BaseStatusData data, int stacks) {
      this.data = data;
      this.stacks = stacks;
    }
  }
}
