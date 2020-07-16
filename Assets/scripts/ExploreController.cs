using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExploreController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;

    // Start is called before the first frame update
    void Start()
    {
        //TextMeshPro textmeshPro = starsCounter.GetComponent<TextMeshPro>();
        //starsCounter.SetText();

        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        starsCounter.text = GameProgress.starsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
