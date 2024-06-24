using UnityEngine;
using DG.Tweening;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1;

    private Tweener rotateTween;

    void Start()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        rotateTween = transform.DORotate(new Vector3(0, 360, 0), rotationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .OnComplete(() => StartRotation());
    }

    // Останавливаем вращение
    public void StopRotation()
    {
        if (rotateTween != null)
        {
            rotateTween.Kill();
        }
    }
}