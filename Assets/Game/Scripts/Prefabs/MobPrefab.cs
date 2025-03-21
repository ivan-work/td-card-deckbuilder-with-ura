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

    public void Highlight(bool enable) {
      // #TODO REMOVE ASAP
      if (enable) {
        OriginalMaterial.color = new Color(1, 1, 0);
      } else {
        OriginalMaterial.color = OriginalColor;
      }
    }

    public Vector2Int GetPos() {
      return gridComponent.gridPos;
    }

    public GameObject GetGameObject() {
      return gameObject;
    }
  }
}
