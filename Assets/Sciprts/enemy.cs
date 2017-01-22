using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	float time = 0, span = 0.5f;
	GameObject bullet;
	// Use this for initialization
	void Start () {
		bullet = Resources.Load <GameObject> ("bullet");
	}
	
	// Update is called once per frame
	void Update () {
		time += 1f * Time.deltaTime;
		if (time >= span) {
			Instantiate (bullet);
			time = 0;
		}
	}
}
