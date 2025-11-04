using UnityEngine;

public class FinalScoreText : BaseText
{
    public void UpdateFinalScoreUI(int score)
    {
        uiText.text = "Final Score\n" + score.ToString();
    }
}
