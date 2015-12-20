using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ScenarioSequence : MonoBehaviour
{

    public List<SequenceElement> elements = new List<SequenceElement>();
    public float duration = 10f;

    public void StartSequence()
    {
        Invoke("SwitchToNext", duration);
    }

    public void SwitchToNext()
    {
        GetComponentInParent<Scenario>().Next();
    }

    void OnEnable()
    {
        elements = GetComponentsInChildren<SequenceElement>().ToList();


    }

}
