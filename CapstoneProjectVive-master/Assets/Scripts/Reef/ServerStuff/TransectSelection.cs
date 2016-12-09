using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransectSelection : MonoBehaviour {
    public static string sphereChosen;
    public static Dictionary<string, Texture2D> transectTextures;
	public static Vector3 returnPos;
	public static Quaternion returnRot;
    // Use this for initialization
    void Start () {
		sphereChosen = "";
        transectTextures = new Dictionary<string, Texture2D>();
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
