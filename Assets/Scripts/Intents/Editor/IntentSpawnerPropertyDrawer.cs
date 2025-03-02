using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Intents.Editor {
  public class IntentDataAndValuesPropertyDrawer {
    private readonly SerializedProperty property;
    private readonly Foldout foldout = new();
    private const string IntentDataFieldName = "IntentData";
    private const string IntentValuesFieldName = "ValuesReference";
    private const string IntentValuesDataFieldName = "IntentValuesDataField";

    public IntentDataAndValuesPropertyDrawer(SerializedProperty property) {
      this.property = property;
      Debug.Log($"new IntentDataAndValuesPropertyDrawer");
    }

    public VisualElement CreatePropertyGUI() {
      foldout.text = property.displayName;
      foldout.BindProperty(property);

      var intentDataProperty = property.FindPropertyRelative(IntentDataFieldName);

      var intentDataField = new ObjectField {
        objectType = typeof(BaseIntentData),
        bindingPath = intentDataProperty.propertyPath
      };

      intentDataField.TrackPropertyValue(intentDataProperty, OnChangedIntentData);
      
      foldout.Add(intentDataField);

      return foldout;
    }

    private void OnChangedIntentData(SerializedProperty intentDataProperty) {
      var previousIntentValuesDataField = foldout.Q<VisualElement>(IntentValuesDataFieldName);
      if (previousIntentValuesDataField != null) {
        previousIntentValuesDataField.Unbind();
        foldout.Remove(previousIntentValuesDataField);
      }

      // Debug.Log($"OnChangedIntentData {intentDataProperty}");
      if (intentDataProperty != null) {
        var intentData = intentDataProperty.objectReferenceValue as BaseIntentData;
        if (intentData != null) {
          var copiedDefaultValues = intentData.BaseDefaultValues.Clone();
          // Debug.Log($"copiedDefaultValues: {copiedDefaultValues}");

          var intentValuesProperty = property.FindPropertyRelative(IntentValuesFieldName);
          intentValuesProperty.managedReferenceValue = copiedDefaultValues;
          intentValuesProperty.serializedObject.ApplyModifiedProperties();

          var intentValuesDataField = new PropertyField();
          intentValuesDataField.name = IntentValuesDataFieldName;
          intentValuesDataField.BindProperty(intentValuesProperty);
          foldout.Add(intentValuesDataField);
        }
      }
    }
  }

  [Serializable]
  [CustomPropertyDrawer(typeof(IntentSpawner))]
  public class IntentSpawnerPropertyDrawer : PropertyDrawer {
    [SerializeField] private VisualTreeAsset VisualTree;

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
      // property = _property;
      var root = new VisualElement();

      root.Add(new IntentDataAndValuesPropertyDrawer(property).CreatePropertyGUI());

      var spawnEffectButton = new Button {
        text = "Spawn Effect",
      };
      spawnEffectButton.clicked += () => {
        var intentSpawner = (property.boxedValue as IntentSpawner);
        Debug.Log($"Intent is [{intentSpawner.Spawn()}]");
      };
      root.Add(spawnEffectButton);
      //
      // VisualTree.CloneTree(root);
      //
      // foldout = root.Q<Foldout>("RootFoldout");
      // foldout.text = property.displayName;
      //
      // var intentDataProperty = property.FindPropertyRelative("IntentData");
      //
      // var intentDataField = root.Q<ObjectField>("IntentDataField");
      // intentDataField.objectType = typeof(BaseIntentData);
      // intentDataField.BindProperty(intentDataProperty);
      // intentDataField.RegisterValueChangedCallback(OnChangedIntentData);
      //
      // var intentSpawner = (property.boxedValue as IntentSpawner);
      // Debug.Log($"intentSpawner.IntentData: {intentSpawner.IntentData}");
      //
      // var intentData = intentSpawner.IntentData;
      //
      // if (intentData) {
      //   // var intentDataDefaultValuesType = intentData.GetType().GetField("DefaultValues", BindingFlags.Public | BindingFlags.Instance)!.FieldType;
      //   // Debug.Log($"intentDataDefaultValuesType: {intentDataDefaultValuesType}");
      //
      //   var copiedDefaultValues = intentSpawner.IntentData.BaseDefaultValues.Clone();
      //   Debug.Log($"copiedDefaultValues: {copiedDefaultValues}");
      //
      //
      //   var intentValuesProperty = property.FindPropertyRelative("ValuesReference");
      //   intentValuesProperty.managedReferenceValue = copiedDefaultValues;
      //   intentValuesProperty.serializedObject.ApplyModifiedProperties();
      //
      //   var intentValuesDataField = new PropertyField();
      //   intentValuesDataField.BindProperty(intentValuesProperty);
      //   foldout.Add(intentValuesDataField);
      // }
      //
      // var spawnEffectButton = new Button();
      // spawnEffectButton.text = "Spawn Effect";
      // root.Add(spawnEffectButton);


      // objectField.BindProperty(intentDataProperty);


      // // objectField.bindingPath = "IntentData";
      //
      // var intentSpawner = (property.boxedValue as IntentSpawner);
      // var intentDataProperty = property.FindPropertyRelative("IntentData");
      //
      // Debug.Log($"intentDataProperty: [{intentDataProperty}]:[{intentDataProperty != null}]");
      // if (intentDataProperty != null) {
      //   var intentData = intentDataProperty.objectReferenceValue as BaseIntentData;
      //   Debug.Log($"intentData: [{intentData}]");
      //   if (intentData != null) {
      //     var intentDataObject = new SerializedObject(intentData);
      //
      //     var defaultValuesProperty = intentDataObject.FindProperty("DefaultValues");
      //     var defaultValuesReferenceProperty = intentDataObject.FindProperty("DefaultValuesReference");
      //     var defaultValues = defaultValuesProperty.boxedValue;
      //     var defaultValuesReference = defaultValuesProperty.boxedValue;
      //     Debug.Log($"defaultValuesProperty: [{defaultValuesProperty}]");
      //     Debug.Log($"defaultValues: [{defaultValues}]");
      //     Debug.Log($"defaultValuesReferenceProperty: [{defaultValuesReferenceProperty}]");
      //     Debug.Log($"defaultValuesReference: [{defaultValuesReference}]");
      //
      //     var defaultValuesPropertyField = new PropertyField();
      //     defaultValuesPropertyField.BindProperty(defaultValuesProperty);
      //     foldout.Add(defaultValuesPropertyField);
      //
      //     var defaultValuesReferencePropertyField = new PropertyField();
      //     defaultValuesReferencePropertyField.BindProperty(defaultValuesReferenceProperty);
      //     foldout.Add(defaultValuesReferencePropertyField);
      //
      //     // intentSpawner.ValuesReference = new StatusIntentValues {Stacks = 123};
      //     // intentValuesProperty.serializedObject.Update();
      //     
      //     var intentValuesProperty = property.FindPropertyRelative("ValuesReference");
      //     intentValuesProperty.managedReferenceValue = new DamageIntentValues {Damage = 5};
      //     var intentValues = intentValuesProperty.managedReferenceValue;
      //     
      //     Debug.Log($"property.serializedObject.targetObject: [{property.serializedObject.targetObject}]");
      //     Debug.Log($"intentValuesProperty: [{intentValuesProperty}]");
      //     Debug.Log($"intentValues: [{intentValues}]");
      //     Debug.Log($"intentValuesProperty.managedReferenceValue: [{intentValuesProperty.managedReferenceValue}]");
      //     Debug.Log($"intentSpawner.ValuesReference: [{intentSpawner.ValuesReference}]");
      //
      //     // var concreteType = intentValuesProperty.managedReferenceValue.GetType();
      //     // Debug.Log($"concreteType: [{concreteType}]");
      //     // var concreteProperty = new SerializedObject(intentValuesProperty.serializedObject.targetObject)
      //     //   .FindProperty("Spawner.ValuesReference");
      //     // concreteProperty.managedReferenceValue = new DamageIntentValues() {Damage = 456};
      //
      //     Debug.Log($"intentValuesProperty.serializedObject.targetObject: [{intentValuesProperty.serializedObject.targetObject}]");
      //     Debug.Log($"intentValuesProperty.propertyPath: [{intentValuesProperty.propertyPath}]");
      //     // Debug.Log($"concreteProperty: [{concreteProperty}]");
      //     // Debug.Log($"concreteProperty.managedReferenceValue: [{concreteProperty.managedReferenceValue}]");
      //
      //     property.serializedObject.ApplyModifiedProperties();
      //
      //     var intentValuesPropertyField = new PropertyField(intentValuesProperty);
      //     intentValuesPropertyField.label = "intentValuesProperty";
      //     intentValuesPropertyField.BindProperty(intentValuesProperty);
      //     foldout.Add(intentValuesPropertyField);
      //
      //     // var concretePropertyPropertyField = new PropertyField(concreteProperty);
      //     // concretePropertyPropertyField.label = "concreteProperty";
      //     // concretePropertyPropertyField.BindProperty(concreteProperty);
      //     // foldout.Add(concretePropertyPropertyField);
      //
      //   }
      // }
      //
      // var objectField = root.Q<ObjectField>("IntentDataField");
      // objectField.RegisterValueChangedCallback(OnChangedIntentData);
      // objectField.objectType = typeof(IntentSpawner).GetField("IntentData", BindingFlags.Public | BindingFlags.Instance)!.FieldType;
      // objectField.BindProperty(intentDataProperty);

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
