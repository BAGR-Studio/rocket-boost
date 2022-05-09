using UnityEngine;

public class Movement : MonoBehaviour {

	[SerializeField] AudioClip movementSound;
	[SerializeField] private readonly float upSpeed = 13;
	[SerializeField] private readonly float rotationSpeed = 2f;


	private ParticleSystem upParticle;
	private readonly int increment = 100;
	private Rigidbody rigidBody;
	private AudioSource audioSource;

	void Start() {
		this.rigidBody = GetComponent<Rigidbody>();
		this.audioSource = GetComponent<AudioSource>();
		this.upParticle = this.gameObject.transform.Find("RocketJet").gameObject.GetComponent<ParticleSystem>();
	}

	void Update() {
		performMovement();
		performRotation();
	}

	private void performMovement() {
		if (Input.GetKey(KeyCode.W)) {
			if (!this.audioSource.isPlaying) {
				this.audioSource.PlayOneShot(this.movementSound);
				this.upParticle.Play();
			}
			this.rigidBody.AddRelativeForce(getIndependentValue(Vector3.up, upSpeed));
		} else {
			this.audioSource.Pause();
			this.upParticle.Stop();
		}
	}

	private void performRotation() {
		if (Input.GetKey(KeyCode.A)) {
			//this.rigidBody.AddTorque(getIndependentValue(Vector3.forward, rotationSpeed));
			this.transform.Rotate(getIndependentValue(Vector3.forward, rotationSpeed));
		} else if (Input.GetKey(KeyCode.D)) {
			//this.rigidBody.AddTorque(getIndependentValue(Vector3.back, rotationSpeed));
			this.transform.Rotate(getIndependentValue(Vector3.back, rotationSpeed));
		}
	}

	private Vector3 getIndependentValue(Vector3 value, float speed) {
		return value * Time.deltaTime * (speed * increment);
	}
}
