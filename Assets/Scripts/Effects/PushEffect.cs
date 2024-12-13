using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PushEffect : BaseEffect {
	private Vector2Int targetPos;
	private MoveComponent component;
	private Vector2Int sourcePos => component.gridComponent.gridPos;
	
	public PushEffect(MoveComponent component, Vector2Int direction, int force) {
		this.component = component;
		targetPos = sourcePos + direction*force;
	}
	public override void start() {
		var gridComponent = component.gridComponent;

		var hasPath = gridComponent.gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
		var hasMob = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

		if (hasPath && !hasMob) {



			gridComponent.moveTo(targetPos);
		}
	}

	protected override void animate() {
		//throw new System.NotImplementedException();
	}
	
	
}
