using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject ingredientIcons;
    public GameObject ingredientText;

    public void DisableMenu(Animator animation){
        animation.SetBool("displayed", false);
    }

    public void EnableMenu(Animator animation)
    {
        animation.SetBool("displayed", true);
    }


    public void SetActive(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }

    public void SetInActive(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }


    //public void changeIngredientsBar(Animator animation)
    //{

    //    if(animation.GetBool("expanded") == true){
    //        animation.SetBool("expanded", false);
    //    }
    //    else{
    //        animation.SetBool("expanded", true);
    //    }

      
    //}

    public void ChangeIngredientsBar(Animator animation)
    {

        if (animation.GetBool("expanded") == true)
        {
            animation.SetBool("expanded", false);
            ingredientIcons.SetActive(true);
            //legend.SetActive(true);
        }
        else
        {
            animation.SetBool("expanded", true);
            ingredientIcons.SetActive(false);
            //legend.SetActive(false);
        }


    }


}
