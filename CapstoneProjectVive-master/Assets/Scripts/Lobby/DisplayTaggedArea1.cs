using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// This class does some string processing to show the user
// the latitude and longitude within the lobby

public class DisplayTaggedArea1 : MonoBehaviour {

	public Text text;

	// public members to send over to the google map display
	public float longitudeCast;
	public float latitudeCast;

	public void ReadLogFile () {
	//	Debug.Log ("Reading File");
		// Hold the last 5 results
		string[] lastTen= new string[10];
		int counter = 0;
		string JSONPath = @"c:\ReefData\JSON\JSONdata.json";
		string filePath = @"c:\ReefData\TXT\datalog.txt";
		string readText = File.ReadAllText (filePath);

		if (!File.Exists (filePath)) {
			return;
		} else {
			// Store the contents of each line in the file into an array
			string[] contents = File.ReadAllLines (filePath);
			// Get the total length
			int lengthContents = contents.Length;

			// Get the last 5 elements
			for (int i = 0; i < 10; i++) {
				lastTen[counter] = contents [lengthContents - 10 + i];
				counter++;
			}

			// Store the two pieces of info
			string tempString1 = lastTen [8];
			string tempString2 = lastTen [9];

			// Extract just the numbers 
			string pattern = "([0-9]+.)";
			string regexResult = Regex.Matches (tempString2, pattern)[0].Value;
			string regexResult1 = Regex.Matches (tempString2, pattern) [1].Value;
			string tempAddLong = regexResult + regexResult1;
			float longCasted;

			// The float we want for Longitude
			longCasted = float.Parse (tempAddLong);
			longitudeCast = longCasted;

			string regexResult2 = Regex.Matches (tempString2, pattern) [2].Value;
			string regexResult3 = Regex.Matches (tempString2, pattern) [3].Value;
			string tempAddLat = regexResult2 + regexResult3;
			float latCasted;
			latCasted = float.Parse (tempAddLat);
			latitudeCast = latCasted;
			text.text = tempString1 + "\n" + tempString2;
		}
	}
		
	// Use this for initialization
	void Start () {
		ReadLogFile ();
	}
}
