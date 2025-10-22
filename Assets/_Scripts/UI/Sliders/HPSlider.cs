using UnityEngine;

public class HPSlider : BaseSlider
{
    [SerializeField] private int HP;
    [SerializeField] private int HPMax;
    protected override void OnChanged(float value)
    { }
    protected void FixedUpdate()
    {
        this.ShowHP();
    }
    protected virtual void ShowHP()
    {
        float hpPercent = (float)this.HP / this.HPMax;
        this.uiSlider.value = hpPercent;
    }
    public void SetHP(int HP)
    {
        this.HP = HP;
    }
    public void SetHPMax(int HPMax)
    {
        this.HPMax = HPMax;
    }
}
