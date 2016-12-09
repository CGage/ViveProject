using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddImageToSphere : MonoBehaviour {
	public Texture2D transect;
	// Use this for initialization
	void Start () {

		if(TransectSelection.transectTextures.ContainsKey(TransectSelection.sphereChosen)) {


			transect = TransectSelection.transectTextures[TransectSelection.sphereChosen];
			gameObject.GetComponent<MeshRenderer> ().material.mainTexture = transect;

		}
	

	}

	// Update is called once per frame
	void Update () {
	
	}
}
