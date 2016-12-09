using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class outputs text to the user to display the type
// of marker that is placed

public class DisplayPlaceMarker : MonoBehaviour {


	public Text text;
	public Camera camera;
	public string taggedName;
	public bool isFading = false;
	private Color c;

	float timer = 3f;
	float originalTimer = 3f;

	public void DisplayUI () {
		taggedName = camera.GetComponent <ReefTagger> ().pinName;
	//	text.text = taggedName;

		switch (taggedName) {
		case "Water":
			text.color = new Color (0.05f, 0.06f, 0.95f);
			text.text = taggedName;

			break;
		case "Algae":
			text.color = new Color (0.2f, 1f, 0.8f);
			text.text = taggedName;
			break;
		case "Sand":
			text.color = new Color (0.97f, 0.85f, 0.25f);
			text.text = taggedName;
			break;
		case "Coral":
			text.color = new Color (1f, 0.4f, 0.17f);
			text.text = taggedName;
			break;
		case "Rock":
			text.color = new Color (0.95f, 0.23f, 0.98f);
			text.text = taggedName;
			break;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		FadeOut ();

	}

	public void  FadeOut () {
		c = text.color;
		c.a -= Time.deltaTime / 2;
		if (c.a <= 0) {
			c.a = 0;
		}
		text.color = c;
	}
}
