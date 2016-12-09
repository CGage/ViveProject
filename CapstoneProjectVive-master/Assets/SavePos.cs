using UnityEngine;
using System.Collections;

public class SavePos : MonoBehaviour {
	public static Vector3 returnPos;
	public static Quaternion returnRot;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		returnPos = TransectSelection.returnPos;
		returnRot = TransectSelection.returnRot;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
