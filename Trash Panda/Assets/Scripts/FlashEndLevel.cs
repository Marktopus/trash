using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEndLevel : MonoBehaviour {

	public Transform text1;
	public Transform text2;
	private float time = 0.0f;
	private bool startTicking = false;

	public void FadeIn() {
		var uitext = text1.GetComponent<Text>();
		var uitext2 = text2.GetComponent<Text>();

		uitext.enabled = true;
		uitext2.enabled = true;
		startTicking = true;
	}

	void Update() {
		if(startTicking) {
			time += Time.deltaTime;
		}

		if(time > 3) {
			NextLevel.LoadLevel();
		}
	}
}
