using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayMeshNameOne : MonoBehaviour {

	public Text text;
	public string elementzero;

	void Start () {
		elementzero = FileUpload.queryMeshes ()[0];
		DisplayMeshName ();
	}

	public void DisplayMeshName () {
		text.text = elementzero;
	}
}
