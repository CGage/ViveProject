using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	public MovieTexture movTexture;

	void Start () {
		GetComponent<Renderer> ().material.mainTexture = movTexture;
		movTexture.loop = true;
		movTexture.Play ();
	}
}
