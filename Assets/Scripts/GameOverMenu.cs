
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] private int cenaLevel1 = 1;
    [SerializeField] private int cenaMenu = 0;

    public void ReiniciarJogo()
    {
        // Garante que o jogo não esteja pausado
        Time.timeScale = 1f;

        // Reinicia desde o Level 1
        SceneManager.LoadScene(cenaLevel1);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;

        // Retorna ao menu inicial
        SceneManager.LoadScene(cenaMenu);
    }
}
