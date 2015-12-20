using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class PosePlayer : MonoBehaviour
{

    public string recordName;
    public Transform cameraTrans;
    public HeadData headData;

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
        var i = 0;

        float elapsedTime = 0;

        while (i < headData.poses.Count)
        {
            //cameraTrans.position = headData.poses[i].position;
            //cameraTrans.rotation = headData.poses[i].rotation;

            HeadPose currentPose = headData.poses[i];
            HeadPose nextPose;

            if(i < headData.poses.Count - 1)
            {
                nextPose = headData.poses[i + 1];
            }
            else
            {
                break;
            }

            var portion = Mathf.InverseLerp(currentPose.time, nextPose.time, elapsedTime);

            cameraTrans.position = Vector3.Lerp(currentPose.position, nextPose.position, portion);
            cameraTrans.rotation = Quaternion.Slerp(currentPose.rotation, nextPose.rotation, portion);

            if (portion >= 1)
            {
                i++;
            }

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        yield return null;
    }

}
