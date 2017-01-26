using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class me : MonoBehaviour {

	Vector2 position; //二次元ベクトル型

	void Update () {
		position = transform.position;
		if (Input.GetKey (KeyCode.UpArrow))
			position.y += 2f * Time.deltaTime;
		if (Input.GetKey (KeyCode.DownArrow))
			position.y -= 2f * Time.deltaTime;
		if (Input.GetKey (KeyCode.RightArrow))
			position.x += 2f * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftArrow))
			position.x -= 2f * Time.deltaTime;
		transform.position = position;
	}

	void OnTriggerEnter2D (Collider2D c){
		if (c.gameObject.tag == "Enemy")
			print ("Hit!");
	}
}