using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadExit : MonoBehaviour {

	public void LoadMain() {
		SceneManager.LoadScene (1);
	}

	public void ExitProgram() {
		Application.Quit ();
	}
		
}
