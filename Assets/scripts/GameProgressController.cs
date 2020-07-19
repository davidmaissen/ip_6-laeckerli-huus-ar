using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameProgressController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;

    // Start is called before the first frame update
    void Start()
    {
        updateStarsCounter(starsCounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void updateStarsCounter(TextMeshProUGUI starsCounter)
    {
        //set value of starsCounter
        Debug.Log(GameProgress.starsCollected.ToString());
        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        starsCounter.text = GameProgress.starsCollected.ToString();

    }
}
