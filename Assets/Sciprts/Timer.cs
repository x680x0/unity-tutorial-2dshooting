using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	float timer = 0;
	Text textComponent;

	void Start (){
		textComponent = this.GetComponent<Text> ();
	}

	void Update () {
		timer += 1f * Time.deltaTime;
		textComponent.text = timer.ToString("F0");
	}
}
