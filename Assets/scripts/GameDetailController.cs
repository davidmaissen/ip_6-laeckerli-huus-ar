using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDetailController : MonoBehaviour
{
    public GameObject gameDetails;
    public GameObject buttonActive;
    public GameObject[] stars;

    public Image labelIcon;

    //Controller
    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();
        updateGameDetails(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGameDetails(int gameID)
    {
       // string tag = obj.tag;
     //   int gameID = Int32.Parse(tag.Substring(8));

        TextMeshProUGUI gameTitle = gameDetails.transform.Find("Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI gameStatus = gameDetails.transform.Find("Status-Game/Status-Text").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI gameHighScore = gameDetails.transform.Find("HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();
       // Sprite labelIcon = gameDetails.transform.Find("Label-Image").gameObject.transform.GetComponent<Image>().sprite;



/*         foreach(GameObject icon in ingredientIcons)
        {
            if (icon.name.Equals(name))
            {
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.gameObject.SetActive(false);
            }
        } */

        (string title, int stars, string status, int highScore) details = gameProgress.getGameDetail(gameID);
        gameTitle.text = details.Item1;
        gameStatus.text = details.Item3;
        gameHighScore.text = details.Item4.ToString();
        labelIcon.sprite = gameProgress.GetIngredientIcon(gameID, false);

        for (int i = 0; i < stars.Length; i++)
        {
            if(i < details.Item2)
            {
                stars[i].transform.Find("active").gameObject.SetActive(true);
                stars[i].transform.Find("inactive").gameObject.SetActive(false);
            }
            else
            {
                stars[i].transform.Find("active").gameObject.SetActive(false);
                stars[i].transform.Find("inactive").gameObject.SetActive(true);
            }
          
        }

    }
}
