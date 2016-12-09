using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

// Scrapes the below site to obtain the current humidity

public class Humidity : MonoBehaviour {

	public Text humidityText;

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
				string pattern = "(\\d+)%";
				// Get the 2nd element in the array, this is the current temp
				tempResult = Regex.Matches (urlSave, pattern)[1].Value;
		//		Debug.Log (tempResult);
				string s = tempResult;
			//	s = s.Remove (s.Length - 6);
				// this should show the temperature
				humidityText.text = s;
			}
			timer = originalTimer;
		} 
	}
}

