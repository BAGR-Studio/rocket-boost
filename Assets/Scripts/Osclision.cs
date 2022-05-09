using UnityEngine;

public class Osclision : MonoBehaviour {

	[SerializeField] Vector3 movementVector;
	[SerializeField] float movementFactor;

	private Vector3 startPosition;

	void Start() {
		this.startPosition = this.transform.position;
	}

	void Update() {
		var offset = movementVector * movementFactor;
		this.transform.position = this.startPosition + offset;

	}
}