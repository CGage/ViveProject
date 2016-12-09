// Basic script to scrape the given 'url' for the wind speed
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class WindSpeed : MonoBehaviour {

	public Text windSpeedText;

	WWW weatherWebsite;

	private float timer = 3f;
	private float originalTimer;

	public string urlSave;
	public string url = "http://www.seatemperature.org/australia-pacific/australia/cairns.htm";
	public string tempResult;

	public bool isFinished;

	void Start () {
		isFinished = true;
		originalTimer = timer;
		weatherWebsite = new WWW (url);
	}

	void Update () {
		// Start timer to scrape the data every x seconds
		timer -= Time.deltaTime;

		if (timer <= 0) {
			// Get the data, store it, perform a string match via regex & truncate the string 
			if (weatherWebsite.isDone) {
				urlSave = weatherWebsite.text;
				string pattern = "(\\d+)\\smph";
				// Get the result & store it for string processing later
				tempResult = Regex.Match (urlSave, pattern).Value;
				string s = tempResult;
				s = s.Remove (s.Length - 4);
				// this should show the wind speed in mph
				windSpeedText.text = s + "  " + "miles per hour";
			}
			timer = originalTimer;
		} 
	}
}
