﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animateIngredientsBar(Animator animation)
    {
        animation.SetBool("expanded", !animation.GetBool("expanded"));

    }

    public void ToggleVisible(Animator animation)
    {
        //var anim = GetComponent<Animator>();
        animation.SetBool("displayed", !animation.GetBool("displayed"));
    }

}
