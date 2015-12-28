using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[ExecuteInEditMode]
public class PosePlayer : MonoBehaviour
{

    public string recordName;
    public Transform cameraTrans;
    public HeadData headData;
    public float time;

    protected bool isPlaying;
    protected int index;

    public void LoadData()
    {
        #if UNITY_EDITOR
        // Refresh recently changed assets in order to load the last version of the level.
        UnityEditor.AssetDatabase.Refresh();
        #endif

        string path = "Heads/Poses/" + recordName;

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
        //LoadData();

        index = headData.poses.FindLastIndex(p => p.time <= time);

        isPlaying = true;

        /*
        if (!isPlaying)
        {
            StartCoroutine(CoPlayHeadData());
        }
        */
        
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public float Duration
    {
        get
        {
            if (headData == null || headData.poses.Count == 0)
                return 0f;

            return headData.poses[headData.poses.Count - 1].time;
        }
    }

    public bool isRecorded;

    void Start()
    {
        /*
        if (isRecorded && recordName != string.Empty)
        {
            //Play();
            if (Application.isPlaying)
            {
                Play();
            }
        }
        */
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            if (recordName != string.Empty && (headData == null || headData.poses.Count == 0))
            {
                LoadData();
            }
            
        }

        UpdatePose();
    }

    void UpdatePose()
    {
        if (!isPlaying || index >= headData.poses.Count)
            return;

        //cameraTrans.position = headData.poses[i].position;
        //cameraTrans.rotation = headData.poses[i].rotation;

        HeadPose currentPose = headData.poses[index];
        HeadPose nextPose;

        if (index < headData.poses.Count - 1)
        {
            nextPose = headData.poses[index + 1];
        }
        else
        {
            return;
        }

        var portion = Mathf.InverseLerp(currentPose.time, nextPose.time, time);
        cameraTrans.position = Vector3.Lerp(currentPose.position, nextPose.position, portion);
        cameraTrans.rotation = Quaternion.Slerp(currentPose.rotation, nextPose.rotation, portion);

        if (portion >= 1)
        {
            index++;
        }

        time += Time.deltaTime;
    }

    IEnumerator CoPlayHeadData()
    {
        //var index = headData.poses.FindLastIndex(p => p.time <= time);

        isPlaying = true;

        while (index < headData.poses.Count && isPlaying)
        {
            //cameraTrans.position = headData.poses[i].position;
            //cameraTrans.rotation = headData.poses[i].rotation;

            HeadPose currentPose = headData.poses[index];
            HeadPose nextPose;

            if(index < headData.poses.Count - 1)
            {
                nextPose = headData.poses[index + 1];
            }
            else
            {
                break;
            }

            var portion = Mathf.InverseLerp(currentPose.time, nextPose.time, time);
            cameraTrans.position = Vector3.Lerp(currentPose.position, nextPose.position, portion);
            cameraTrans.rotation = Quaternion.Slerp(currentPose.rotation, nextPose.rotation, portion);

            if (portion >= 1)
            {
                index++;
            }

            yield return null;
            time += Time.deltaTime;
        }

        yield return null;
    }

}
