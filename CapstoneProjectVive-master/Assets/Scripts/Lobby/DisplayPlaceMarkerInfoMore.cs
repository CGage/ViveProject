using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Displays text to the user to inform them a marker has been placed

public class DisplayPlaceMarkerInfoMore : MonoBehaviour {

	public Text text;
	private Color d;

	void Update () {
		FadeOut ();
	}

	public void DisplayUI ()
	{	
		text.color = new Color (0.21f, 0.21f, 0.21f);
		text.text = "Marker Placed";
	}

	public void  FadeOut () {
		d = text.color;
		d.a -= Time.deltaTime / 2;
		if (d.a <= 0) {
			d.a = 0;
		}
		text.color = d;
	}
}
