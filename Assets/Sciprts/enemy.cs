using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	float time = 0, span = 0.5f;
	GameObject bullet, bulletParent;
	void Start () {
		//Prefabの読み込む
		bullet = Resources.Load <GameObject> ("bullet");
		//親
		bulletParent = GameObject.Find("bullets");
	}

	void Update () {
		time += 1f * Time.deltaTime;
		if (time >= span) {
			//Instantiate (bullet, transform.position, Quaternion.identity, bulletParent.transform); //生成
			GameObject obj = Instantiate(bullet);
			obj.transform.position = transform.position;
			obj.transform.SetParent (bulletParent.transform);
			time = 0;
		}
	}
}