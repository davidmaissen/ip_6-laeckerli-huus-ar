using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSettingsController : MonoBehaviour
{
    public GameObject menuSetting;
    public GameObject menuHome;


    // Start is called before the first frame update
    void Start()
    {
            menuSetting.SetActive(true);
            menuHome.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPanel(string name)
    {
        switch (name)
        {
            case "Settings":
            menuSetting.SetActive(true);
            menuHome.SetActive(false);
            break;

             case "Home":
            menuSetting.SetActive(false);
            menuHome.SetActive(true);
            break;
        }
    }
}
