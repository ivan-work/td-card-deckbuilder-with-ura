using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate IEnumerator CoroutineListener();

public class CoroutineList {
  private List<CoroutineListener> list = new();

  public IEnumerator Invoke(MonoBehaviour executor) {
    foreach (var item in list) {
      yield return executor.StartCoroutine(item());
    }

    yield return null;
  }

  public void AddListener(CoroutineListener listener) {
    list.Add(listener);
  }

  public void RemoveListener(CoroutineListener listener) {
    list.Remove(listener);
  }
}