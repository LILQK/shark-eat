using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int roundsPerPlayer = 2; // Cuántas rondas juega cada rol
    public float roundDuration = 60f; // Duración de cada ronda en segundos
    public int totalFish = 0; // Cantidad de peces en la escena

    public int currentRound = 1;
    public int globalRound = 1;
    private bool isSharkTurn = true; // Indica si es turno del tiburón
    public float timer;
    public bool gameRunning = false;
    
    [Header("References")]
    public FishMovement shark;

    public Collider2D sharkCollider;
    public PlayerController canon;
    public Material vignette;
    public TMPro.TMP_Text titleText;
    public FishSpawner fishSpawner;
    public AudioSource openAudio;
    public AudioSource closeAudio;
    public GameObject firstScreen;
    public VideoPlayer VideoPlayer;
    public UnityEvent OnNewRoundStarted = new UnityEvent();
    public UnityEvent OnRoundEnded = new UnityEvent();
    public AudioSource defaultMusic;
    public AudioSource epicMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        shark.canMove = false;
        canon.enabled = false;
        timer = roundDuration;
        ScoreManager.Instance.ResetScores();
        StartCoroutine(WaitAndStart());
      
    }

    IEnumerator WaitAndStart()
    {
        if (firstScreen.activeInHierarchy)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            firstScreen.transform.DOMoveY(-999f, 2f).OnComplete(() =>
            {
                VideoPlayer.Stop();
                
                firstScreen.SetActive(false);
            });
        }

        sharkCollider.enabled = true;
        vignette.SetFloat("_ApertureSize", 0f);
        vignette.DOFloat(1f,"_ApertureSize",3f);
        openAudio.Play();
        gameRunning = false;
        titleText.text = "Ready? \n (Press Spacebar)";
        titleText.transform.DOScale(1f, 1f).From(0f);
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        yield return new WaitForSeconds(2f);
        titleText.text = "3";
        titleText.transform.DOScale(1f,1f).From(0f);
        yield return new WaitForSeconds(.9f);
        titleText.text = "2";
        titleText.transform.DOScale(1f,1f).From(0f);
        yield return new WaitForSeconds(.9f);
        titleText.text = "1";
        titleText.transform.DOScale(1f,1f).From(0f);
        yield return new WaitForSeconds(.9f);
        titleText.transform.DOScale(0f,.5f);
        if (globalRound == 3)
        {
            defaultMusic.Pause();
            epicMusic.Play();
        }
        gameRunning = true;
        OnNewRoundStarted?.Invoke();
        fishSpawner.StartSpawning();
        shark.canMove = true;
        canon.enabled = true;
    }
    

    private void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        StartCoroutine(WaitAndEnd());
    }

    IEnumerator WaitAndEnd()
    {
        sharkCollider.enabled = false;
        vignette.SetFloat("_ApertureSize", 1f);
        vignette.DOFloat(0f,"_ApertureSize",3f);
        closeAudio.Play();
        fishSpawner.DeleteAllFish();
        shark.canMove = false;
        canon.enabled = false;
        titleText.text = "Time's Out! \n Points: " + ScoreManager.Instance.GetScore(GetCurrentRole()).ToString();
        titleText.transform.DOScale(1f,1f).From(0f);
        OnRoundEnded?.Invoke();
        gameRunning = false;
        ScoreManager.Instance.EvaluateRound(isSharkTurn);
        yield return new WaitForSeconds(3f);
        // Cambiar roles o finalizar el juego
        if (currentRound < roundsPerPlayer * 2)
        {
            
            yield return new WaitForSeconds(2f);
            titleText.text = "Change Controls!";
            titleText.transform.DOScale(1f,1f).From(0f);
            yield return new WaitForSeconds(2f);
            if (!isSharkTurn)
            {
                globalRound++;
            }

            
            isSharkTurn = !isSharkTurn;
            currentRound++;
            StartNextRound();
        }
        else
        {
            CheckWinner();
        }
    }

    private void StartNextRound()
    {
        timer = roundDuration;
        StartCoroutine(WaitAndStart());
        
    }

    private void CheckWinner()
    {
        int player1Score = ScoreManager.Instance.GetScore(1);
        int player2Score = ScoreManager.Instance.GetScore(2);

        if (player1Score > player2Score)
        {
            Debug.Log("¡Jugador 1 gana!");
            titleText.text = "Player 1 Wins! \n " + " Score:" + ScoreManager.Instance.GetScore(1);
        }
        else if (player2Score > player1Score)
        {
            Debug.Log("¡Jugador 2 gana!");
            titleText.text = "Player 2 Wins! \n" + " Score:" + ScoreManager.Instance.GetScore(2);;
        }
        else
        {
            Debug.Log("Empate. ¡Reiniciando el juego!");
            StartCoroutine(Draw());
        }
    }

    IEnumerator Draw()
    {
        titleText.text = "Draw!";
        titleText.transform.DOScale(1f,1f).From(0f);
        yield return new WaitForSeconds(2f);
        RestartGame();
    }

    private void RestartGame()
    {
        currentRound = 1;
        isSharkTurn = true;
        ScoreManager.Instance.ResetScores();
        StartNextRound();
    }

    public int GetCurrentRole()
    {
        return isSharkTurn ? 1 : 2;
    }
    
}