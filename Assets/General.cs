using UnityEngine;
using System.Collections;

public class General : MonoBehaviour
{
	void Start ()
    {

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        
#endif

#if UNITY_STANDALONE
        Application.targetFrameRate = 90;
#endif

    }
}
