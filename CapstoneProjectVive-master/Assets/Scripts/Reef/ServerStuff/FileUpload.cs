// C# file names: "FileUpload.cs"
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


public class FileUpload : MonoBehaviour {
    private string m_LocalFileName = "C:/ReefData/JSON/JSONData.json";
    private string m_URL = @"http://localhost:80/upload.php";
    private static string message = "";
    public static DirectoryInfo info;
    private string[] transectFiles;
    private string[] meshFiles;
    GameObject myObject;
  
    

    void Start() {
        transectFiles = new string[5];
        meshFiles = new string[1];
        StartCoroutine(queryTransectsDB( (i) => {transectFiles = i;
        
        for (int j = 0; j < 4; j++) {
            StartCoroutine(loadTransect(transectFiles[j]));       
        }
        }));


        if (MeshSelection.meshToLoad != null) {
            try {
                loadMesh(MeshSelection.meshToLoad.Substring(0, MeshSelection.meshToLoad.Length - 4));
            } catch (ArgumentException e) {
                print(e + "The mesh loading did not work");
            } 
        }

    }
    // Returns a string array of file names within the ReefMeshes folder on localhost
    public static string[] queryMeshes() {
        string reefURL = @"c:\xampp\htdocs\ReefMeshes\";
		string[] files = new string[100];
        try {
            info = new DirectoryInfo(reefURL);
            FileInfo[] fileInfo = info.GetFiles();
			int i = 0;
            foreach (FileInfo file in fileInfo) {
                if (file.ToString().Contains(".ply") == true) {      
					files[i] = file.ToString().Substring(reefURL.Length);
                    i++;
					//print(file.ToString().Substring(reefURL.Length)); // prints each file in the folder
                }
            }
        } catch(ArgumentException e) {
            print(e + "| The server folder does not exist!");
        }
        return files;
    }

    public IEnumerator queryTransectsDB(System.Action<string[]> callback) {
        WWWForm form = new WWWForm();       
        form.AddField("randomthing", "");
        WWW w = new WWW("http://localhost:80/ReefTransects/getTransect.php", form);
        yield return w;
        if (w.error == null) {
            string[] fileNames = w.text.Remove(w.text.Length - 1).Split(' ');
            callback(fileNames);    
        }
        else {
            message += "ERROR: " + w.error + "\n";
            print(w.error);
        }
    }


    public IEnumerator querymeshesDB(System.Action<string[]> callback) {
        WWWForm form = new WWWForm();

        form.AddField("randomthing", "");
        WWW w = new WWW("http://localhost:80/ReefMeshes/getMesh.php", form);
        yield return w;
        if (w.error == null) {
            string[] fileNames = w.text.Remove(w.text.Length - 1).Split(' ');
            callback(fileNames);
        } else {
            message += "ERROR: " + w.error + "\n";
            print(w.error);
        }
    }

    // Loads the mesh "reefSection".ply at the coordinate position desiganted in the txt file "reefSection".txt
    // which are saved on the localhost at "http://localhost/ReefMeshes/"
    public IEnumerator loadMesh(string reefSection) {
        string url = "http://localhost/ReefMeshes/" + reefSection;
        string path = @"C:\ReefData\" + reefSection;
        WWW wwwScript = new WWW(url + ".ply");
        WWW wwwMeta = new WWW(url + ".txt"); // real server stuff
        yield return wwwScript;
        yield return wwwMeta; 


        File.WriteAllBytes(path + ".ply", wwwScript.bytes);
        
        string[] coords = wwwMeta.text.ToString().Split(',');

        float xPos = CoordConversion.calcX(float.Parse(coords[1]));
        float zPos = CoordConversion.calcZ(float.Parse(coords[0]));
		CoordConversion.resetOrigin (xPos, zPos); // resets origin and puts mesh in origin position- easily changed 
        GetComponent<PLYImporter>().ReadBinaryFile(path + ".ply", 0, 0f, 0);
        
    }

	public void loadDummyMesh(string reefSection) {
		string path = @"C:\dummyserver\" + reefSection;
		string[] coords = System.IO.File.ReadAllText (path + ".txt").Split(',');
		print(coords[0] + "   " + coords[1]);
		float xPos = CoordConversion.calcX(float.Parse(coords[1]));
		float zPos = CoordConversion.calcZ(float.Parse(coords[0]));
		GetComponent<PLYImporter>().ReadBinaryFile(path + ".ply", xPos, 0f, zPos);
	
	}

    // Returns a string array of file names within the ReefTransects folder on localhost
    public static string[] queryTransects() {
        string reefURL = @"c:\xampp\htdocs\ReefTransects\";
        
        try {
            info = new DirectoryInfo(reefURL);
            FileInfo[] fileInfo = info.GetFiles();
            string[] files = new string[fileInfo.Length/2];
            int i = 0;
            foreach (FileInfo file in fileInfo) {
                if (file.ToString().Contains(".jpg") == true) {
                    files[i] = file.ToString().Substring(reefURL.Length);
                    i++;
                }
            }
            return files;
        }
        catch (ArgumentException e) {
            print(e + "| The server folder does not exist!");
        }
        return null;
    }
    public IEnumerator loadTransect(string transect) {
        string url = "http://localhost/ReefTransects/" +transect;
        WWW wwwImage = new WWW(url + ".jpg");
        WWW wwwCoords = new WWW(url + ".txt"); 
        yield return wwwImage;
        yield return wwwCoords;

        Texture2D texture = new Texture2D(10, 10);

        wwwImage.LoadImageIntoTexture(texture);
        TransectSelection.transectTextures.Add(transect, texture);
        string[] coords = wwwCoords.text.ToString().Split(',');
        float xPos = CoordConversion.calcX(float.Parse(coords[1]));
        float zPos = CoordConversion.calcZ(float.Parse(coords[0]));

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = transect;
        Vector3 worldPos = new Vector3(xPos, 0, zPos);
        sphere.transform.position = new Vector3(xPos, RandomTerrainGenerator.terrain.SampleHeight(worldPos) - 6f, zPos);
        sphere.GetComponent<Renderer>().material.mainTexture = texture;
        sphere.AddComponent<Rigidbody>().isKinematic = true;
        sphere.GetComponent<Rigidbody>().useGravity = false;
        sphere.tag = "transect";
    }

    // Uploads a json file with the reefdata to the localhost 
    public static IEnumerator uploadFileCo(string localFileName, string uploadURL) {
        WWW localFile = new WWW("file:///" + localFileName);
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        postForm.AddBinaryData("theFile", localFile.bytes, localFileName, "text/plain");
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }

    // Attempts to upload a file to the localhost with error handling
    static void uploadFile(string localFileName, string uploadURL) {
        try { uploadFileCo(localFileName, uploadURL); } catch (ArgumentException e) { print(e + "The file did not upload");}
    }

    // Inserts a new row into the database on localhost
    public static IEnumerator insertRow(float lat, float lon, float alt, int cat) {
        WWWForm form = new WWWForm();
        form.AddField("latitude", lat.ToString());
        form.AddField("longitude", lon.ToString());
        form.AddField("altitude", alt.ToString());
        form.AddField("category", cat);
        
        WWW w = new WWW("http://localhost:80/ReefDatabase/reefData.php", form);
        yield return w;
        if (w.error == null) {

            print(w.text);
        }
        else {
            message += "ERROR: " + w.error + "\n";
            print(w.error);
        }
    }
}