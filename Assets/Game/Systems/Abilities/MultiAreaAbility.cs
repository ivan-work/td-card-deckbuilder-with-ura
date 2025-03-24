using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/MultiAreaAbility")]
  public class MultiAreaAbility : Ability, IMultiClickTargetingContextConfig {
    [SerializeField] private int _maxClicks = 3;
    public int MaxClicks => _maxClicks;

    [SerializeField] public List<Vector2Int> TargetArea = new() {
      Vector2Int.zero,
      new Vector2Int(0, -1),
      new Vector2Int(+1, -1),
      new Vector2Int(+1, 0),
      new Vector2Int(0, +1),
      new Vector2Int(-1, +1),
      new Vector2Int(-1, 0),
    };

    [SerializeField] private GameObject? _indicator = null;
    public GameObject? Indicator => null; // TODO fix
    private LineRenderer lineRenderer;

    private void Awake() {
      // var gameObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Game/Prefabs/CircleIndicator.prefab");
      // _indicator = Instantiate(gameObj);
      // _indicator = new GameObject();
      // var spriteRenderer = _indicator.AddComponent<SpriteRenderer>();
      // spriteRenderer.sprite = Sprite.Create();
      // _indicator.transform.Rotate(90, 0, 0);
      // _indicator.transform.localScale.Set(2, 2, 2);
      // indicator = new GameObject();
      // lineRenderer = indicator.AddComponent<LineRenderer>();
      // lineRenderer.startWidth = .25f;
      // lineRenderer.endWidth = .25f;
      // lineRenderer.numCapVertices = 3;
      // lineRenderer.numCornerVertices = 3;
      // lineRenderer.loop = true;
    }

    public override ITargetingContext CreateTargetingContext() {
      return new MultiClickTargetingContext(this);
    }


    public List<Vector2Int> GetAffectedLocs(AbilityContext context, ITarget target, IEnumerable<IEnumerable<Vector2Int>> lockedAreas) {
      return TargetArea
        .Select(offset => GridSystem.AxialToOffset(GridSystem.OffsetToAxial(target.GetLoc()) + offset))
        .ToList();
    }
  }
}
