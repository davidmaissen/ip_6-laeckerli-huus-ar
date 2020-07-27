using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateController : MonoBehaviour
{
    public Sprite imageInactive;
    public Sprite imageActive;

    public bool isButtonGroup;
  //  public GameObject[] ingredientButtons;
    //public GameObject[] languageButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setButtonStates()
    {
        GameObject buttonClicked = this.gameObject;


        if (buttonClicked.transform.GetComponent<Image>().sprite == imageActive)
        {
            buttonClicked.transform.GetComponent<Image>().sprite = imageInactive;
        }
        else
        {
            buttonClicked.transform.GetComponent<Image>().sprite = imageActive;
        }

        if(isButtonGroup)
        {
            GameObject buttonGroup = buttonClicked.transform.parent.gameObject;
            for (int i = 0; i < buttonGroup.transform.childCount; i++)
            {
                GameObject button =  buttonGroup.transform.GetChild(i).gameObject;
                if(button != buttonClicked)
                 {
                button.transform.GetComponent<Image>().sprite = imageInactive;
                }
            }

        }
    }
}

        
      //  for (int i = 0; i < gameObject.transform.childCount; i++)
       // {
        //}
     /*        GameObject buttonGroup = this.gameObject;
            GameObject button =  buttonGroup.transform.GetChild(i).gameObject;
            if(button == buttonClicked)
            {
                if (button.transform.GetComponent<Image>().sprite == imageActive)
                {
                    button.transform.GetComponent<Image>().sprite = imageInactive;
                }
                else
                {
                    button.transform.GetComponent<Image>().sprite = imageInactive;
                }

            } */
            
            //gameObject.transform.GetChild(i).transform.GetComponent<Image>().sprite = imageActive;


/*     public void changeImage(GameObject buttonClicked)
    {
        foreach(GameObject button in buttons)
        {
            if(button == buttonClicked)
            {
                if (buttonClicked.GetComponent<Image>().sprite == imageActive)
                {
                    buttonClicked.GetComponent<Image>().sprite = imageInactive;
                }
                else
                {
                    buttonClicked.GetComponent<Image>().sprite = imageActive;
                }
            }
            else
            {
                button.GetComponent<Image>().sprite = imageInactive;
            }
        }



    } */
