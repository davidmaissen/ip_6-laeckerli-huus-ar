using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateController : MonoBehaviour
{
    public Sprite imageInactive;
    public Sprite imageActive;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeImage(GameObject buttonClicked)
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



    }
}
