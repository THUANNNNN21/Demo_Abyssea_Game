using UnityEngine;

public class TimerText : BaseText
{
    public void UpdateTimerUI(float time)
    {
        uiText.text = Mathf.CeilToInt(time).ToString();
    }
}
