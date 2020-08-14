using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IngredientController : MonoBehaviour
{

    public GameObject test;
    public Sprite testSprite;

    void Start()
    {
        Sprite sp  = Resources.Load<Sprite>("Sprites/tower");
        test.transform.GetComponent<Image>().sprite = sp;
    }
}
