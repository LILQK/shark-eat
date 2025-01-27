using DG.Tweening;
using UnityEngine;

public class Shake : MonoBehaviour
{
    
    void Start()
    {
        transform.DOShakePosition(5, 4f,fadeOut:false).SetEase(Ease.Linear).SetLoops(-1);
    }
}
