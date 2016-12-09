using UnityEngine;
using System.Collections;

public class globalFlock : MonoBehaviour {

	public GameObject[] fishPrefab = new GameObject[5];
	public static int tankSizeX = 100;
	public static int tankSizeY = 17;
	public static int tankSizeZ = 100;

	static int numFish = 100;

	public static GameObject[] allFish = new GameObject[numFish];
    public static int numFishTypes = 5;
	public static Vector3[] goalPos = new Vector3[numFishTypes];
    public static int[] fishies = new int[numFish];

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < numFish; i++)
		{
            fishies[i] = Random.Range(0, numFishTypes - 1);
			Vector3 pos = new Vector3(Random.Range(-tankSizeX,tankSizeX),
				                      Random.Range(15, 17),
									  Random.Range(-tankSizeZ,tankSizeZ));
            allFish[i] = (GameObject) Instantiate(fishPrefab[fishies[i]], pos, Quaternion.identity);
            int fishScale = Random.Range(1, 3);
            allFish[i].transform.localScale = new Vector3(fishScale, fishScale, fishScale);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        for (int i = 0; i < numFishTypes; i++) {
            if (Random.Range(0, 10000) < 50) {
                goalPos[i] = new Vector3(Random.Range(-tankSizeX, tankSizeX),
					Random.Range(15, 17),
                                      Random.Range(-tankSizeZ, tankSizeZ));
            }
        }
	}
}
