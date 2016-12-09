using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class will probably not be used as the user
// doesn't need to know that a JSON object has been written

public class DisplayWriteJSON : MonoBehaviour {

	public Text text;
	private Color c;

	public void DisplayWrite() {
		string informWriteJSON = "Creating JSON object";
		text.text = informWriteJSON;
	}
		
	public void FadeOut () {
		c = text.color;
		c.a -= Time.deltaTime / 2;
		if (c.a <= 0) {
			c.a = 0;
		}
		text.color = c;
	}
}
