using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class OrientationLayoutController : MonoBehaviour
{
    //https://forum.unity.com/threads/device-screen-rotation-event.118638/?_ga=2.268232782.134089061.1595601046-2023691235.1592315311

    RectTransform rectTransform;
    public GameObject[] panelsInfo;
    public GameObject[] panelsSettings;
    public GameObject gameDetail;

    public GameObject button;

    public GameObject[] infoItemGroup;

    public GameObject[] nestedInfoItemGroup;
    public GameObject[] settingsItemGroup;


    //public GameObject landscape;
    //public GameObject portrait;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateLayout();
        //landscape = transform.Find("landscape").gameObject;
        //portrait = transform.Find("portrait").gameObject;
        //SetOrientation();
    }
    void UpdateLayout()
    {
        if(rectTransform == null) return;

        if(rectTransform.rect.width < rectTransform.rect.height){
            Debug.Log("Layout for Portrait");

            //setup Layout for PortraitView
            GameObject nestedLayoutGroupInfo = panelsInfo[0].transform.Find("Menu-Area/Panel-Background-Portrait/Label-Ingredients").gameObject;
            GameObject panelInfo = panelsInfo[0].transform.Find("Menu-Area/Panel-Background-Portrait").gameObject;

            //GameObject nestedLayoutGroupInfo = panelPortrait.transform.Find("Menu-Area/Panel-Background-Portrait/Label-Ingredients").gameObject;
            GameObject panelSettings = panelsSettings[0].transform.Find("Menu-Area/Panel-Background-Portrait").gameObject;

            CreateGrouping(panelInfo, nestedLayoutGroupInfo, panelSettings);
        }
        else
        {
            Debug.Log("Layout for Landscape");
            //setup Layout for LandscapeView
            GameObject nestedLayoutGroupInfo = panelsInfo[1].transform.Find("Menu-Area/Panel-Background-Landscape/Label-Ingredients").gameObject;
            GameObject panelInfo = panelsInfo[1].transform.Find("Menu-Area/Panel-Background-Landscape").gameObject;

            GameObject panelSettings = panelsSettings[1].transform.Find("Menu-Area/Panel-Background-Landscape").gameObject;

            CreateGrouping(panelInfo, nestedLayoutGroupInfo, panelSettings);
        }
    }
    
    public bool IsPortrait()
    {
        return rectTransform.rect.width < rectTransform.rect.height;
    }
   
    //add the menu items to the specific Menu Panel
    private void CreateGrouping(GameObject panelInfo, GameObject nestedLayoutGroupInfo, GameObject panelSettings)
    {

            //Add all Menu-Info items to panel
            foreach(GameObject item in infoItemGroup)
            {
                item.transform.SetParent ( panelInfo.gameObject.transform );
                if(item.name.Equals("Label-Ingredients-Detail"))
                {
                    item.transform.SetSiblingIndex(0);
                }
            }
            //Add all Menu-Info nested items to panel


                    foreach(GameObject item in nestedInfoItemGroup)
                    {
                    item.transform.SetParent (nestedLayoutGroupInfo.gameObject.transform);
                    }
    


            //Add all Menu-Settings items to panel
            foreach(GameObject item in settingsItemGroup)
            {
                item.transform.SetParent ( panelSettings.gameObject.transform );
                if(item.name.Equals("Label-Settings"))
                {
                    item.transform.SetSiblingIndex(0);
                }
            }


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