using UnityEngine;

public class ScoreText : BaseText
{
    public void UpdateScoreUI(int score)
    {
        uiText.text = "Score: " + score.ToString();
    }
}
