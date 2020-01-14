using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float height=125;

	public float distance=-125;

	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate() {
		transform.position = player.transform.position + new Vector3(0.0f, height, distance);
	}
}
