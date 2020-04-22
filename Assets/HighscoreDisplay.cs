using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreDisplay : MonoBehaviour
{
    public IntVariable score;
    public TextMeshProUGUI highscoreText;

    int highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    public void DisplayHigscore ()
    {
        if (score.RuntimeValue > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score.RuntimeValue);
            highscore = score.RuntimeValue;
        }

        highscoreText.text = "BEST" + '\n' + highscore;
    }
}
