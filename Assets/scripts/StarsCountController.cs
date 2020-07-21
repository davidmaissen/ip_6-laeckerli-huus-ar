using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsCountController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;

    // Start is called before the first frame update
    void Start()
    {
        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        updateStarsCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void updateStarsCounter()
    {
        //set value of starsCounter
        Debug.Log(GameProgress.starsCollected.ToString());
        starsCounter.text = GameProgress.starsCollected.ToString();

    }
}
