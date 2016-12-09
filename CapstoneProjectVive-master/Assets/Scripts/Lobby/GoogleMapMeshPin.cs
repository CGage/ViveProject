using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoogleMapMeshPin : MonoBehaviour {

	public Text textGoogleScript;

	public enum MapType {
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;

	public float longitudePin;
	public float latitudePin;
	
	void Start() {
		if(loadOnStart) Refresh();	
		// get the data from DisplayTaggedArea1 class
	}
	
	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}
	
	IEnumerator _Refresh ()
	{
		longitudePin = textGoogleScript.GetComponent <DisplayTaggedArea1> ().longitudeCast;
		latitudePin = textGoogleScript.GetComponent <DisplayTaggedArea1> ().latitudeCast;
		var url = "http://maps.googleapis.com/maps/api/staticmap";
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
				// insert our real co-ords.
				centerLocation.latitude = latitudePin;
				centerLocation.longitude = longitudePin;
				Debug.Log (latitudePin);

				qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", centerLocation.latitude, centerLocation.longitude));
			}
			qs += "&zoom=" + zoom.ToString ();
		}
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
		qs += "&scale=" + (doubleResolution ? "2" : "1");
		qs += "&maptype=" + mapType.ToString ().ToLower ();
		var usingSensor = false;
#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif
		qs += "&sensor=" + (usingSensor ? "true" : "false");
		
		foreach (var i in markers) {
			qs += "&markers=" + string.Format ("size:{0}|color:{1}|label:{2}", i.size.ToString ().ToLower (), i.color, i.label);
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}
		
		foreach (var i in paths) {
			qs += "&path=" + string.Format ("weight:{0}|color:{1}", i.weight, i.color);
			if(i.fill) qs += "|fillcolor:" + i.fillColor;
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}
		
		var req = new WWW (url + "?" + qs);
		yield return req;
		GetComponent<Renderer> ().material.mainTexture = req.texture;
	}
	
	
}
// changed the following clases with lobby at the end as it was conflicting with the other map in
// the lobby
public enum GoogleMapColorLobby
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

// changed the following clases with lobby at the end as it was conflicting with the other map in
// the lobby
[System.Serializable]
public class GoogleMapLocationLobby
{
	public string address;
	public float latitude;
	public float longitude;
}

// changed the following clases with lobby at the end as it was conflicting with the other map in
// the lobby
[System.Serializable]
public class GoogleMapMarkerLobby
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;
	
}

// changed the following clases with lobby at the end as it was conflicting with the other map in
// the lobby
[System.Serializable]
public class GoogleMapPathLobby
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}