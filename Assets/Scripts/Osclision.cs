using UnityEngine;

public class Osclision : MonoBehaviour {

	[SerializeField] Vector3 movementVector;
	
	private int period = 2;

	private Vector3 startPosition;

	void Start() {
		this.startPosition = this.transform.position;
	}

	void Update() {
		var cycles = Time.time / period;

		const float tau = Mathf.PI * 2;
		var rawSinWave = Mathf.Sin(cycles * tau);
		var movementFactor = (rawSinWave + 1f) / 2f;

		var offset = movementVector * movementFactor;
		this.transform.position = this.startPosition + offset;
	}
}
