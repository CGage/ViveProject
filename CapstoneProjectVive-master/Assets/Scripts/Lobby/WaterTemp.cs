// This script will scrape the given 'url' for the water temperature
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class WaterTemp : MonoBehaviour {

	public Text waterTempText;

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
				string pattern = "(\\d+)&deg;C";
				// Get the 2nd element in the array, this is the current temp
				tempResult = Regex.Matches (urlSave, pattern) [1].Value;
				string s = tempResult;
				s = s.Remove (s.Length - 6);
				// this should show the temperature
				waterTempText.text = s + " " + "°" + "C";
			}
			timer = originalTimer;
		} 
	}
}
