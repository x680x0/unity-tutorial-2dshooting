using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	GameObject bullet;
	GameObject target;
	float targetPosY;
	float time = 0;

	void Start () {
		//Prefabの読み込む
		bullet = Resources.Load <GameObject> ("bullet");
		//自機の取得
		target = GameObject.Find("me");
	}

	void Update () {
		time += 1f * Time.deltaTime;
		if (time >= 1f) {
			targetPosY = target.transform.position.y;
			GameObject clone = Instantiate (bullet);
			clone.transform.position = new Vector2 (4f, targetPosY);
			time = 0;
		}
	}
}