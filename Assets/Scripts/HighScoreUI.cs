using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoresUI : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private GameObject highScoresPanel;

    [SerializeField] private GameObject menuPrincipal;

    [Header("Textos do Ranking")]
    [SerializeField] private TMP_Text[] textosScores;

    void Start()
    {
        highScoresPanel.SetActive(false);
    }

    public void AbrirHighScores()
    {
        AtualizarRanking();

        if (menuPrincipal != null)
            menuPrincipal.SetActive(false);

        highScoresPanel.SetActive(true);
    }

    public void FecharHighScores()
    {
        highScoresPanel.SetActive(false);

        if (menuPrincipal != null)
            menuPrincipal.SetActive(true);
    }

    void AtualizarRanking()
    {
        List<int> scores = HighScoreManager.ObterScores();

        for (int i = 0; i < textosScores.Length; i++)
        {
            if (textosScores[i] == null)
                continue;

            if (i < scores.Count)
            {
                textosScores[i].text =
                    $"{i + 1}.  {scores[i]:00000}";
            }
            else
            {
                textosScores[i].text =
                    $"{i + 1}.  -----";
            }
        }
    }
}