using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading;


public class CollisionHandler : MonoBehaviour {

	[SerializeField] AudioClip levelFinishSound;
	[SerializeField] AudioClip gameFinishSound;
	[SerializeField] AudioClip crashSound;
	[SerializeField] AudioClip gameOverSound;

	public Text healthText;
	public Text fuelText;

	private ParticleSystem explosionParticle;

	private AudioSource audioSource;
	private int health;
	private int fuel;
	private float time;

	private bool isTransitioning;

	void Start() {
		this.audioSource = GetComponent<AudioSource>();
		this.explosionParticle = this.gameObject.transform.Find("Explosion").gameObject.GetComponent<ParticleSystem>();
		this.health = 100;
		this.fuel = 50;
		this.time = Time.time;
		this.isTransitioning = false;
	}

	void Update() {
		var currentTime = Time.time;

		if (currentTime - this.time >= 1) {
			this.time = currentTime;
			this.fuel -= 1;
		}

		if (this.health <= 0) {
			Debug.Log("All life lost");
			this.ReloadLevel();
		}

		if (this.fuel <= 0) {
			Debug.Log("Out of fuel");
			this.ReloadLevel();
		}

		if (Input.GetKeyDown(KeyCode.L)) {
			this.ReloadLevel();
		}

		this.UpdateScoreBar();
	}

	private void OnCollisionEnter(Collision collision) {
		if (isTransitioning) { return; }

		if (collision != null && (collision.gameObject.tag == Constants.OBSTACLE_NAME || collision.gameObject.tag == Constants.ENVIRONMET_NAME)) {
			this.explosionParticle.Play();
			this.PlaySound(this.crashSound, 0); //figure out why there is sound delay
			this.health--;
			return;
		}

		if (collision != null && collision.gameObject.tag == "Finish") {
			this.isTransitioning = true;
			var landingParticle = collision.gameObject.transform.Find("LandingParticle").gameObject.GetComponent<ParticleSystem>();
			this.FinishLevel(landingParticle);
			return;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Fuel") {
			Debug.Log("added fuel");
			this.fuel += 10;
			var particle = other.gameObject.transform.Find("Disappearance").gameObject.GetComponent<ParticleSystem>();
			particle.Play();
			StartCoroutine(this.RemoveFuelCan(other.gameObject, particle.duration));
			return;
		}
	}

	private void FinishLevel(ParticleSystem landingParticle) {
		Debug.Log("Level completed!");

		GetComponent<Movement>().enabled = false;

		landingParticle.Play();

		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (isNextScenePresent(sceneIndex)) {
			StartCoroutine(this.LoadNextLevel(sceneIndex, landingParticle.duration));
			return;
		}
		this.FinishGame();
	}

	IEnumerator LoadNextLevel(int currentSceneIndex, float particleDuration) {
		Debug.Log("Loading next level");
		this.PlaySound(this.levelFinishSound);
		yield return new WaitForSeconds(particleDuration + Constants.PARTICLE_DELAY);
		SceneManager.LoadScene(currentSceneIndex + 1);
	}

	IEnumerator RemoveFuelCan(GameObject fuelCan, float particleDuration) {
		yield return new WaitForSeconds(particleDuration);
		fuelCan.SetActive(false);
	}

	private void FinishGame() {
		Debug.Log("Game completed");
		this.PlaySound(this.gameFinishSound);
	}

	private void ReloadLevel() {
		Debug.Log("Reloading level");
		this.PlaySound(this.gameOverSound);
		Thread.Sleep(1000);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private bool isNextScenePresent(int currentSceneIndex) {
		return SceneManager.sceneCountInBuildSettings - 1 > currentSceneIndex ? true : false;
	}

	private void PlaySound(AudioClip audioClip) {
		this.PlaySound(audioClip, 10);
	}
	private void PlaySound(AudioClip audioClip, int volume) {
		this.audioSource.Stop();
		this.audioSource.PlayOneShot(audioClip, volume);
	}

	private void UpdateScoreBar() {
		this.healthText.text = string.Format("Health: {0}", this.health);
		this.fuelText.text = string.Format("Fuel: {0}", this.fuel);
	}
}
