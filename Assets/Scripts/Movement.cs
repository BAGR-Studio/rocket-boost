using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Vector3 startPossition = new Vector3(-30, 2.77f, 0);
	private Quaternion startRotation = new Quaternion(0, 0, 0, 0);
	private Rigidbody rigidBody;
	[SerializeField]
	private float upSpeed = 1;
	[SerializeField]
	private float moveSpeed = 1;

	void Start() {
		Debug.Log("Startig");
		this.rigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			this.rigidBody.AddRelativeForce(getIndependentValue(Vector3.up, upSpeed));
			Debug.Log("Performing jump");
		}

		if (Input.GetKey(KeyCode.A)) {
			this.transform.Rotate(getIndependentValue(Vector3.forward, moveSpeed));
			Debug.Log("Moving left");
			return;
		}

		if (Input.GetKey(KeyCode.D)) {
			this.transform.Rotate(getIndependentValue(Vector3.back, moveSpeed));
			Debug.Log("Moving right");
			return;
		}
	}

	private Vector3 getIndependentValue(Vector3 value, float speed) {
		return value * Time.deltaTime * speed;
	}
}
