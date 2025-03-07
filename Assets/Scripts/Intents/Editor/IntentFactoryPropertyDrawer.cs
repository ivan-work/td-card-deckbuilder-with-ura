using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Intents.Editor {
  [Serializable]
  [CustomPropertyDrawer(typeof(IntentFactory))]
  public class IntentSpawnerPropertyDrawer : PropertyDrawer {
    private const string IntentBehaviourPropertyName = "Behaviour";
    private const string IntentValuesPropertyName = "Values";
    private const string IntentValuesDataFieldName = "IntentValuesDataField";

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
      var root = new Foldout();

      root.text = property.displayName;
      root.BindProperty(property);

      var intentBehaviourProperty = property.FindPropertyRelative(IntentBehaviourPropertyName);

      Debug.Log($"property [{property.serializedObject.targetObject}]");
      Debug.Log($"property [{JsonUtility.ToJson(property.serializedObject.targetObject)}]");
      Debug.Log($"intentBehaviourProperty2 [{property.serializedObject.FindProperty("Behaviour")}]");
      Debug.Log($"intentBehaviourProperty [{intentBehaviourProperty}]");

      var intentBehaviourField = new ObjectField {
        label = intentBehaviourProperty?.displayName ?? "NEW PROPERTY",
        objectType = typeof(IntentBehaviour),
        bindingPath = intentBehaviourProperty?.propertyPath ?? IntentBehaviourPropertyName
      };

      // Debug.Log($"{IntentValuesPropertyName}: [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");

      intentBehaviourField.TrackPropertyValue(intentBehaviourProperty, (_) => OnChangedIntentData(root, property));

      root.Add(intentBehaviourField);

      RecreatePropertyValue(root, property);

      // Debug.Log($"{IntentValuesPropertyName}:2 [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");

      return root;
    }

    private void OnChangedIntentData(VisualElement root, SerializedProperty property) {
      var intentDataProperty = property.FindPropertyRelative(IntentBehaviourPropertyName);
      var intentValuesProperty = property.FindPropertyRelative(IntentValuesPropertyName);
      var intentData = intentDataProperty.objectReferenceValue as IntentBehaviour;
      if (intentData != null) {
        var copiedDefaultValues = intentData.BaseDefaultValues.Clone();
        intentValuesProperty.managedReferenceValue = copiedDefaultValues;
        // Debug.Log($"OnChangedIntentData: [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");
      } else {
        intentValuesProperty.managedReferenceValue = null;
      }

      intentValuesProperty.serializedObject.ApplyModifiedProperties();
      RecreatePropertyValue(root, property);
    }

    private void RecreatePropertyValue(VisualElement root, SerializedProperty property) {
      // Debug.Log($"RecreatePropertyValue: [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");
      var previousIntentValuesDataField = root.Q<VisualElement>(IntentValuesDataFieldName);
      if (previousIntentValuesDataField != null) {
        root.Remove(previousIntentValuesDataField);
      }

      var intentValuesProperty = property.FindPropertyRelative(IntentValuesPropertyName);
      var intentValuesDataField = new PropertyField {
        name = IntentValuesDataFieldName,
      };
      intentValuesDataField.BindProperty(intentValuesProperty);
      root.Add(intentValuesDataField);
      intentValuesDataField.Focus();
    }
  }
}
