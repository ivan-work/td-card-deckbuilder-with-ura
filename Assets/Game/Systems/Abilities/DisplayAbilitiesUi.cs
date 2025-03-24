using System.Collections.Generic;
using System.Linq;
using Components;
using UnitSelection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Abilities {
  public class DisplayAbilitiesUi : MonoBehaviour {
    [SerializeField] private VisualTreeAsset _abilityButtonTemplate = null!;
    private ScrollView abilitiesContainer = null!;

    private void Awake() {
      SelectionManager.Instance.SelectionChanged.AddListener(OnSelectionChanged);
      var uiDocument = this.GetAssertComponent<UIDocument>();
      abilitiesContainer = uiDocument.rootVisualElement.Q<ScrollView>("AbilitiesContainer");

      this.ThrowWhenNull(_abilityButtonTemplate);
      this.ThrowWhenNull(abilitiesContainer);

      abilitiesContainer.Clear();
    }

    private VisualElement createAbilityButton(GameObject source, Ability ability) {
      var abilityButton = _abilityButtonTemplate.CloneTree().Q<Button>("Root");
      abilityButton.text = ability.Name;
      abilityButton.iconImage = ability.Icon;
      abilityButton.clicked += () => { EventManager.Instance.StartAbility.Invoke(source, ability); };
      return abilityButton;
    }

    private void OnSelectionChanged(IEnumerable<SelectableComponent> selection) {
      abilitiesContainer.Clear();
      
      var abilitiesComponent = selection
        .Select(component => component.GetComponent<AbilitiesComponent>())
        .FirstOrDefault(component => component is not null);
      
      if (abilitiesComponent is not null) {
        foreach (var ability in abilitiesComponent.Abilities) {
          abilitiesContainer.Add(createAbilityButton(abilitiesComponent.gameObject, ability));
        }
      }
    }
  }
}
