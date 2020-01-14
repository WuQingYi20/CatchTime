using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public Button startButton;
	// Use this for initialization
	void Start () {
		Button btn = startButton.GetComponent<Button>();
		btn.onClick.AddListener(delegate { OnStartGame("Game"); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartGame(String scene) {
		SceneManager.LoadScene(scene);
	}
}
