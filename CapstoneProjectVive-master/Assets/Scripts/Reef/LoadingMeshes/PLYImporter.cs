using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class PLYImporter : MonoBehaviour 
{
	private List<Vector3> points;
	private List<Color> pointColors;
	private List<int> faces;
	private List<Mesh> meshes;

	public string meshLocation;
	public int numfaces = 0;
	
	private static Vector3 centerPoint;
	public  Material lineMaterial;
	
	void Start () 
	{

	}
	
	static void CreateLineMaterial ()
	{
        //if (!lineMaterial) {
        //    // Unity has a built-in shader that is useful for drawing
        //    // simple colored things.
        //    var shader = Shader.Find("Assets/Materials/VertexColor");
        //    Material fred = (Material)Resources.Load("VertexColor", typeof(Material)) as Material;
        //    lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        //    // Turn on alpha blending
        //    lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //    lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //    // Turn backface culling off
        //    lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
        //    // Turn off depth writes
        //    //lineMaterial.SetInt ("_ZWrite", 0);
        //}
    }

	static void DebugIntList (List<int> list, string heading, int Count)
	{
		string faceOut = heading;
		for (int face = 0; face < Count; face++)
			faceOut += list[face] + "\t";
		Debug.Log(faceOut);		
	}
	
	static void DebugPointList (List<Vector3> list, string heading, int Count)
	{
		string faceOut = heading;
		for (int face = 0; face < Count; face++)
			faceOut += list[face].ToString () + "\t";
		Debug.Log(faceOut);		
	}

	static void DebugIntArray (int[] list, string heading, int Count)
	{
		string faceOut = heading;
		for (int face = 0; face < Count; face++)
			faceOut += list[face].ToString() + "\t";
		Debug.Log(faceOut);		
	}

    public void ReadBinaryFile(string file, float xPos, float yPos, float zPos) 
	{
		// Worker Variables
		int count = 0;
		points = new List<Vector3>();
		pointColors = new List<Color>();
		faces = new List<int>();
		int numvertices = 0;
		
		//---------------------------------------------------------------------
		// Read in the header information.
		//---------------------------------------------------------------------
		try {
			Stream s = File.Open (file, FileMode.Open);
			StreamReader ar = new StreamReader(s);
			string line = "";
			while (line != "end_header") {
				line = ar.ReadLine ();
				string[] parts = line.Split (' ');
				if (parts.Length == 3 && parts[1] == "vertex") {
					numvertices = int.Parse (parts[2]);
				} else if (parts.Length == 3 && parts[1] == "face") {
					numfaces = int.Parse (parts[2]);	
				}
			}
			s.Close ();
		} catch {
			return;	
		}

		CreateLineMaterial();
		
		//---------------------------------------------------------------------
		// Read past all of the ASCII header data.
		//---------------------------------------------------------------------
		Stream test = File.Open(file, FileMode.Open);
		BinaryReader tr = new BinaryReader(test);
		while (true) {
			char c = tr.ReadChar ();
			if (c == '\n') { 
				count++; 
				if (count == 14) break;	
			}
		}
		//---------------------------------------------------------------------
		// Read in the binary file for vertex and face data.
		//---------------------------------------------------------------------
		for (int i = 0; i < numvertices; i++) 
		{
				// First 3 bytes represent XYZ vertex position.
				float x = tr.ReadSingle ();
				float y = tr.ReadSingle ();
				float z = tr.ReadSingle ();
				//Debug.Log("XYZ: " + x + " " + y + " " + z);
				Vector3 point = new Vector3(x, y, z);
				centerPoint += point;
				points.Add (point);
	
				// Next 4 bytes represent RGBA vertex color.
				float r = tr.ReadByte () / 512.0f;
				float g = tr.ReadByte () / 512.0f;
				float b = tr.ReadByte () / 512.0f;
				float a = tr.ReadByte () / 512.0f;
				//Debug.Log("rgba: " + r + " " + g + " " + b + " " + a);
				pointColors.Add (new Color(r, g, b, a));
		}

		centerPoint /= numvertices;

		//transform.position = -centerPoint;
		//Camera.main.GetComponent<SmoothMouseOrbit>().UpdateDistance(Mathf.Abs(transform.position.z));

		// Read in each triplet of vertices.
		for (int i = 0; i < numfaces; i++) 
		{
			tr.ReadByte ();
			int face1 = tr.ReadInt32 ();
			faces.Add(face1);
			int face2 = tr.ReadInt32 ();
			faces.Add(face2);
			int face3 = tr.ReadInt32 ();
			faces.Add(face3);
		}
		tr.Close ();

		List<int> newFaces;
		newFaces = new List<int>();
		newFaces.Clear ();

		List<int> tempFaces;
		tempFaces = new List<int>();
		tempFaces.Clear ();

		List<Vector3> newPoints;
		newPoints = new List<Vector3>();
		newPoints.Clear ();

		List<Color> newColors;
		newColors = new List<Color>();
		newColors.Clear ();

		lineMaterial.SetPass (0);

		int[] mapping;
		mapping = new int[numvertices];
		int MeshSize = 64998;

		GameObject mainObject = new GameObject();
        mainObject.transform.position = new Vector3(xPos,yPos,zPos);
		mainObject.name = "New Mesh";

		for (int j = 0; j <= faces.Count / MeshSize; j++)
		{
			int range = faces.Count - j * MeshSize;
			tempFaces.Clear();

			if (range < MeshSize)
				tempFaces = faces.GetRange(j * MeshSize, range);
			else
				tempFaces = faces.GetRange(j * MeshSize, MeshSize);

			for (int a = 0; a < numvertices; a += 1) 
				mapping[a] = -1;

			for (int i = 0; i < tempFaces.Count; i += 3)
			{
				if (mapping[tempFaces[i]] == -1) 			// has not been added to the list and mapped.
				{
					newPoints.Add(points[tempFaces[i]]);  	// Add to the list.
					newColors.Add(pointColors[tempFaces[i]]);
					mapping[tempFaces[i]] = newPoints.Count - 1; 	// Update mapping.
	            }
				newFaces.Add(mapping[tempFaces[i]]);
					                     
				if (mapping[tempFaces[i + 1]] == -1) 		// has not been added to the list and mapped
				{
					newPoints.Add(points[tempFaces[i + 1]]); // Add to the list.
					newColors.Add(pointColors[tempFaces[i + 1]]);
					mapping[tempFaces[i + 1]] = newPoints.Count - 1;		// Update mapping.
				}
				newFaces.Add(mapping[tempFaces[i + 1]]);
		        
				if (mapping[tempFaces[i + 2]] == -1) // has not been added to the list and mapped
		        {
					newPoints.Add(points[tempFaces[i + 2]]);  // Add to the list.
					newColors.Add(pointColors[tempFaces[i + 2]]);
					mapping[tempFaces[i + 2]] = newPoints.Count - 1;  // Update mapping.
			    }
				newFaces.Add(mapping[tempFaces[i + 2]]);
				
//				DebugIntList(tempFaces, 	  "Temp Faces     :\t", 9);
//				DebugIntList(newFaces, 	  "New Faces :\t", newFaces.Count);
//				DebugPointList(points, 	  "Points    :\t", points.Count);
//				DebugPointList(newPoints, "New Points:\t", newPoints.Count);
//				DebugIntArray(mapping,    "Mapping   :\t", mapping.Length);
			}

			Mesh mesh = new Mesh();
			mesh.Clear ();
			GameObject newObject = new GameObject();
			newObject.name = "SubMesh" + j;
			newObject.transform.SetParent(mainObject.transform);
			mesh.vertices = newPoints.ToArray();
			mesh.colors = newColors.ToArray();
			mesh.triangles = newFaces.ToArray();
			newObject.AddComponent<MeshFilter>().mesh = mesh;
			newObject.AddComponent<MeshRenderer>().material = lineMaterial;
			newPoints.Clear ();
			newColors.Clear ();
			newFaces.Clear ();
			tempFaces.Clear();
		}
	}
}
