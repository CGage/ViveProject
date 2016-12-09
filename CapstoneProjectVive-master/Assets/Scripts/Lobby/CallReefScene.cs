// Script that loads the main Reef Scene
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CallReefScene : MonoBehaviour {

	// Call Reef Scene
	public void CallReef () {
		SceneManager.LoadScene (1);
	}
}
