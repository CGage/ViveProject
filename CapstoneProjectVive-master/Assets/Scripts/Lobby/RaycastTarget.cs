using UnityEngine;
using System.Collections;

// Draws a debug ray for testing

public class RaycastTarget : MonoBehaviour {

	public void Raycaster () {
		Vector3 forward = transform.TransformDirection (Vector3.forward) * 30;
		Debug.DrawRay (transform.position, forward, Color.red);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, forward, out hit)) {
			Debug.Log (hit.collider.gameObject.name);
		}
	}
}
