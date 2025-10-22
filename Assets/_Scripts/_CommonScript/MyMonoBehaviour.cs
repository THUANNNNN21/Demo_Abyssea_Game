using UnityEditor.PackageManager.Requests;
using UnityEngine;

public abstract class MyMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
        this.LoadValues();
    }
    protected virtual bool ShouldAutoLoad()
    {
        return false;
    }
    // Các hàm virtual để script con tùy biến
    protected void Reset()
    {
        this.LoadComponents();
        this.LoadValues();
    }
    protected virtual void LoadComponents() { }
    protected virtual void LoadValues() { }
}