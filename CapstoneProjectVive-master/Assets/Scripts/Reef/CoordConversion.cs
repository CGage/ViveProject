using UnityEngine;
using System.Collections;

// Calculates real world coordinatates from the point of origin

public class CoordConversion : MonoBehaviour {
	public static float oLat = 0;
	public static float oLon = 0;
	public static float kmPerDegLat = (float)((2 * Mathf.PI * 6373.6) / 360);
	public static float kmPerDegLon = (float)(kmPerDegLat* Mathf.Cos(Mathf.Deg2Rad* oLat));

	// Resets the origin of the world in relation to a set of real world coordinates
	public static void resetOrigin(float newLon, float newLat) {
		oLat = newLat;
		oLon = newLon;
		kmPerDegLon = (float)(kmPerDegLat*Mathf.Cos(oLat));
	}

	// Calculate the x position of the longitude in relation to the origin (converts to Metres)
	public static float calcX(float lon) {
		return (lon - oLon) * kmPerDegLon * 1000;
	}

	// Calculates the z position of the latitude in relation to the origin (converts to metres)
	public static float calcZ(float lat){
		return (lat - oLat) * kmPerDegLat * 1000;
	}

	// Calculates the real world longitude based off the displacement of xpos from the origin (in metres!)
	public static float calcLon(float x) {
		return (x / kmPerDegLon) / 1000 + oLon;
	}

	// Calculates the real world latitude based of the displacement of zpos from the origin (in metres!)
	public static float calcLat(float z) {
		return (z / kmPerDegLat)/1000 + oLat;
	}
}
