using UnityEngine;

public class Stats : MonoBehaviour {
	private int health;

	void Start() {
		this.health = 10;
	}

	void Update() {
		if (this.health <= 0) {
			Debug.Log("All life lost");
			// add logic for game over screen
		}

	}

	private void OnCollisionEnter(Collision collision) {
		if (collision != null && collision.gameObject.tag == "Obstacle") {
			this.health--;
		}
		// add logic for hit animation
	}
}
