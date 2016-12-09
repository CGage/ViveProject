using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	void Update () {
		transform.Rotate (new Vector3 (45, 30, 15) * Time.deltaTime);
	}
}
