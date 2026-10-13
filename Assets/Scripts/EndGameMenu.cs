using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] private int cenaLevel1 = 1;
    [SerializeField] private int cenaMenu = 0;

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(cenaLevel1);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(cenaMenu);
    }
}
