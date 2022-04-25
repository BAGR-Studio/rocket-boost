using UnityEngine;

public class Movement : MonoBehaviour {

	private readonly int increment = 100;
	private Rigidbody rigidBody;
	private AudioSource audioSource;

	[SerializeField]
	private readonly float upSpeed = 13;
	[SerializeField]
	private readonly float rotationSpeed = 1;

	void Start() {
		this.rigidBody = GetComponent<Rigidbody>();
		this.audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		performMovement();
		performRotation();
	}

	private void performMovement() {
		if (Input.GetKey(KeyCode.W)) {
			if (!this.audioSource.isPlaying) {
				this.audioSource.Play();
			}
			this.rigidBody.AddRelativeForce(getIndependentValue(Vector3.up, upSpeed));
		} else {
			this.audioSource.Pause();
		}
	}

	private void performRotation() {
		if (Input.GetKey(KeyCode.A)) {
			this.transform.Rotate(getIndependentValue(Vector3.forward, rotationSpeed));
			return;
		}

		if (Input.GetKey(KeyCode.D)) {
			this.transform.Rotate(getIndependentValue(Vector3.back, rotationSpeed));
			return;
		}
	}

	private Vector3 getIndependentValue(Vector3 value, float speed) {
		return value * Time.deltaTime * (speed * increment);
	}
}
