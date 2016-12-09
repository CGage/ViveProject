using UnityEngine;
using System.Collections;
using Valve.VR;

public class ButtonInputMenu : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	private float timer = 0.2f;
	private float originalTimer = 0.2f;

	public bool triggerPressed;

	void Awake() {
		trackedObj = GetComponent <SteamVR_TrackedObject> ();
	}

	void Update() { // changed to update from fixedupdate
		device = SteamVR_Controller.Input ((int)trackedObj.index);

		timer -= Time.deltaTime;

		Debug.Log ("Trigger Pressed: " + triggerPressed);


		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			triggerPressed = true;
			device.TriggerHapticPulse (700);
		}

		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			triggerPressed = false;
		}

	}
}
