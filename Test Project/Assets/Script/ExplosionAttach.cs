using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttach : MonoBehaviour {

	private GameObject bag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("breakable")) {
			Destroy(other.gameObject);
		}
		Destroy(gameObject);
	}
}
