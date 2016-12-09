using System.IO;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class DisplayTaggedArea4 : MonoBehaviour {

	public Text text;

	public void ReadLogFile () {
//		Debug.Log ("Reading File");
		// Hold the last 5 results
		string[] lastTen = new string[10];
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
//			Debug.Log ("length of Contents array: " + lengthContents);
			// Get the last 5 elements
			for (int i = 0; i < 10; i++) {
	//			Debug.Log (lastTen [i]);
				lastTen[counter] = contents [lengthContents - 10 + i];
//				Debug.Log (counter);
				counter++;
			}

			string tempString1 = lastTen [2];
			string tempString2 = lastTen [3];
			text.text = tempString1 + "\n" + tempString2;
			// Check the contents
			//		for (int i = 0; i < lastFive.Length; i++) {
			//		Debug.Log (lastFive [i]);
			//	}
		}



	}


	// Use this for initialization
	void Start () {
		ReadLogFile ();
	}

	// Update is called once per frame
	void Update () {

	}
}
