using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class me : MonoBehaviour {
	float posX, posY;
	void Update () {
		//自身の座標を取り出す
		posX = transform.position.x;
		posY = transform.position.y;
		//キー入力
		if (Input.GetKey (KeyCode.UpArrow))
			posY += 0.1f;
		if (Input.GetKey (KeyCode.DownArrow))
			posY -= 0.1f;
		if (Input.GetKey (KeyCode.RightArrow))
			posX += 0.1f;
		if (Input.GetKey (KeyCode.LeftArrow))
			posX -= 0.1f;
		transform.position = new Vector2 (posX, posY);
	}

	void OnTriggerEnter2D (Collider2D c){
		if (c.gameObject.tag == "Enemy") {
			Destroy (this.gameObject);
		}
	}
}