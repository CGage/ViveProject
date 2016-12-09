// Script to control the behaviour of the Earth globe
// Brendan Clohesy

using UnityEngine;
using System.Collections;

public class RaycastAction : MonoBehaviour {

	public Transform originalPosition;
	public bool isLooked = false;
	public float timeNow;

	private Vector3 finalPos;
	private Vector3 startPos;
	private float globeSpeed = 2f;

	void Start () {
		// Initialise the positions
		finalPos = new Vector3 (1.1f, 1.5f, -0.8f);
		startPos = originalPosition.position;
	}

	public void RaycastOnEnter () {
		if (!isLooked) {
			StartCoroutine ("MoveGlobeTowards");
		}
	}

	public void RaycastOnExit () {
			if (isLooked) {
			StartCoroutine ("MoveGlobeAway");
		}
	}

	// Coroutine to move the glove towards the user
	IEnumerator MoveGlobeTowards () {
		while (transform.position != finalPos & !isLooked) {
			float step = globeSpeed * Time.deltaTime;
			transform.localScale += new Vector3 (1f, 1f, 1f);
			transform.position = Vector3.MoveTowards (transform.position, finalPos, step);
			yield return null;
		}
		isLooked = true;
	}
		
	// Coroutine to move the globe back onto the UI Canvas
	IEnumerator MoveGlobeAway () {
		while (transform.position != startPos && isLooked) {
			float step = globeSpeed * Time.deltaTime;
			transform.localScale += new Vector3 (-1f, -1f, -1f);
			transform.position = Vector3.MoveTowards (transform.position, startPos, step);
			yield return null;
		}
		isLooked = false;
	}

}
