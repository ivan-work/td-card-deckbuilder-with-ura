using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Intents.Editor {
  
  [Serializable]
  [CustomPropertyDrawer(typeof(IntentSpawner))]
  public class IntentSpawnerPropertyDrawer : PropertyDrawer {
    [SerializeField] private VisualTreeAsset VisualTree;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
      var root = new VisualElement();
      
      VisualTree.CloneTree(root);
      
      return root;
    }


    // [SerializeField] private VisualTreeAsset VisualTree;
    // public override VisualElement CreateInspectorGUI() {
    //   var root = new VisualElement();
    //
    //   VisualTree.CloneTree(root);
    //   
    //   return root;
    // }
  }
}
