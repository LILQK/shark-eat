using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text scoreText;
    private int currentScore = 0;
    
    private float originalX;
    private float originalY;
    private void Start()
    {
        GameManager.Instance.OnNewRoundStarted.AddListener(NewRoundStarted);
        GameManager.Instance.OnRoundEnded.AddListener(OnRoundEnded);
        originalX = scoreText.rectTransform.anchoredPosition.x;
        originalY = scoreText.rectTransform.anchoredPosition.y;
        scoreText.rectTransform.anchoredPosition = new Vector2(-25f,-20);
    }

    private void OnRoundEnded()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        scoreText.rectTransform.anchoredPosition = new Vector2(-25f,-20);

    }

    private void NewRoundStarted()
    {
        currentScore = 0;
        scoreText.rectTransform.DOAnchorPos(new Vector2(originalX,originalY), 1f).SetEase(Ease.InBack);
    }

    public void AddScore(int player)
    {
        currentScore++;
        scoreText.text = currentScore.ToString("00");
    }

    public void RemoveScore(int i)
    {
        currentScore -= i;
        scoreText.DOColor(Color.red,.2f).SetLoops(4, LoopType.Yoyo).From(Color.white).OnComplete(() =>
        {
            
            scoreText.color = Color.white;
        });
        scoreText.text = currentScore.ToString("00");
    }
}
