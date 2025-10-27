using UnityEngine;

public class LevelFX : MonoBehaviour
{
    public void SelfDisable()
    {
        this.gameObject.SetActive(false);
    }
    public void SelfEnable()
    {
        this.gameObject.SetActive(true);
        // Debug.Log("LevelFX: Level Up Animation Played");
        Invoke(nameof(SelfDisable), 0.5f);
    }
}
