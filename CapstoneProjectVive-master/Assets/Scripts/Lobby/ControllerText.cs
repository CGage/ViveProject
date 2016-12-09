using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class displays the controller output text within the Lobby scene


public class ControllerText : MonoBehaviour {

	public GameObject Abutton;
	public GameObject Bbutton;
	public GameObject Xbutton;
	public GameObject Ybutton;
	public GameObject Lbumper;
	public GameObject Rbumper;
	public GameObject Startbutton;
	public GameObject Backbutton;
	public GameObject LeftStickClick;
	public GameObject RightStickClick;
	public GameObject LeftTrigger;
	public GameObject RightTrigger;
	public GameObject Horizontal;
	public GameObject Vertical;

	public Text InputText;
	public string outputString;
	public bool isPressed;

	public int mode;
	public float timer;
	public float originalTimer;

	void Start () {
		mode = 0;
		timer = 1f;
		originalTimer = timer;
		isPressed = false;

		// Set all of the buttons on the controller to text
		Abutton.SetActive (false);
		Bbutton.SetActive (false);
		Xbutton.SetActive (false);
		Ybutton.SetActive (false);
		Lbumper.SetActive (false);
		Rbumper.SetActive (false);
		Startbutton.SetActive (false);
		Backbutton.SetActive (false);
		LeftStickClick.SetActive (false);
		RightStickClick.SetActive (false);
		LeftTrigger.SetActive (false);
		RightTrigger.SetActive (false);
		Horizontal.SetActive (false);
		Vertical.SetActive (false);


	}
	

	void Update () {

		// Update every second instead of every frame
		timer -= Time.deltaTime;

		float lt = Input.GetAxis ("Trigger");
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");

	 
		// Reset the buttons to false
		if (timer <= 0) {
			Abutton.SetActive (false);
			Bbutton.SetActive (false);
			Xbutton.SetActive (false);
			Ybutton.SetActive (false);
			Lbumper.SetActive (false);
			Rbumper.SetActive (false);
			Startbutton.SetActive (false);
			Backbutton.SetActive (false);
			LeftStickClick.SetActive (false);
			RightStickClick.SetActive (false);
			LeftTrigger.SetActive (false);
			RightTrigger.SetActive (false);
			Horizontal.SetActive (false);
			Vertical.SetActive (false);

			isPressed = false;
			Color c = InputText.color;
			c.a = 0f;
			InputText.color = c;
			timer = originalTimer;
		}
			
		if (Input.GetButtonDown ("Fire1")) {
			mode = 1;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire2")) {
			mode = 2;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire3")) {
			mode = 3;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire4")) {
			mode = 4;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire5")) {
			mode = 5;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire6")) {
			mode = 6;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire7")) {
			mode = 7;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("Fire8")) {
			mode = 8;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("LStickClick")) {
			mode = 9;
			timer = originalTimer;
		}

		if (Input.GetButtonDown ("RStickClick")) {
			mode = 10;
			timer = originalTimer;
		}

		if (hor > 0) {
			mode = 13;
			timer = originalTimer;
		}

		if (hor < 0) {
			mode = 13;
			timer = originalTimer;
		}


		if (lt > 0) {
			mode = 11;
			timer = originalTimer;
		//	Debug.Log ("lt > 0");
		}

		if (lt < 0) {
			mode = 12;
			timer = originalTimer;
		//	Debug.Log ("lt < 0");
		}

		if (ver < 0 || ver > 0) {
			mode = 14;
			timer = originalTimer;
		}

	//	Debug.Log (lt);
			
		switch (mode) {
		case 1:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\tMake Selection / Activate";
				Abutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 2:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tPlace Marker / Menu";
				Bbutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 3:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tRemove Last Marker";
				Xbutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 4:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\tToggle Google MiniMap";
				Ybutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 5:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Lbumper.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 6:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Rbumper.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 7:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Backbutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 8:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Startbutton.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 9:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				LeftStickClick.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 10:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				RightStickClick.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 11:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				LeftTrigger.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 12:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				RightTrigger.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 13:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Horizontal.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;

		case 14:
			if (!isPressed) {
				Color c = InputText.color;
				c.a = 1f;
				InputText.color = c;
				outputString = "\n\t\t\t\t\t\t\t\t\tNull";
				Vertical.SetActive (true);
				isPressed = true;
				mode = 0;
			}
			break;
		}

		InputText.text = outputString;

	}


}
