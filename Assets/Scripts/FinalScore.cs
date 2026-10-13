using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [Header("HUD - Pontuação Final")]
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        int pontuacao = GameManager.PontuacaoFinal;

        scoreText.text = $"{pontuacao:00000}";
    }
}
