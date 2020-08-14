using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarStateController : MonoBehaviour
{
    public Sprite imageInactive;
    public Sprite imageActive;

    public void setStarGroup(GameObject stars, int starsCount)
    {

        for (int i = 0; i < stars.transform.childCount; i++)
        {   
            if (i <= starsCount-1)
            {
                stars.transform.GetChild(i).transform.GetComponent<Image>().sprite = imageActive;
            }
            else
            {
                stars.transform.GetChild(i).transform.GetComponent<Image>().sprite = imageInactive;
            }
        }
    }

}
