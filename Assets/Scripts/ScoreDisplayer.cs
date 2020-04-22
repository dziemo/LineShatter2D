using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public IntVariable score, multiplier;
    public FloatVariable multiplierTimer, maxTimer;
    public TextMeshProUGUI scoreText;
    public Image timerImage;

    private void Update()
    {
        scoreText.text = score.RuntimeValue + "x" + multiplier.RuntimeValue;
        timerImage.fillAmount = multiplierTimer.RuntimeValue / maxTimer.RuntimeValue;
    }

}
