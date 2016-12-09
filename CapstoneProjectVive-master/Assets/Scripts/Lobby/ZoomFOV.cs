// Simple script to enable a camera zoom
// Be careful as it is VR and can make people sick
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using System;

public class ZoomFOV : MonoBehaviour {

	private float initialFOV;
	private float zoomFOV;
	private float zoomLevel;
	private float zoomInSpeed;
	private float zoomOutSpeed;

	void Start () {
		// Get the initial camera FOV
		// Make sure it goes back correctly, technicolour yawn....
		initialFOV = Camera.main.fieldOfView;
		zoomLevel = 1.5f;
		zoomInSpeed = 100f;
		zoomOutSpeed = 100f;
	}

	void Update () {
		float triggerAxis = Input.GetAxis ("Trigger");
		// If the left Trigger or Right Mouse button....
		if ((triggerAxis != 0 || Input.GetMouseButtonDown (1))) {
			ZoomIn ();
		} else {
			ZoomOut ();
		}
	}	

	void ZoomIn() {
		if (Math.Abs (Camera.main.fieldOfView - (initialFOV / zoomLevel)) < 0.5f) {
			Camera.main.fieldOfView = initialFOV / zoomLevel;
		} else if (Camera.main.fieldOfView - (Time.deltaTime * zoomInSpeed) >= (initialFOV / zoomLevel)) {
			Camera.main.fieldOfView -= (Time.deltaTime * zoomInSpeed);
		}
	}

	void ZoomOut() {
		if (Math.Abs (Camera.main.fieldOfView - initialFOV) < 0.5f) {
			Camera.main.fieldOfView = initialFOV;
		} else if (Camera.main.fieldOfView + (Time.deltaTime * zoomOutSpeed) <= initialFOV) {
			Camera.main.fieldOfView += (Time.deltaTime * zoomOutSpeed);
		}
	}
}
