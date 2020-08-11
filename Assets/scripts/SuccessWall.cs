using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SuccessWall : MonoBehaviour
{
    public TextMeshPro playerNameLabel;
    public TextMeshPro collectedStarsLabel;
    private void Awake() {
        playerNameLabel.text = GameProgress.playerName;
        collectedStarsLabel.text = GameProgress.starsCollected.ToString();
    }
}
