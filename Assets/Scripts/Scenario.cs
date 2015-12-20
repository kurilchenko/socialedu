using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Scenario : SingletonComponent<Scenario>
{

    public List<ScenarioSequence> sequences = new List<ScenarioSequence>();
    int currentSequenceIndex = -1;

    void Start()
    {
        //sequences = GetComponentsInChildren<ScenarioSequence>().ToList();

        foreach (var sequence in sequences)
        {
            sequence.gameObject.SetActive(false);
        }

        Next();
    }

    public void Next()
    {
        if (currentSequenceIndex == sequences.Count - 1)
            return;

        if (currentSequenceIndex > -1)
        {
            sequences[currentSequenceIndex].gameObject.SetActive(false);
        }

        sequences[currentSequenceIndex + 1].gameObject.SetActive(true);

        sequences[currentSequenceIndex + 1].StartSequence();

        currentSequenceIndex++;
    }

    /*
    void Update()
    {
        int elementsDone = 0;

        foreach (var element in sequences[currentSequenceIndex].elements)
        {
            if (element.isDone)
                elementsDone++;
        }

        Debug.Log(elementsDone + " " + sequences[currentSequenceIndex].elements.Count);

        if (elementsDone >= sequences[currentSequenceIndex].elements.Count - 1)
        {
            Next();
        }
    }
    */

}
