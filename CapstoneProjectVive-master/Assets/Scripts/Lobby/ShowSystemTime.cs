// A very basic script that will display the time on the UI
// Brendan Clohesy

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShowSystemTime : MonoBehaviour {

	public Text timeText;
	public string timeNow;

	private float timer = 1f;
	private float originalTimer;

	void Start () {
		originalTimer = timer;
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timeNow = DateTime.Now.ToString();
			timeText.text = timeNow;
			timer = originalTimer;
		}
	}
}
