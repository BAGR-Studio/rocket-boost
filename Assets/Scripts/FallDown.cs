using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

	private float startTime;
	private int index;
	void Start() {
		this.gameObject.GetComponent<Rigidbody>().useGravity = false;
		this.startTime = Time.time;
		this.index = Random.Range(1, 20);
	}

	// Update is called once per frame
	void Update() {
		Debug.Log(this.index);
		var rigidBody = this.gameObject.GetComponent<Rigidbody>();
		if (isActionTime() && !rigidBody.useGravity) {
			rigidBody.useGravity = true;
		}

	}

	private void OnCollisionEnter(Collision collision) {
		if (collision != null && collision.gameObject.tag == Constants.ENVIRONMET_NAME) {
			this.gameObject.SetActive(false);
		}
	}
	private bool isActionTime() {
		if ((int)(Time.time - this.startTime) == this.index) {
			return true;
		}
		return false;
	}

}
