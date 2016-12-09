using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("transect")) {
			TransectSelection.returnPos = HackyMovement.player.transform.position + new Vector3 (1f, 10f, 1f);
			TransectSelection.returnRot = HackyMovement.player.transform.rotation;
            TransectSelection.sphereChosen = other.name;
			SceneManager.LoadScene(2);
			print("Go To Next Scene");
		}
	}
}
