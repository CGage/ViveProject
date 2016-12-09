// Basic script to scrape the given 'url' for Australian Eastern Standarad Time
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Aest : MonoBehaviour {

	public Text timeText;

	WWW timeWebsite;

	private float timer = 10f;
	private float originalTimer;

	public string urlSave;
	public string url = "http://www.timeanddate.com/worldclock/australia/brisbane";
	public string tempResult;

	public bool isFinished;



	void Start () {
		isFinished = true;
		originalTimer = timer;
		timeWebsite = new WWW (url);
	}


	void Update () {
		// Start timer to scrape the data every x seconds
		timer -= Time.deltaTime;

		if (timer <= 0) {
			// Get the data, store it, perform a string match via regex & truncate the string 
			if (timeWebsite.isDone) {
				urlSave = timeWebsite.text;
			//	Debug.Log (urlSave);
				string pattern = "(\\d\\d:\\d\\d:\\d\\d\\s\\w.)";
				// Get the result & store it for string processing later
				tempResult = Regex.Match (urlSave, pattern).Value;
				timeText.text = tempResult;
			}
			timer = originalTimer;
		} 
	}
}
