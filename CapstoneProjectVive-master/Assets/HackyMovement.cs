using UnityEngine;
using System.Collections;

public class HackyMovement : MonoBehaviour {
	public static CharacterController player;
	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController> ();
		if (SavePos.returnPos != null) {
			player.transform.position = SavePos.returnPos;
			player.transform.position = player.transform.position + new Vector3 (0, 30f, 0);
			player.transform.rotation = SavePos.returnRot;
		} else {

			player.transform.position = new Vector3 (0, 30f, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
