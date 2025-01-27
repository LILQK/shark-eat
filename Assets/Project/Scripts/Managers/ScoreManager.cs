using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private int player1Score = 0; // Puntos del jugador 1
    [SerializeField]
    private int player2Score = 0; // Puntos del jugador 2

    private ScoreUI scoreUI;
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
        
        scoreUI = GetComponent<ScoreUI>();
    }

    public void AddScore(int player, int points)
    {
        if (player == 1)
        {
            player1Score += points;
        }
        else if (player == 2)
        {
            player2Score += points;
        }
        scoreUI.AddScore(1);
    }

    public int GetScore(int player)
    {
        return player == 1 ? player1Score : player2Score;
    }

    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public void EvaluateRound(bool isSharkTurn)
    {
        if (isSharkTurn)
        {
            Debug.Log("El (Jugador 1) ha terminado su turno.");
        }
        else
        {
            Debug.Log("El (Jugador 2) ha terminado su turno.");
        }
    }

    public void RemoveScore(int getCurrentRole, int i)
    {
        if (getCurrentRole == 1)
        {
            player1Score -= i;
        }
        else if (getCurrentRole == 2)
        {
            player2Score -= i;
        }
        scoreUI.RemoveScore(i);
    }
}