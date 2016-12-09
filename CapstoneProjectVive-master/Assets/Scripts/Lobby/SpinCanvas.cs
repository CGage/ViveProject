// Script to rotate the Canvases 180 degrees
// so that the user doesn't have to look behind
// all the time to view content.
// Brendan Clohesy

using UnityEngine;
using System.Collections;

public class SpinCanvas : MonoBehaviour {

	public void Spin () {
		StartCoroutine ("Rotate");
	}

	IEnumerator Rotate () {
		float rotateTime = 2.57f;
		while (rotateTime > 0) {
			rotateTime -= Time.deltaTime;
			transform.Rotate (Vector3.up,  (Time.deltaTime * 70));
			yield return null;
		}
	}
}
