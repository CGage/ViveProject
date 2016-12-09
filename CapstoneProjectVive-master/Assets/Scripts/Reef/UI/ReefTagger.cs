
// Brendan Clohesy

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ReefTagger : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	// Hold each Reef Tag pin into an array
	public List <GameObject> ReefTaggers;

	public GameObject canvas;
	public GameObject canvasJSONtext;
	public GameObject Xamp;

	public GameObject OriginalTagger;
	public GameObject TaggerClone;
	public GameObject CircleUI;
	public GameObject CircleUIclone;
	public GameObject Conversion;

	public Transform Camera;
	public Transform CameraPos;
	public Transform PlayerPos;


	// Store the number of tags deployed
	private int taggerCount;

	// Raycast 
	public Vector3 forward; 
	public Vector3 theCoord;
	public float distance = 100f;
	public string objectName;

	// Logic flags for the menu pausing
	private bool isPaused;
	public static bool hasChosen;
	public bool isTimeOut;
	public bool isPinDropped;

	private int listSize;
	public int mode;
	public string pinType;
	public string pinName;

	public float xValue;
	public float zValue;
	public float xValueConverted;
	public float zValueConverted;


	// Vive controller values
	public bool isButtonPressed = false;


	void Awake() {
		trackedObj = GetComponent <SteamVR_TrackedObject> ();
	}


	void Start () {
		isPaused = false;
		hasChosen = false;
		isTimeOut = false;
		List <GameObject> ReefTaggers = new List <GameObject> ();
	}



	void Update () {
		
		//device = SteamVR_Controller.Input ((int)trackedObj.index);
		DrawRay ();
		// If Xbox 360 controller button 'B' or right mouse button...
		// Place the marker and present the UI
		//Debug.Log(isButtonPressed);

		if (isButtonPressed) {			
			isPaused = !isPaused;
			if (isPaused) {
				// Pause and Drop PIn
				DropPin ();
				PauseMenu ();
				Time.timeScale = 0;
			} else if (!isPaused) {
				Time.timeScale = 1;
				DestroyMenu ();
				Destroy (TaggerClone.gameObject);
			}
			isButtonPressed = !isButtonPressed;
		}

		// So the problem is that the HMD raycast or reticle is not
		// responding to raycast collisions etc...
		if (hasChosen) {
			Time.timeScale = 1;
			hasChosen = !hasChosen;
			isPaused = !isPaused;
		}

		if (ControllerInput.padPressed) {
			Destroy (TaggerClone.gameObject);
			ReefTaggers.RemoveAt (taggerCount);
		}
	}

	// Draws a ray for debugging
	public void DrawRay() {
		forward = transform.TransformDirection (Vector3.forward * distance);
		Debug.DrawRay (transform.position, forward, Color.green);
	}

	// Instantiates a Drop Pin or Tag on the Reef to mark selected area.
	public void DropPin () {
		RaycastHit hit;
		// Drop a pin wherever the raycast intersects with the collider,
		// and rename them so they are unique
		if (Physics.Raycast (transform.position, forward, out hit)) {
			theCoord = hit.point;
			xValue = theCoord.x;
			zValue = theCoord.z;
			objectName = hit.collider.gameObject.name;
			TaggerClone = Instantiate (OriginalTagger, hit.point, Quaternion.identity) as GameObject;
			TaggerClone.name = "ReefTag_" + taggerCount.ToString ();
			ReefTaggers.Add (TaggerClone);
			taggerCount++;
		}
    }

	// Instantiates the UI menu. Attached to the Camera, local positions are reset so that
	// the camera will always face the UI. The UI is then detached from the camera, and 
	// attached to the player so that choice can be made with the HUD. 
	public void PauseMenu() {
		if (CircleUIclone == null) {
			CircleUIclone = GameObject.Instantiate (CircleUI, Camera.transform.position, Quaternion.identity) as GameObject;
			CircleUIclone.transform.parent = CameraPos;
			CircleUIclone.transform.localPosition = new Vector3 (0, 0, 0);
			CircleUIclone.transform.localPosition += new Vector3 (-0.047f, 0.137f, 3.285f);
			CircleUIclone.transform.localRotation = Quaternion.Euler (270f, -0.04f, 0);
			CircleUIclone.transform.parent = PlayerPos;	
		}
	}


	// Function is called from the Event trigger attached to
	// The UI section on the donut. It will pass an int representing
	// the type of material, and will return it for use elsewhere.
	public void MaterialType (int type) {
		//Debug.Log ("Material Function Called");
		mode = type;

		switch (mode) {
		case 1:
			Color bluePin = new Color (0.05f, 0.06f, 0.95f);
			TaggerClone.GetComponent <Renderer> ().material.color = bluePin;
			pinName = "Water";
			break;
		
		case 2:
			// Algae
			Color aquaPin = new Color (0.2f, 1f, 0.8f);
			TaggerClone.GetComponent <Renderer> ().material.color = aquaPin;
			pinName = "Algae";
			break;

		case 3:
			// Sand
			Color yellowPin = new Color (0.97f, 0.85f, 0.25f);
			TaggerClone.GetComponent <Renderer> ().material.color = yellowPin;
			pinName = "Sand";
			break;
		
		case 4:
			// Coral
			Color redPin = new Color (1f, 0.4f, 0.17f);
			TaggerClone.GetComponent <Renderer> ().material.color = redPin;
			pinName = "Coral";
			break;
		
		case 5:
			// Rock
			Color pinkPin = new Color (0.95f, 0.23f, 0.98f);
			TaggerClone.GetComponent <Renderer> ().material.color = pinkPin;
			pinName = "Rock";
			break;
		}
		pinType = mode.ToString ();
	//	InformUserJSON();		// Display Message to User 
		ConvertX (xValue);
		ConvertZ (zValue);
		Invoke ("WriteJSON", 0.2f);			// Write the JSON to file
		SendUI ();                          // Show the message to user
        ChrisIsAwesome(ConvertX(xValue), ConvertZ(zValue), type);// CALL CHRIS FUNCTION HERE - //SQL and other stuff
		hasChosen = true;
    }

    // Adds marker to google minimap and inserts new row into sql
    public void ChrisIsAwesome(float lat, float lon, int type) {
        StartCoroutine(FileUpload.insertRow(lat, lon, GameObject.Find("GetPlayerPos").transform.position.x, type));
        GameObject.Find("BentMiniMap").GetComponent<MiniMap>().addMarker(lon, lat, type);
    }
	// Destroys the menu
	public void DestroyMenu () {
		Destroy (CircleUIclone);
	}

	public void SendUI () {
		canvas.GetComponent <DisplayPlaceMarker> ().DisplayUI ();
		canvas.GetComponent <DisplayPlaceMarkerInfoMore> ().DisplayUI ();
	}



	public void ReadLogFile () {
		Debug.Log ("Reading File");
		// Hold the last 5 results
		string[] lastFive = new string[5];
		int counter = 0;
		string JSONPath = @"c:\ReefData\JSON\JSONdata.json";
		string filePath = @"c:\ReefData\TXT\datalog.txt";
		string readText = File.ReadAllText (filePath);


		// Store the contents of each line in the file into an array
		string[] contents = File.ReadAllLines (filePath);
		// Get the total length
		int lengthContents = contents.Length;
	//	Debug.Log ("length of Contents array: " + lengthContents);
		// Get the last 5 elements
		for (int i = 0; i < 5; i++) {
			lastFive[counter] = contents[lengthContents - 5 + i];
			counter++;
		}
		// Check the contents
		for (int i = 0; i < lastFive.Length; i++) {
			Debug.Log (lastFive [i]);
		}
	}
		

	// Writes a JSON file of the data associated with the tagged objects
	// Also creates a txt log file for easier reading
	public JsonData WriteJSON () {
		string pathFile = @"c:\ReefData\TXT\datalog.txt";
		string JSONPath = @"c:\ReefData\JSON\JSONdata.json";
		string pathDir = @"c:\ReefData";
		string JSONDir = @"c:\ReefData\JSON";
		string DataDir = @"c:\ReefData\TXT";

		// Create JSON data object
		JsonData theData = new JsonData (xValueConverted, zValueConverted, pinName);

			if (!Directory.Exists (pathDir)) {
				DirectoryInfo di = Directory.CreateDirectory (pathDir);
			}

		if (!Directory.Exists (JSONPath)) {
			DirectoryInfo di = Directory.CreateDirectory (JSONDir);
		}

		if (!Directory.Exists (DataDir)) {
			DirectoryInfo di = Directory.CreateDirectory (DataDir);
		}

		// Check to see if the path exists, and write/append to the file
		if (!File.Exists (pathFile)) {
		//	Debug.Log ("Checking PathFile");
			string logfile = Environment.NewLine + pinName + "  " + DateTime.Now + Environment.NewLine +
				 "Long: " + ConvertX (xValue) + "   " + "Lat: " + ConvertZ (zValue);
			File.WriteAllText (pathFile, logfile);
			string jsonObj = JsonUtility.ToJson (theData);
			File.WriteAllText (JSONPath, jsonObj);
		} else {
			string appendText = Environment.NewLine + pinName + "  " + DateTime.Now + Environment.NewLine +
			     "Long: " + ConvertX (xValue) + "   " + "Lat: " + ConvertZ (zValue);// theCoord.ToString ();
			File.AppendAllText (pathFile, appendText);
			string jsonObj = Environment.NewLine + JsonUtility.ToJson (theData);
			File.AppendAllText (JSONPath, jsonObj);
		}

		// Xamp.GetComponent<XAMP> ().postJson ();
	//	GetComponent<XAMP>().postJson();
		// Return it for use elsewhere
		return theData;
	}

	public float ConvertX (float x) {
		float xvalue = x;
		xValueConverted = CoordConversion.calcLon (x);
		return xValueConverted;
	}

	public float ConvertZ (float z) {
		float zvalue = z;
		zValueConverted = CoordConversion.calcLat (z);
		return zValueConverted;
	}
		
	public void InformUserJSON () {
		// message to User - Writing to file
		canvasJSONtext.GetComponent <DisplayWriteJSON> ().DisplayWrite();
	}
}
	
// JSON class that extends MonoBehaviour to allow objects
// to be created from MonoBehaviour datatypes
[Serializable]
public class JsonData {

	public float LonX;
	public float LatZ;
	public string taggedName;
	public string dateTime;

	public JsonData (float LonX, float LatZ, string taggedName) {
		this.LonX = LonX;
		this.LatZ = LatZ;
		this.taggedName = taggedName;
		this.dateTime = "" + DateTime.Now;
	}
}