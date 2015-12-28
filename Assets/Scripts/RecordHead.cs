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

public class RecordHead : Recordable
{
    public HeadData headData;
    public string recordName;

    float elapsedRecordTime;
    Transform cameraTrans;

    public override void Record()
    {
        base.Record();

        Debug.Log(gameObject.name + " starts recording head poses.");

        elapsedRecordTime = 0;
        //StartCoroutine(CoRecord());

        headData = new HeadData();
        headData.name = gameObject.name;
    }

    public override void StopRecording()
    {
        base.StopRecording();

        Debug.Log(gameObject.name + " stops recording head poses.");

        SaveData();
    }

	public void SaveData()
    {
        string path = Application.persistentDataPath;
        #if UNITY_EDITOR
        path = Application.dataPath;
        #endif

        XmlSerializer serializer = new XmlSerializer(typeof(HeadData));

        Debug.Log(headData.name + " is saving data to " + path);

        FileStream stream = new FileStream(path + "/Resources/Heads/Poses/" + headData.name + ".xml", FileMode.Create);
        serializer.Serialize(stream, headData);
        stream.Close();
    }

    protected override void Start()
    {
        base.Start();

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

        headData.AddPose(cameraTrans.position, cameraTrans.rotation, elapsedRecordTime);
        elapsedRecordTime += Time.fixedDeltaTime;
    }
	
	void Update ()
    {
        /*
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
        */

        /*
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
        */
	}

}
