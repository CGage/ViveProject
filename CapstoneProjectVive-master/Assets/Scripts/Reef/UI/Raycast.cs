using UnityEngine;
using System.Collections;

public class Raycast: MonoBehaviour {

//	public GameObject TheCamera;

	public GameObject WaterText;
	public GameObject AlgaeText;
	public GameObject SandText;
	public GameObject RockText;
	public GameObject CoralText;

	// Store the original colour value
	private Color originalC;
	private Color blueButton;
	private Color blueText;
	private Color aquaButton;
	private Color aquaText;
	private Color yellowButton;
	private Color yellowText;
	private Color redButton;
	private Color redText;
	private Color pinkButton;
	private Color pinkText;


	void Start () {
		originalC = GetComponent<Renderer> ().material.color;
		blueButton = new Color (0.05f, 0.6f, 0.95f);
		blueText = new Color (0.2f, 0.7f, 0.95f);
		aquaButton = new Color (0.2f, 1f, 0.8f);
		aquaText = new Color (0.5f, 1f, 0.85f);
		yellowButton = new Color (0.97f, 0.85f, 0.25f);
		yellowText = new Color (0.99f, 0.92f, 0.42f);
		redButton = new Color (1f, 0.4f, 0.17f);
		redText = new Color (1f, 0.5f, 0.25f);
		pinkButton = new Color (0.95f, 0.23f, 0.98f);
		pinkText = new Color (0.97f, 0.44f, 0.99f);
	}

	void Update () {

	

	}

	public void RayCastEnter (int mode) {

		switch (mode) {

		case 1:
		//	Debug.Log ("WaterRayCastEnter");
			GetComponent<Renderer> ().material.color = blueButton;
			WaterText.GetComponent<Renderer> ().material.color = blueText;
		//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		
		case 2:
			GetComponent<Renderer> ().material.color = aquaButton;
			AlgaeText.GetComponent<Renderer> ().material.color = aquaText;
		//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
			break;

		case 3:
			GetComponent<Renderer> ().material.color = yellowButton;
			SandText.GetComponent<Renderer> ().material.color = yellowText;

		//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		
		case 4:
			GetComponent<Renderer> ().material.color = redButton;
			CoralText.GetComponent<Renderer> ().material.color = redText;

		//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		
		case 5:
			GetComponent<Renderer> ().material.color = pinkButton;
			RockText.GetComponent<Renderer> ().material.color = pinkText;
		//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		}
	}

	public void RayCastExit () {
		GetComponent<Renderer> ().material.color = originalC;
	//	transform.localScale += new Vector3 (0.0f, 0.0f, 0.0f);
		WaterText.GetComponent<Renderer> ().material.color = originalC;
		AlgaeText.GetComponent<Renderer> ().material.color = originalC;
		SandText.GetComponent<Renderer> ().material.color = originalC;
		CoralText.GetComponent<Renderer> ().material.color = originalC;
		RockText.GetComponent<Renderer> ().material.color = originalC;
	}

}

// Get Event Scripts
/**
 * start () 
 * 	var scripts = get the base event scripts on the object
 * 	var camera = get the camera in the scene
 * 
 * 	var pointScript = scripts[get the pointer click event]
 * 
 * 	pointScript[whatever the object location is] = camera
 * 	pointScript[whateverthe method location is] = method to run on event
 *  pointScript[whatever the value location is] = the value of the event.
 * 
 * 
 */