using Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abilities {
  public class TargetableUnitComponent : MonoBehaviour, ITarget, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] private GameObject _model = null!;
    private GridComponent gridComponent = null!;

    private Color? originalColor;
    private Material originalMaterial = null!;

    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
      originalMaterial = _model.GetComponent<MeshRenderer>().material;
      this.ThrowWhenNull(_model);
      this.ThrowWhenNull(originalMaterial);
    }


    public void Highlight(bool isEnable, bool isValid) {
      // #TODO REMOVE ASAP
      if (isEnable) {
        originalColor = originalMaterial.color;
        originalMaterial.color = new Color(1, 1, 0);
      } else if (originalColor.HasValue) {
        originalMaterial.color = originalColor.Value;
      }
    }

    public Vector2Int GetLoc() {
      return gridComponent.gridLoc;
    }

    public GameObject GetGameObject() {
      return gameObject;
    }

    public void OnPointerClick(PointerEventData eventData) {
      AbilityManager.Instance.ConfirmTarget(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
      AbilityManager.Instance.OnHoverStart(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
      AbilityManager.Instance.OnHoverStop(this);
    }

    public virtual bool IsCell => false;
  }
}
