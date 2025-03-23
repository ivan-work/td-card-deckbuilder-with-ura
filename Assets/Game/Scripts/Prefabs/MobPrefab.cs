using Abilities;
using Components;
using TMPro;
using UnityEngine;

namespace Prefabs {
  public class MobPrefab : MonoBehaviour, ITarget {
    [SerializeField] private GameObject _model = null!;
    private HealthComponent healthComponent = null!;
    private GridComponent gridComponent = null!;

    private Color OriginalColor { get; set; }
    private Material OriginalMaterial { get; set; }

    private void Awake() {
      healthComponent = this.GetAssertComponent<HealthComponent>();
      gridComponent = this.GetAssertComponent<GridComponent>();
      this.ThrowWhenNull(_model);
      OriginalMaterial = _model.GetComponent<MeshRenderer>().material;
      OriginalColor = OriginalMaterial.color;
    }

    private void Update() {
      if (healthComponent) {
        GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
      }
    }

    public void Highlight(bool isEnable, bool isValid) {
      // #TODO REMOVE ASAP
      if (isEnable) {
        OriginalMaterial.color = new Color(1, 1, 0);
      } else {
        OriginalMaterial.color = OriginalColor;
      }
    }

    public Vector2Int GetLoc() {
      return gridComponent.gridLoc;
    }

    public GameObject GetGameObject() {
      return gameObject;
    }

    public bool IsCell => false;
  }
}
