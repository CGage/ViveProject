using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Allows persistence across scenes in order to load a mesh into the
// Reef scene

public class MeshSelection : MonoBehaviour {

	public static string[] names; 
	public static string meshToLoad;

	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		names = FileUpload.queryMeshes ();
	}

	public void returnName (int num) {
		meshToLoad = names [num];
		SceneManager.LoadScene(1);
	}
		
}
