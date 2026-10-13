using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [Header("Vidas")]
    [SerializeField] private Image[] imagensVida;

    [Header("Textos")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    public Image[] ImagensVida => imagensVida;
    public TMP_Text TimerText => timerText;
    public TMP_Text ScoreText => scoreText;
}
