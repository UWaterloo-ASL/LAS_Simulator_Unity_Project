using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSMAContraction : MonoBehaviour
{
    Animator SMA_animator;
    public float SMA_action;

    // Start is called before the first frame update
    void Start()
    {
        SMA_animator = GetComponent<Animator>();
    }

    public void SetSMAAction(float action)
    {
        SMA_action = action;
    }

    // Update is called once per frame
    void Update()
    {
        if (SMA_action > 0)
        {
            SMA_animator.SetTrigger("SMAContractionTrigger");
        }
    }
}
