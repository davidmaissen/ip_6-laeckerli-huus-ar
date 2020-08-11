using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject topBar;
    //Controller
    private OrientationLayoutController layoutController;

    //Toggles ingredientsBar
    public void animateIngredientsBar(Animator animation)
    {
        animation.SetBool("expanded", !animation.GetBool("expanded"));
    }

     public void ToggleVisible(Animator animation)
    {
        //var anim = GetComponent<Animator>();
        animation.SetBool("displayed", !animation.GetBool("displayed"));
    }

    //Toggles menu- and info-panel for landscape and portrait view
     public void TogglePanel(GameObject panel)
    {
        layoutController = GetComponent<OrientationLayoutController>();

        if(layoutController.IsPortrait())
        {
            Animator animatorPortrait = panel.transform.GetChild(0).transform.GetComponent<Animator>();      
            animatorPortrait.SetBool("displayed", !animatorPortrait.GetBool("displayed"));   
        }
        else
        {
            Animator animatorLandscape = panel.transform.GetChild(1).transform.GetComponent<Animator>();
            animatorLandscape.SetBool("displayed", !animatorLandscape.GetBool("displayed"));
            topBar.SetActive(!topBar.active);           
        }
    } 


}
