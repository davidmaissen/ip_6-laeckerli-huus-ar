using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSuccessController : MonoBehaviour
{
    public GameObject successCanvas;
    public GameObject uiController;
    public GameObject stars;

    public GameObject starsTop;

    public TextMeshProUGUI objCounter;

    private StarStateController starStateController;


    void Start()
    {
        starStateController = uiController.GetComponent<StarStateController>();
        starStateController.setStarGroup(starsTop, 0);
        setCounter(0);
    }

    public void setCounter(int count)
    {
        objCounter.text = count.ToString();
    }

    public void updateProgress(int count, int starsCount)
    {
        objCounter.text = count.ToString();
        starStateController.setStarGroup(starsTop, starsCount);
    }

     public void showSuccessPanel(int starsCount)
    {
        successCanvas.SetActive(true);
        starStateController = uiController.GetComponent<StarStateController>();
        starStateController.setStarGroup(stars, starsCount);


    }
}
