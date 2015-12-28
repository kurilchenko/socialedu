using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Windows : MonoBehaviour
{
    public Awespace.Sequence sequence;

    List<Window> list;

	void Start ()
    {
        list = GetComponentsInChildren<Window>().ToList();

        foreach (var window in list)
        {
            window.sequence = sequence;
        }
	}

}
