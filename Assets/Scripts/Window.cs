using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour
{
    [HideInInspector]
    public Awespace.Sequence sequence;
    public float startTime;
    public bool isPlaying { get; private set; }
    public bool isAlreadyPlayed;

    SelectArea selectArea;

    float EndTime
    {
        get
        {
            return startTime + 0.3f;
        }
    }

    void Start()
    {
        selectArea = GetComponentInChildren<SelectArea>();
    }

	void Update ()
    {
        if (sequence == null)
            return;

        if (sequence.RunningTime >= startTime && sequence.RunningTime <= EndTime)
        {
            Play();
        }

        if (sequence.RunningTime < startTime || sequence.RunningTime > EndTime)
        {
            isAlreadyPlayed = false;
        }
	}

    void Play()
    {
        if (isAlreadyPlayed)
            return;

        isAlreadyPlayed = true;

        sequence.Pause();
        selectArea.Open();
    }

}
