using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartScreen : MonoBehaviour
{
    [Header ("Move Components")]
    [SerializeField] private Move move;
    [SerializeField] private SwipeDetection swipeDetection;

    [Header("UI")]
    [SerializeField] private GameObject fineger;
    [SerializeField] private Image finegerImage;

    private void OnEnable()
    {
        MoveFinger();
        move.enabled = false;
        swipeDetection.SwipeEvent += OnSwipe;
    }
    private void OnSwipe(Vector2 swipeDirection)
    {
        if (swipeDirection == Vector2Int.right)
        {
            move.enabled = true;            
            Invoke(nameof(HidePanel), 0.1f);
        }
    }  

    private void HidePanel()
    {        
        move.SetDirection(Vector2Int.right);
        gameObject.SetActive(false);
    }

    private void MoveFinger()
    {
        Vector3 endRotate = fineger.transform.rotation.eulerAngles - Vector3.forward * 60f;

        Color alpha = new Color(1, 1, 1, 0);
        Color nonAlpha = new Color(1, 1, 1, 1);

        Sequence fingerAlpha = DOTween.Sequence();
        fingerAlpha.Append(finegerImage.DOColor(nonAlpha, 0.5f)).Append(finegerImage.DOColor(alpha, 0.5f));
        fingerAlpha.SetLoops(-1);

        fineger.transform.DORotate(endRotate, 1f,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);      
    }

}
