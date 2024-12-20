using UnityEngine;
public class MoveAnimation : BaseAnimation {
	private float time = 0;
	private float duration = 0.3f;
	public Vector3 sourcePosition;
	public Vector3 targetPosition;


	public override bool animate(MoveComponent component) {
		time += Time.deltaTime;
		var percents = time / duration;
		component.gameObject.transform.position = Vector3.Lerp(sourcePosition, targetPosition, percents);


		return percents < 1;
	}
}