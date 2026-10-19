using UnityEngine;
using System.Collections.Generic;

public static class HighScoreManager
{
    private const int limiteScores = 5;
    private const string chaveScores = "Got2Survive_HighScores";

    [System.Serializable]
    private class ScoreData
    {
        public List<int> scores = new List<int>();
    }

    public static List<int> ObterScores()
    {
        string json = PlayerPrefs.GetString(chaveScores, "");

        if (string.IsNullOrEmpty(json))
            return new List<int>();

        ScoreData dados = JsonUtility.FromJson<ScoreData>(json);

        if (dados == null || dados.scores == null)
            return new List<int>();

        List<int> scores = new List<int>(dados.scores);

        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > limiteScores)
            scores.RemoveRange(limiteScores, scores.Count - limiteScores);

        return scores;
    }

    public static bool SalvarScore(int pontuacao)
    {
        if (pontuacao < 0)
            return false;

        List<int> scores = ObterScores();

        scores.Add(pontuacao);
        scores.Sort((a, b) => b.CompareTo(a));

        bool entrouNoRanking =
            scores.IndexOf(pontuacao) < limiteScores;

        if (scores.Count > limiteScores)
            scores.RemoveRange(limiteScores, scores.Count - limiteScores);

        ScoreData dados = new ScoreData();
        dados.scores = scores;

        string json = JsonUtility.ToJson(dados);

        PlayerPrefs.SetString(chaveScores, json);
        PlayerPrefs.Save();

        return entrouNoRanking;
    }

    public static int ObterMelhorScore()
    {
        List<int> scores = ObterScores();

        if (scores.Count == 0)
            return 0;

        return scores[0];
    }
}