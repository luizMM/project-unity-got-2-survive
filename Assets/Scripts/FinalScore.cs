using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [Header("Pontuação Final")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Melhor Pontuação")]
    [SerializeField] private TMP_Text bestScoreText;

    [Header("Novo Recorde")]
    [SerializeField] private GameObject newRecordText;

    void Start()
    {
        AtualizarPontuacao();
    }

    void AtualizarPontuacao()
    {
        // Pontuação da partida atual
        int pontuacao = GameManager.PontuacaoFinal;

        if (scoreText != null)
        {
            scoreText.text = $"{pontuacao:00000}";
        }

        // Melhor pontuação registrada
        if (bestScoreText != null)
        {
            int melhorScore = HighScoreManager.ObterMelhorScore();

            bestScoreText.text = $"{melhorScore:00000}";
        }

        // Exibe NEW apenas quando houver novo recorde
        if (newRecordText != null)
        {
            newRecordText.SetActive(GameManager.NovoRecorde);
        }
    }
}