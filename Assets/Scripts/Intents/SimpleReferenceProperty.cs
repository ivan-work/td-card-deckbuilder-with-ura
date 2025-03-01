using System;
using System.Diagnostics;
using UnityEngine;

namespace Intents {
  [Conditional("UNITY_EDITOR")]
  [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
  public class SimpleReferenceProperty : PropertyAttribute { }
}
