using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text fansCountText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        fansCountText.text = "You have " + score.ToString() + " FANS";
    }
}
