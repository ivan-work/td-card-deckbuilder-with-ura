using System.Collections.Generic;

public interface IHasIntent {
  IEnumerable<BaseEffect> getIntents();
}