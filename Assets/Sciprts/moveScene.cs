﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScene : MonoBehaviour {
	void Update () {
		if (Input.anyKey)
			SceneManager.LoadScene ("play");
	}
}
