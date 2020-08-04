using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDetailController : MonoBehaviour
{
    public GameObject gameDetails;
    // public GameObject buttonActive;
    public GameObject[] stars;
    public int activeGame;
    public GameObject labelIcon;
    public bool isUi;

    //Controller
    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();
        updateGameDetails(activeGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGameDetails(int gameID)
    {
        Debug.Log("UpdateGameDetail");
       // string tag = obj.tag;
     //   int gameID = Int32.Parse(tag.Substring(8));
        (string title, int stars, string status, int highScore) details = gameProgress.getGameDetail(gameID);

        if (isUi) {
            TextMeshProUGUI gameTitle = gameDetails.transform.Find("Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI gameStatus = gameDetails.transform.Find("Status-Game/Status-Text").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI gameHighScore = gameDetails.transform.Find("HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();
            gameTitle.text = details.Item1;
            gameStatus.text = details.Item3;
            gameHighScore.text = details.Item4.ToString();
            labelIcon.gameObject.GetComponent<Image>().sprite = gameProgress.GetIngredientIcon(gameID, false);
        } else {
            GameObject gameDetailsUpdated = gameDetails;
            TextMeshPro gameTitle = gameDetailsUpdated.transform.Find("Title-Game").gameObject.GetComponent<TextMeshPro>();
            TextMeshPro gameStatus = gameDetailsUpdated.transform.Find("Status-Game/Status-Text").gameObject.GetComponent<TextMeshPro>();
            TextMeshPro gameHighScore = gameDetailsUpdated.transform.Find("HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshPro>();
            gameTitle.text = details.Item1;
            gameStatus.text = details.Item3;
            gameHighScore.text = details.Item4.ToString();
            labelIcon.GetComponent<Renderer>().material = gameProgress.GetMaterial(gameID);
        }

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
        Debug.Log("UpdateGameDetail - Updated Stars");

    }
}
