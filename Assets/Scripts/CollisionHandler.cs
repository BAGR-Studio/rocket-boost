using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

	[SerializeField] AudioClip levelFinishSound;
	[SerializeField] AudioClip gameFinishSound;
	[SerializeField] AudioClip crashSound;
	[SerializeField] AudioClip gameOverSound;

	private AudioSource audioSource;
	private int health;
	public int fuel;
	private float time;

	private bool isTransitioning;

	void Start() {
		this.audioSource = GetComponent<AudioSource>();
		this.health = 10000;
		this.fuel = 50;
		this.time = Time.time;
		this.isTransitioning = false;
	}

	void Update() {
		var currentTime = Time.time;

		if (currentTime - this.time >= 5) {
			this.time = currentTime;
			this.fuel -= 5;
		}
/*
		if (this.health <= 0) {
			Debug.Log("All life lost");
			this.ReloadLevel();
		}

		if (this.fuel <= 0) {
			Debug.Log("Out of fuel");
			this.ReloadLevel();
		}*/
	}

	private void OnCollisionEnter(Collision collision) {
		if (isTransitioning) { return; }

		if (collision != null && collision.gameObject.tag == "Obstacle") {
			this.PlaySound(this.crashSound, 1);
			this.health--;
			return;
		}

		if (collision != null && collision.gameObject.tag == "Fuel") {
			// think how to deal with fuel -> logic about consumption should be in movement script, but refilling should be here
			// think if refilling should be here
			this.fuel += 10;
			return;
		}

		if (collision != null && collision.gameObject.tag == "Finish") {
			this.isTransitioning = true;
			this.FinishLevel();
			return;
		}
	}

	private void FinishLevel() {
		Debug.Log("Level completed!");
	
		GetComponent<Movement>().enabled = false; //disable control from player

		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (isNextScenePresent(sceneIndex)) {
			this.LoadNextLevel(sceneIndex);
			return;
		}
		this.FinishGame();
	}

	private void LoadNextLevel(int currentSceneIndex) {
		Debug.Log("Loading next level");
		this.PlaySound(this.levelFinishSound);
		Thread.Sleep(1000);
		SceneManager.LoadScene(currentSceneIndex + 1);
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
		return SceneManager.sceneCount > currentSceneIndex ? true : false;
	}

	private void PlaySound(AudioClip audioClip) {
		this.PlaySound(audioClip, 10);
	}
	private void PlaySound(AudioClip audioClip, int volume) {
		this.audioSource.Stop();
		this.audioSource.PlayOneShot(audioClip, volume);
	}
}
