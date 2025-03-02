using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Intents.Editor {
  [Serializable]
  [CustomPropertyDrawer(typeof(IntentCreator))]
  public class IntentSpawnerPropertyDrawer : PropertyDrawer {
    private const string IntentDataPropertyName = "IntentData";
    private const string IntentValuesPropertyName = "IntentValues";
    private const string IntentValuesDataFieldName = "IntentValuesDataField";

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
      var root = new Foldout();

      root.text = property.displayName;
      root.BindProperty(property);

      var intentDataProperty = property.FindPropertyRelative(IntentDataPropertyName);

      var intentDataField = new ObjectField {
        label = intentDataProperty.displayName,
        objectType = typeof(BaseIntentData),
        bindingPath = intentDataProperty.propertyPath
      };

      // Debug.Log($"{IntentValuesPropertyName}: [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");

      intentDataField.TrackPropertyValue(intentDataProperty, (_) => OnChangedIntentData(root, property));

      root.Add(intentDataField);

      RecreatePropertyValue(root, property);

      // Debug.Log($"{IntentValuesPropertyName}:2 [{property.FindPropertyRelative(IntentValuesPropertyName).managedReferenceValue}]");

      return root;
    }

    private void OnChangedIntentData(VisualElement root, SerializedProperty property) {
      var intentDataProperty = property.FindPropertyRelative(IntentDataPropertyName);
      var intentValuesProperty = property.FindPropertyRelative(IntentValuesPropertyName);
      var intentData = intentDataProperty.objectReferenceValue as BaseIntentData;
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
