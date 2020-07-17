using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExploreController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;
    public GameObject[] ingredients;
    private GameProgress gameProgress;

    // Start is called before the first frame update
    void Start()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        starsCounter.text = GameProgress.starsCollected.ToString();


        for (int i = 0; i < ingredients.Length; i++)
        {
            string name = ingredients[i].name;
         
            if(gameProgress.isGameCompleted(name))
            {
                ingredients[i].transform.Find("complete").gameObject.SetActive(true);
                ingredients[i].transform.Find("incomplete").gameObject.SetActive(false);
            }
            else
            {
                ingredients[i].transform.Find("complete").gameObject.SetActive(false);
                ingredients[i].transform.Find("incomplete").gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
