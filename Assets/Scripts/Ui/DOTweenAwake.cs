using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DOTweenAwake : MonoBehaviour 
{
	private void Awake()
	{
		DOTween.Init(true, true, LogBehaviour.Default);
	}

}
