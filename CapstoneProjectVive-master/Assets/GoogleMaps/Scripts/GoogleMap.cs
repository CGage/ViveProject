using UnityEngine;
using System.Collections;

public class GoogleMap : MonoBehaviour
{
	public enum MapType
	{
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

    private int[] markerCount = { 0, 0, 0, 0, 0 };

    void Start() {
        
        initMarkers();
        //foreach () {
        //addMarker(); //add markers here somehow probably within another method
        //im not sure how, probably from the json or datalog file
        //}
        if (loadOnStart) Refresh();	
	}
	
	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}

    // Initialises the marker arrays in the class
    public void initMarkers() {
        int noMarkerTypes = 6, markersAllowed = 100;
        markers = new GoogleMapMarker[noMarkerTypes];
        for (int i = 0; i < noMarkerTypes; i++) {
            GoogleMapMarker marker = new GoogleMapMarker();
            marker.size = GoogleMapMarker.GoogleMapMarkerSize.Mid;
            GoogleMapLocation[] locs = new GoogleMapLocation[markersAllowed];
            for (int j = 0; j < markersAllowed; j++) {
                locs[j] = new GoogleMapLocation();
                locs[j].address = "";
            }
            marker.locations = locs;
            markers[i] = marker;
        }
    }

    // Adds a marker to the google map
    public void addMarker(float y, float x, int markerType) {
        switch (markerType) {
            case 0:
                markers[0].color = GoogleMapColor.red;
                markers[0].label = "Player";
                break;
            case 1:
                markers[1].color = GoogleMapColor.blue;
                markers[1].label = "Water";
                markerCount[1]++;
                break;
            case 2:
                markers[markerType].color = GoogleMapColor.green;
                markers[markerType].label = "Alge";
                markerCount[2]++;
                break;
            case 3:
                markers[markerType].color = GoogleMapColor.yellow;
                markers[markerType].label = "Sand";
                markerCount[3]++;
                break;
            case 4:
                markers[markerType].color = GoogleMapColor.orange;
                markers[markerType].label = "Coral";
                markerCount[4]++;
                break;
            case 5:
                markers[markerType].color = GoogleMapColor.brown;
                markers[markerType].label = "Rock";
                markerCount[5]++;
                break;
        }
        markers[markerType].locations[markerCount[markerType]].longitude = CoordConversion.calcLon(x);
        markers[markerType].locations[markerCount[markerType]].latitude = CoordConversion.calcLat(y);
        markers[markerType].size = GoogleMapMarker.GoogleMapMarkerSize.Mid;
    }

    IEnumerator _Refresh ()
	{
		var url = "http://maps.googleapis.com/maps/api/staticmap";
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
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

public enum GoogleMapColor
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

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
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

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}