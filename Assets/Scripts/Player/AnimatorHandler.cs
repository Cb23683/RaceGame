using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    private Animator anim;

    public bool canRotate = true;

    public void Initialize()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void UpdateAnimatorValues(float vMov, float hMov)
    {
        anim.SetFloat("vertical", vMov, .1f, Time.deltaTime);
            
    }

    public void StopRotation()
    {
        canRotate = false;
    }

    public void StartRotation()
    {
        canRotate = true;
    }
}
