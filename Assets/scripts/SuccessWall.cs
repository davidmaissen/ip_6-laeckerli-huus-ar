using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SuccessWall : MonoBehaviour
{
    public TextMeshPro playerNameLabel;
    private void Awake() {
        playerNameLabel.text = GameProgress.playerName;
    }
}
