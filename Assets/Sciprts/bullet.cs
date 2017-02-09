using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	float posX, posY;


	void Update () {
		posX = transform.position.x;
		posY = transform.position.y;
		posX -= 0.1f;
		transform.position = new Vector2 (posX, posY);
	}
}