using UnityEngine;
using System.Collections;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class AssertComponents {
  //Even though they are used like normal methods, extension
  //methods must be declared static. Notice that the first
  //parameter has the 'this' keyword followed by a Transform
  //variable. This variable denotes which class the extension
  //method becomes a part of.
  public static T GetAssertComponent<T>(this MonoBehaviour target) {
    if (target.TryGetComponent(out T component)) {
      return component;
    }
    throw new NoComponentException($"No component ${typeof(T)}");
  }

  public static T GetAssertComponentInParent<T>(this MonoBehaviour target) {
    var component = target.GetComponentInParent<T>() ?? throw new NoComponentException($"No component ${typeof(T)}");
    return component;
  }
}