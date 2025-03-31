using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace UnitSelection {
  public class SelectableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] private GameObject _hoverModel = null!;
    [SerializeField] private GameObject _selectedModel = null!;

    private void Awake() {
      Assert.IsNotNull(_hoverModel);
      Assert.IsNotNull(_selectedModel);
      _hoverModel.SetActive(false);
      _selectedModel.SetActive(false);
      SelectionManager.Instance.SelectionChanged.AddListener(OnSelectionChanged);
      
      EventManager.Instance.AbilityTargetingStart.AddListener(() => enabled = false);
      EventManager.Instance.AbilityTargetingStop.AddListener(() => enabled = true);
    }

    private void OnSelectionChanged(IEnumerable<SelectableComponent> selection) {
      setSelected(selection.Any(item => item == this));
    }

    private void setSelected(bool isSelected) {
      _selectedModel.SetActive(isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData) {
      _hoverModel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
      _hoverModel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
      SelectionManager.Instance.Select(this);
    }
  }
}
