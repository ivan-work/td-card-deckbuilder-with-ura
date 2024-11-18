using System;
using System.Collections.Generic;
using UnityEngine;

public class CellIndicatorObjectPool : MonoBehaviour {
  public static CellIndicatorObjectPool SharedInstance;
  public List<GameObject> pooledObjects;
  public GameObject objectToPool;
  public int amountToPool;

  private void Awake() {
    SharedInstance = this;
  }

  private void Start() {
    pooledObjects = new List<GameObject>();
    GameObject tmp;
    for (int i = 0; i < amountToPool; i++) {
      tmp = Instantiate(objectToPool);
      tmp.SetActive(false);
      pooledObjects.Add(tmp);
    }
  }

  public GameObject getPooledObject() {
    for (var i = 0; i < amountToPool; i++) {
      if (!pooledObjects[i].activeInHierarchy) {
        pooledObjects[i].SetActive(true);
        return pooledObjects[i];
      }
    }
    
    return null;
  }

  public void reset() {
    pooledObjects.ForEach(pooledObject => pooledObject.SetActive(false));
  }
}