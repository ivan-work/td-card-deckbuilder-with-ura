using System.Diagnostics.CodeAnalysis;
using UnityEditor;

namespace System {
  public static class NullableHelper {
    public static void ThrowWhenNull<T>([NotNull] T? value) {
      if (value is null) throw new ArgumentNullException(nameof(value), $"{typeof(T)} is null");
    }
  }
}
