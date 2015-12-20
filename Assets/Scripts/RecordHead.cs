using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[System.Serializable]
public struct HeadPose
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;

    public HeadPose(Vector3 position, Quaternion rotation, float time)
    {
        this.position = position;
        this.rotation = rotation;
        this.time = time;
    }
}

[System.Serializable]
[XmlRoot]
public class HeadData
{
    [XmlAttribute("name")]
    public string name;

    [XmlElement("poses")]
    public List<HeadPose> poses = new List<HeadPose>();    

    public void AddPose(Vector3 position, Quaternion rotation, float time)
    {
        poses.Add(new HeadPose(position, rotation, time));
    }
}

public class RecordHead : MonoBehaviour
{
    public HeadData headData;
    public string recordName;

    bool isRecording;
    float elapsedRecordTime;
    Transform cameraTrans;

    public void Record()
    {
        Debug.Log(gameObject.name + " starts recording.");

        isRecording = true;
        elapsedRecordTime = 0;
        //StartCoroutine(CoRecord());

        headData = new HeadData();
        headData.name = gameObject.name;
    }

    public void StopRecording()
    {
        Debug.Log(gameObject.name + " stops recording.");

        SaveData();

        isRecording = false;
    }

	public void SaveData()
    {
        string path = Application.persistentDataPath;
        #if UNITY_EDITOR
        path = Application.dataPath;
        #endif
        
        /*
        var fullPath = path + "/Resources/Heads/" + headData.name + ".txt";

        using (StreamWriter writer = new StreamWriter(fullPath))
        {
            writer.Write("Hello!");
        }
        */

        //FileStream stream = new FileStream(fullPath, FileMode.Create);

        XmlSerializer serializer = new XmlSerializer(typeof(HeadData));

        Debug.Log(serializer.ToString());

        Debug.Log(headData.name + " is saving data to " + path);

        FileStream stream = new FileStream(path + "/Resources/Heads/" + headData.name + ".txt", FileMode.Create);
        serializer.Serialize(stream, headData);

        //Debug.Log((serializer.Deserialize(stream) as HeadData).name);
        stream.Close();

    }

    public void LoadData()
    {
#if UNITY_EDITOR
        // Refresh recently changed assets in order to load the last version of the level.
        UnityEditor.AssetDatabase.Refresh();
#endif

        string path = "Heads/" + recordName;

        //Debug.Log(Resources.Load("Heads/Visitor"));

        TextAsset textAsset = Resources.Load(path) as TextAsset;

        if (textAsset == null)
        {
            Debug.LogWarning("File not found in a following path: " + path);

            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(HeadData));
        HeadData data = null;

        using (TextReader reader = new StringReader(textAsset.text))
        {
            data = serializer.Deserialize(reader) as HeadData;
        }

        if (data == null)
        {
            Debug.LogWarning("Not able to serialize a file in a following path: " + path);
        }

        this.headData = data;
    }

    public void Play()
    {
        LoadData();

        StartCoroutine(CoPlayHeadData());
    }

    IEnumerator CoPlayHeadData()
    {
        for (var i = 0; i < headData.poses.Count; i++)
        {
            cameraTrans.position = headData.poses[i].position;

            if (i +1 > headData.poses.Count - 1)
            {
                var deltaTime = headData.poses[i + 1].time - headData.poses[i].time;

                yield return new WaitForSeconds(deltaTime);
            }
            
        }

        yield return null;
    }

    void Start()
    {
        cameraTrans = GetComponent<Visitor>().cameraRig.GetComponentInChildren<Camera>().transform;
    }

    IEnumerator CoRecord()
    {
        var lastRecordTime = Time.time;

        while (isRecording)
        {
            var currentDelta = Time.time - lastRecordTime;

            Debug.Log(currentDelta);

            lastRecordTime = Time.time;

            yield return new WaitForSeconds(1f / 60f);
        }
    }

    void FixedUpdate()
    {
        if (!isRecording)
            return;

        headData.AddPose(transform.position, cameraTrans.rotation, elapsedRecordTime);
        elapsedRecordTime += Time.fixedDeltaTime;
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.CapsLock) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isRecording)
            {
                StopRecording();
            }
            else
            {
                Record();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach (var r in FindObjectsOfType<PosePlayer>())
            {
                if (r.recordName != string.Empty)
                {
                    r.Play();
                }
            }

        }
	}

}
