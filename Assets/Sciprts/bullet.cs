using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	Vector2 position;
	// Use this for initialization
	void Start () {
		//ターゲット(自機)の位置を取得
		GameObject target;
		Vector2 targetPosition;
		target = GameObject.Find ("me");
		targetPosition = target.transform.position;
		//角度を求める
		Vector2 distance; //自分とターゲットの距離
		distance.x = targetPosition.x - transform.position.x;
		distance.y = targetPosition.y - transform.position.y;
		float degree = Mathf.Atan2 (distance.y, distance.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3 (0, 0, -180 + degree);
	}
	
	// Update is called once per frame
	void Update () {
		//Transformの赤軸方向にすすめる
		position = transform.position;
		position.x += -5 * transform.right.x * Time.deltaTime;
		position.y += -5 * transform.right.y * Time.deltaTime;
		transform.position = position;
	}
}
