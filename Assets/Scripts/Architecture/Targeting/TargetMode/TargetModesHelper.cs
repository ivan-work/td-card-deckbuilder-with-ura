using System;

namespace Architecture.Targeting.TargetMode {
  public static class TargetModesHelper {
    public enum TargetMode {
      Single,
      Line
    }

    public static AbstractTargetMode createTargetMode(Card card) {
      return card.targetMode switch {
        TargetMode.Single => new TargetModeSingle(card),
        TargetMode.Line => new TargetModeLine(card),
        _ => throw new ArgumentOutOfRangeException()
      };
    }
  }
}
