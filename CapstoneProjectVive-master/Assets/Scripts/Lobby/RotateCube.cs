using UnityEngine;
using System.Collections;

// Rotates a cube for testing.
// This can be deleted.. it tests whether the game is paused or not

public class RotateCube : MonoBehaviour {

	void Update () {
		transform.Rotate (new Vector3 (45, 30, 15) * Time.deltaTime);
	}
}
