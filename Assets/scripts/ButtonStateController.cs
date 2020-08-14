using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateController : MonoBehaviour
{
    public Sprite imageInactive;
    public Sprite imageActive;
    public bool isButtonGroup;
    public bool isActive;

    public void Initialize()
    {
        this.transform.GetComponent<Button>().onClick.Invoke();
        this.transform.GetComponent<Image>().sprite = this.imageActive;
    }

    // change appearance of clicked button
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

        if (isButtonGroup)
        {
            GameObject buttonGroup = buttonClicked.transform.parent.gameObject;
            for (int i = 0; i < buttonGroup.transform.childCount; i++)
            {
                GameObject button =  buttonGroup.transform.GetChild(i).gameObject;
                if(button != buttonClicked)
                {
                    Sprite image = button.GetComponent<ButtonStateController>().imageInactive;
                    button.transform.GetComponent<Image>().sprite = image;
                }
            }
        }
    }
}
