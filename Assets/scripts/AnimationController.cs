using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private OrientationLayoutController layoutController;
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
            Debug.Log("Button pressed");
            Animator animatorLandscape = panel.transform.GetChild(1).transform.GetComponent<Animator>();
            Debug.Log(panel.transform.GetChild(1).name);
            animatorLandscape.SetBool("displayed", !animatorLandscape.GetBool("displayed"));
            
        }
        //var anim = GetComponent<Animator>();
      //  animation.SetBool("displayed", !animation.GetBool("displayed"));
    } 


}
