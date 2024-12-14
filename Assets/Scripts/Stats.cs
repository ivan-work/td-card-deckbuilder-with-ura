using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      EventManager.PhasePlayerIntent.AddListener(() => GetComponent<TextMeshPro>().text = $"Turn {GameManager.Instance.turn}");  
    }
}
