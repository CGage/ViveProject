using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class TransectControls : MonoBehaviour {

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

	void Awake() {
		trackedObj = GetComponent <SteamVR_TrackedObject> ();
	}



	void Update() { // changed to update from fixedupdate
		device = SteamVR_Controller.Input((int)trackedObj.index);

		timer -= Time.deltaTime;

	

		// Application button now gets the menu....
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
			SceneManager.LoadScene(1);


		}
	}
}