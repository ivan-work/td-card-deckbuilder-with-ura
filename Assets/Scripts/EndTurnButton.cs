using UnityEngine;
using UnityEngine.UIElements;

public class EndTurnButton : MonoBehaviour {
  [SerializeField] private UIDocument? _uiDocument;
  [SerializeField] private string _endTurnButtonId = "EndTurnButton";
  private Button? button;

  private void Awake() {
    if (_uiDocument != null) {
      button = _uiDocument.rootVisualElement.Q<Button>(_endTurnButtonId);
    } else {
      Debug.LogWarning("UIDocument is null");
    }
  }

  private void OnEnable() {
    if (button != null) button.clicked += OnPointerClick;
  }

  private void OnDisable() {
    if (button != null) button.clicked -= OnPointerClick;
  }

  private void OnPointerClick() {
    EventManager.PhasePlayerIntent.Invoke();
  }
}
