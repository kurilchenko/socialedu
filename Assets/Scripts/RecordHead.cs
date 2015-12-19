using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

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

    //[XmlElement]
    //public List<HeadPose> poses = new List<HeadPose>();    

    public void AddPose(Vector3 position, Quaternion rotation, float time)
    {
        //poses.Add(new HeadPose(position, rotation, time));
    }
}

public class RecordHead : MonoBehaviour
{
    public HeadData headData = new HeadData();
    public bool isRecording;
    float elapsedRecordTime;

    public void Record()
    {
        Debug.Log(gameObject.name + " starts recording.");

        isRecording = true;
        elapsedRecordTime = 0;
        //StartCoroutine(CoRecord());

        headData = new HeadData();
    }

    public void Stop()
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
        XmlSerializer serializer = new XmlSerializer(typeof(HeadData));

        Debug.Log(serializer.ToString());

        Debug.Log(headData.name + " is saving data to " + path);

        FileStream stream = new FileStream(path + "/Resources/Heads/" + headData.name + ".xml", FileMode.Create);
        serializer.Serialize(stream, headData);
        stream.Close();

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

        headData.AddPose(transform.position, transform.rotation, elapsedRecordTime);
        elapsedRecordTime += Time.fixedDeltaTime;
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.CapsLock) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isRecording)
            {
                Stop();
            }
            else
            {
                Record();
            }
        }
	}

}
