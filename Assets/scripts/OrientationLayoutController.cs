using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationLayoutController : MonoBehaviour
{

//Source: https://forum.unity.com/threads/swap-from-horizontal-to-vertical-layout-group.485034/

    public VerticalLayoutGroup verticalGroup;
    public HorizontalLayoutGroup horizontalGroup;
    public GameObject[] elements;

    public
 
    void ChangeVerticalLayout()
    {
        verticalGroup.gameObject.SetActive ( true );
        horizontalGroup.gameObject.SetActive ( false );
        foreach ( GameObject element in elements )
        {
            element.transform.SetParent ( verticalGroup.gameObject.transform );
        }
    }
 
    void ChangeHorizontalLayout()
    {
        verticalGroup.gameObject.SetActive ( false );
        horizontalGroup.gameObject.SetActive ( true );
        foreach ( GameObject element in elements )
        {
            element.transform.SetParent ( horizontalGroup.gameObject.transform );
        }
    }
 
}
