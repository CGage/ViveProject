using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Random terrain generation

public class RandomTerrainGenerator : MonoBehaviour {
	public GameObject[] rock;
	public Rigidbody[] rb;

	public float hillsHeight;		
	public float hillsNum;


	public static Terrain terrain;

    void Start() {
        //The higher the numbers, the more hills/mountains there are
        float HM = Random.Range(15, 20);
		hillsHeight = HM;
        //The lower the numbers in the number range, the higher the hills/mountains will be...
        float divRange = Random.Range(20, 30);
		hillsNum = divRange;
        GenerateTerrain(GetComponent<Terrain>(), HM, divRange, HM);
		StartCoroutine ("rockOn");
    }

    //Our Generate Terrain function
    public void GenerateTerrain(Terrain t, float tileSize, float divRange, float HM)
    {
		terrain = t;
        //Heights For Our Hills/Mountains
        float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];
        for (int i = 0; i < t.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < t.terrainData.heightmapHeight; k++)
            {
                hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize) / divRange;
			

            }
        }

		// Creates a rock - Needs to be completed
		// Currently, the rock is not instantiating to the correct height
		// GameObject instance = Instantiate(Resources.Load ("Rock", typeof (GameObject))) as GameObject;
		// print(t.terrainData.alphamapWidth + "   " +  t.terrainData.alphamapHeight);


     //   Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
        t.terrainData.SetHeights(0, 0, hts);
    }

	// Creates rocks in the reef
	public void rockOn() {
		int numRocks = 1500;
		rock = new GameObject[numRocks];
		for (int i = 0; i < numRocks; i++) {

			// Create the rock
			rock[i] = Instantiate(Resources.Load ("Rock", typeof (GameObject))) as GameObject;
			rock[i].AddComponent<Rigidbody>().useGravity= false;
			rock[i].GetComponent<Rigidbody>().isKinematic = true;

            // Rotate each rock randomly
            float rotX = Random.Range(0.3f, 1f);
            float rotY = Random.Range(0.3f, 1f);
            float rotZ = Random.Range(0.3f, 1f);
            rock[i].transform.rotation = new Quaternion(rotX, rotY, rotZ, 1);

            // Scale each rock randomly
            float randSizeA = Random.Range (0.3f, 0.7f);
			float randSizeB = Random.Range (0.3f, 0.7f);
			float randSizeC = Random.Range (0.3f, 0.7f);
			Vector3 s = new Vector3 (randSizeA, randSizeB, randSizeC);
			rock[i].transform.localScale = s;

			// Randomize each rock location
			int x = Random.Range (-250, 250);
			int z = Random.Range (-250, 250);
            //float y = t.terrainData.GetHeight(x, z);
            Vector3 worldy = new Vector3(x, 0, z);
			Vector3 v = new Vector3((float)x, terrain.SampleHeight(worldy)-12, (float)z);
			rock[i].transform.position =  v;
		}
	}
}