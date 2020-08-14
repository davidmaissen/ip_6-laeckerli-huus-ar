using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Based on source: https://forum.unity.com/threads/device-screen-rotation-event.118638/?_ga=2.268232782.134089061.1595601046-2023691235.1592315311

public class OrientationLayoutController : MonoBehaviour
{
    RectTransform rectTransform;
    public GameObject[] panelsInfo;
    public GameObject[] panelsSettings;
    public GameObject gameDetail;
    public GameObject button;
    public GameObject[] infoItemGroup;
    public GameObject[] nestedInfoItemGroup;
    public GameObject[] settingsItemGroup;
    public GameObject[] nestedInfoSettingsGroup;
    private Scene activeScene;

    void Awake()
    {
        activeScene = SceneManager.GetActiveScene();

        // lock orientation based on current scene
        switch(activeScene.name)
        {
             case "start":
             case "tutorial":
             Screen.orientation = ScreenOrientation.Portrait;
             break;

             default:
             Screen.orientation = ScreenOrientation.AutoRotation;
             break;
        }
        rectTransform = GetComponent<RectTransform>();
        UpdateLayout();
    }
    void UpdateLayout()
    {
        if(rectTransform == null) return;

        setView();

        if(rectTransform.rect.width < rectTransform.rect.height){

            // setup layout for portrait view
            GameObject nestedLayoutGroupInfo = panelsInfo[0].transform.Find("Menu-Area/Panel-Background-Portrait/Label-Ingredients").gameObject;
            GameObject panelInfo = panelsInfo[0].transform.Find("Menu-Area/Panel-Background-Portrait").gameObject;

            GameObject nestedLayoutGroupSettings = panelsSettings[0].transform.Find("Menu-Area/Panel-Background-Portrait/Label-Settings-Items").gameObject;
            GameObject panelSettings = panelsSettings[0].transform.Find("Menu-Area/Panel-Background-Portrait").gameObject;

            CreateGrouping(panelInfo, nestedLayoutGroupInfo, nestedLayoutGroupSettings, panelSettings);
            LayoutRebuilder.ForceRebuildLayoutImmediate(panelInfo.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(panelSettings.GetComponent<RectTransform>());
        }
        else
        {
            // setup layout for landscape view
            GameObject nestedLayoutGroupInfo = panelsInfo[1].transform.Find("Menu-Area/Panel-Background-Landscape/Label-Ingredients").gameObject;
            GameObject panelInfo = panelsInfo[1].transform.Find("Menu-Area/Panel-Background-Landscape").gameObject;

            GameObject nestedLayoutGroupSettings = panelsSettings[1].transform.Find("Menu-Area/Panel-Background-Landscape/Label-Setting-Items").gameObject;
            GameObject panelSettings = panelsSettings[1].transform.Find("Menu-Area/Panel-Background-Landscape").gameObject;

            CreateGrouping(panelInfo, nestedLayoutGroupInfo, nestedLayoutGroupSettings, panelSettings);
            LayoutRebuilder.ForceRebuildLayoutImmediate(panelInfo.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(panelSettings.GetComponent<RectTransform>());
        }
    }
    
    public bool IsPortrait()
    {
        return rectTransform.rect.width < rectTransform.rect.height;
    }
   
    // add the menu items to the specific Menu Panel
    private void CreateGrouping(GameObject panelInfo, GameObject nestedLayoutGroupInfo, GameObject nestedLayoutGroupSettings, GameObject panelSettings)
    {

            // add all menu info items to panel
            foreach(GameObject item in infoItemGroup)
            {
                item.transform.SetParent ( panelInfo.gameObject.transform );
                if(item.name.Equals("Label-Ingredients-Detail") && IsPortrait())
                {
                    item.transform.SetSiblingIndex(0);
                }
                else if (item.name.Equals("Label-Ingredients-Detail") && !IsPortrait())
                {
                    item.transform.SetSiblingIndex(1);
                }
            }
             // add all menu info items to nested panel
            foreach(GameObject item in nestedInfoItemGroup)
            {
               item.transform.SetParent (nestedLayoutGroupInfo.gameObject.transform);
            }

            // add all menu settings items to panel
            foreach(GameObject item in settingsItemGroup)
            {
                item.transform.SetParent ( panelSettings.gameObject.transform );
                if(item.name.Equals("Label-Settings") && IsPortrait())
                {
                    item.transform.SetSiblingIndex(0);
                }
                else if (item.name.Equals("Label-Settings") && !IsPortrait())
                {
                    item.transform.SetSiblingIndex(1);
                }
            }

            // add all menu setting item to nested panel
            foreach(GameObject item in nestedInfoSettingsGroup)
            {
               item.transform.SetParent (nestedLayoutGroupSettings.gameObject.transform);
            }
    }

    private void setView()
    {
            if(IsPortrait()){
                panelsInfo[0].SetActive(true);
                panelsInfo[1].SetActive(false);

                panelsSettings[0].SetActive(true);
                panelsSettings[1].SetActive(false);
            }
            else
            {
                panelsInfo[0].SetActive(false);
                panelsInfo[1].SetActive(true);

                panelsSettings[0].SetActive(false);
                panelsSettings[1].SetActive(true);                
            }  
    }

    void OnRectTransformDimensionsChange()
    {
        UpdateLayout();
    }


 

}