using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerInput : MonoBehaviour {

	public static bool buttonPressed;
	public static bool triggerPressed;
	public static bool padPressed;
	public Camera camera;


	private bool isPickedUp;
	Vector2 touchpad;
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	private float sensitivityX = 1.5F;

	private float timer = 0.2f;
	private float timerReset = 0.2f;

	public Transform headPos;
	public Camera headCamera;
	public CharacterController directionFacing;
	public CharacterController player;

	public Vector3 lookDir;

	void Awake() {
		trackedObj = GetComponent <SteamVR_TrackedObject> ();
	}



	void Update() { // changed to update from fixedupdate
		device = SteamVR_Controller.Input((int)trackedObj.index);
		//headPos = GetComponent<CharacterController> ();

		// get the camera transform....
		headPos = headCamera.GetComponent<Transform>();

		directionFacing = player.GetComponent<CharacterController> ();

		timer -= Time.deltaTime;


		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			triggerPressed = true;
			device.TriggerHapticPulse(700);
		}

		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
			triggerPressed = false;
		}

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
			//Debug.Log("gripped");

			//	buttonPressed = true;
			//	camera.GetComponent<ReefTagger> ().isButtonPressed = true;
		}

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
			//	buttonPressed = false;
			//	camera.GetComponent<ReefTagger> ().isButtonPressed = false;
			//Debug.Log("not gripped");
		}



		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			padPressed = true;
			timer = timerReset;
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			padPressed = false;
			timer = timerReset;
		}


		//--------------------------//
//		HackyMovement.player.transform.rotation.y = directionFacing.transform.rotation.y;

	//	lookDir = new Vector3 (directionFacing.transform.rotation.x, directionFacing.transform.rotation.y, directionFacing.transform.rotation.z);
		// HackyMovement.player.transform.Rotate (0, headPos.transform.rotation.y * Time.deltaTime * 20, 0);
	//	HackyMovement.player.transform.rotation = lookDir;





		if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
			//Read the touchpad values
			touchpad = device.GetAxis (EVRButtonId.k_EButton_SteamVR_Touchpad);


			// Handle movement via touchpad
			if (touchpad.y > 0.2f || touchpad.y < -0.2f) {
				// Move Forward
				HackyMovement.player.transform.position += headPos.transform.forward * Time.deltaTime * (touchpad.y * 5f);



			
			}
		}
		
				///////



				// Application button now gets the menu....
				if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
					Debug.Log("application button pressed");
					buttonPressed = true;
					camera.GetComponent<ReefTagger>().isButtonPressed = true;
				}

				if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
					Debug.Log("application button pressed");
					buttonPressed = false;
					camera.GetComponent<ReefTagger>().isButtonPressed = false;
				}
			
}



	void OnTriggerStay (Collider other) {
		Debug.Log (other.name);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			isPickedUp = true;
			other.attachedRigidbody.isKinematic = true;
			other.gameObject.transform.SetParent (gameObject.transform);
			other.transform.rotation = gameObject.transform.rotation;
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			isPickedUp = false;
			other.gameObject.transform.SetParent (null);
			//other.attachedRigidbody.isKinematic = false;
		}
	}
}