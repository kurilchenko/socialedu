using UnityEngine;
using System.Collections;

public class SetAnimatorTrigger : SequenceElement
{

    public Animator animator;
    public string name;
    public float delay;
    public float doneAfter = 10f;

	void OnEnable()
    {
        Invoke("SetTrigger", delay);
	}
	
	void SetTrigger()
    {
        Debug.Log(animator.name + " gets set to trigger " + name);
        animator.SetTrigger(name);
    }

    IEnumerator CoDoneAfter()
    {
        yield return new WaitForSeconds(doneAfter);

        isDone = true;        
    }
}
