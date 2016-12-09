using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayMeshNameTwo : MonoBehaviour {

	public Text text;
	public string elementzero;
	public int num;

	void Start () {

		elementzero = FileUpload.queryMeshes ()[num];
		DisplayMeshName ();
	}

	public void DisplayMeshName () {
		text.text = elementzero;
	}
}
