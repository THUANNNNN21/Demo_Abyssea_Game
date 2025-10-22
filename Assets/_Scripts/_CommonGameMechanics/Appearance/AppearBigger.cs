
using UnityEngine;
public class AppearBigger : ObjAppearing
{
    // [Header("Grow Bigger Settings")]
    // [SerializeField] private float growSpeed = 2f; // units per second
    // [SerializeField] private float maxScale = 1f;
    // [SerializeField] private float currentScale = 0f;

    // private Vector3 scaleVector = Vector3.zero; // Cache vector
    // private void Start()
    // {
    //     this.InitScale();
    //     this.TriggerOnAppearing();
    // }
    // private void FixedUpdate()
    // {
    //     if (isAppearing)
    //     {
    //         Appearing();
    //     }
    // }
    // private void InitScale()
    // {
    //     this.transform.parent.localScale = Vector3.zero;
    //     this.currentScale = 0f;
    // }
    // protected override void Appearing()
    // {
    //     // Tăng scale theo thời gian thực
    //     currentScale = Mathf.Min(currentScale + growSpeed * Time.fixedDeltaTime, maxScale);

    //     // Kiểm tra hoàn thành trước khi set scale
    //     if (currentScale >= maxScale)
    //     {
    //         this.FullyAppeared();
    //         return;
    //     }

    //     // Set scale với cached vector
    //     scaleVector.Set(currentScale, currentScale, currentScale);
    //     this.transform.parent.localScale = scaleVector;
    // }
    // protected override void FullyAppeared()
    // {
    //     // Đảm bảo scale chính xác
    //     scaleVector.Set(maxScale, maxScale, maxScale);
    //     this.transform.parent.localScale = scaleVector;

    //     base.FullyAppeared();
    // }
    // // public bool CanShoot()
    // // {
    // //     return this.isFullyAppeared;
    // // }

}

