using System.Collections.Generic;
using UnityEngine;

public class CellIndicatorObjectPool : MonoBehaviour {
  public static CellIndicatorObjectPool SharedInstance;
  public List<GameObject> pooledObjects;
  public GameObject objectToPool;
  public int amountToPool;

  void Awake() {
    SharedInstance = this;
  }

  void Start() {
    pooledObjects = new List<GameObject>();
    GameObject tmp;
    for (int i = 0; i < amountToPool; i++) {
      tmp = Instantiate(objectToPool);
      tmp.SetActive(false);
      pooledObjects.Add(tmp);
    }
  }

  public GameObject GetPooledObject() {
    for (int i = 0; i < amountToPool; i++) {
      if (!pooledObjects[i].activeInHierarchy) {
        pooledObjects[i].SetActive(true);
        return pooledObjects[i];
      }
    }
    
    return null;
  }
}