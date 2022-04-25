using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private int increment = 100;
	private Rigidbody rigidBody;
	private AudioSource audioSource;

	[SerializeField]
	private float upSpeed = 13;
	[SerializeField]
	private float rotationSpeed = 1;

	void Start() {
		Debug.Log("Startig");
		this.rigidBody = GetComponent<Rigidbody>();
		this.audioSource= GetComponent<AudioSource>();
	}

	void Update() {
		if (Input.GetKey(KeyCode.W)) {
			if (!this.audioSource.isPlaying) {
				this.audioSource.Play();
			}
			this.rigidBody.AddRelativeForce(getIndependentValue(Vector3.up, upSpeed));
			Debug.Log("Moving up");
		} else {
			this.audioSource.Pause();
		}

		if (Input.GetKey(KeyCode.A)) {
			this.transform.Rotate(getIndependentValue(Vector3.forward, rotationSpeed));
			Debug.Log("Moving left");
			return;
		}

		if (Input.GetKey(KeyCode.D)) {
			this.transform.Rotate(getIndependentValue(Vector3.back, rotationSpeed));
			Debug.Log("Moving right");
			return;
		}
	}

	private Vector3 getIndependentValue(Vector3 value, float speed) {
		return value * Time.deltaTime * (speed * increment);
	}
}
