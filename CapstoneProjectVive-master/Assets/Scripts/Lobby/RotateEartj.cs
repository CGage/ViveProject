using UnityEngine;
using System.Collections;

// Script to rotate the Earth globe in lobby. 
// Currently, direction needs to be changed as it
// is rotating the wrong way

public class RotateEartj : MonoBehaviour {

	void Update () {
		transform.Rotate (Vector3.down * Time.deltaTime * 20);
	}
}
