using UnityEngine;
using System.Collections;

public class SequenceAutoPlay : MonoBehaviour
{

	void Start ()
    {
        GetComponent<Awespace.Sequence>().Play();
	}

}
