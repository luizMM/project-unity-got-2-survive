
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configurações das fases")]
    [SerializeField] private float tempoPorFase = 10f;

    [SerializeField] private int[] fases =
    {
        1,
        2,
        3,
        4
    };

    [Header("Pontuação")]
    [SerializeField] private float pontosPorSegundo = 10f;

    private float tempoRestante;
    private float pontuacao = 0f;

    private int faseAtual = 0;
    private int vidasAtuais = 3;

    private bool jogoAtivo = true;

    // Referências da HUD da cena atual
    private TMP_Text timerText;
    private TMP_Text scoreText;
    private Image[] imagensVida;

    private PlayerHealth player;

    public static int PontuacaoFinal { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        PontuacaoFinal = 0;

        Instance = this;
        DontDestroyOnLoad(gameObject);

        tempoRestante = tempoPorFase;
    }

    void Start()
    {
        Time.timeScale = 1f;

        SceneManager.sceneLoaded += AoCarregarCena;

        ConfigurarCena();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
            Instance = null;
        }
    }

    void Update()
    {
        if (!jogoAtivo)
            return;

        tempoRestante -= Time.deltaTime;
        pontuacao += pontosPorSegundo * Time.deltaTime;

        tempoRestante = Mathf.Max(0f, tempoRestante);

        AtualizarHUD();

        if (tempoRestante <= 0f)
        {
            ProximaFase();
        }
    }

    void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        ConfigurarCena();
    }

    void ConfigurarCena()
    {
        Time.timeScale = 1f;

        player = FindFirstObjectByType<PlayerHealth>();

        if (player != null)
        {
            player.DefinirVidas(vidasAtuais);
        }

        // Busca os elementos da HUD da nova cena
        GameHUD hud = FindFirstObjectByType<GameHUD>();

        if (hud != null)
        {
            timerText = hud.TimerText;
            scoreText = hud.ScoreText;
            imagensVida = hud.ImagensVida;

            AtualizarVidas(vidasAtuais);
            AtualizarHUD();
        }
        else
        {
            timerText = null;
            scoreText = null;
            imagensVida = null;

            Debug.LogWarning("GameHUD não encontrado na cena.");
        }
    }

    void AtualizarHUD()
    {
        int segundosTotais = Mathf.CeilToInt(tempoRestante);

        int minutos = segundosTotais / 60;
        int segundos = segundosTotais % 60;

        if (timerText != null)
            timerText.text = $"{minutos:00}:{segundos:00}";

        if (scoreText != null)
            scoreText.text =
                $"{Mathf.FloorToInt(pontuacao):00000}";
    }

    public void AtualizarVidas(int vidas)
    {
        vidasAtuais = Mathf.Max(0, vidas);

        if (imagensVida == null)
            return;

        for (int i = 0; i < imagensVida.Length; i++)
        {
            if (imagensVida[i] != null)
                imagensVida[i].enabled = i < vidasAtuais;
        }
    }

    void ProximaFase()
    {
        jogoAtivo = false;

        faseAtual++;

        if (faseAtual >= fases.Length)
        {
            Vitoria();
            return;
        }

        tempoRestante = tempoPorFase;

        SceneManager.LoadScene(fases[faseAtual]);

        jogoAtivo = true;
    }

    
    
    public void GameOver()
    {
        if (!jogoAtivo)
            return;

        jogoAtivo = false;

        // Salva a pontuação final
        PontuacaoFinal = Mathf.FloorToInt(pontuacao);

        Debug.Log("GAME OVER! Score: " + PontuacaoFinal);

        SceneManager.sceneLoaded -= AoCarregarCena;

        Instance = null;
        Destroy(gameObject);

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }


    
    
    void Vitoria()
    {
        // Salva a pontuação final
        PontuacaoFinal = Mathf.FloorToInt(pontuacao);

        Debug.Log("VITÓRIA! Score: " + PontuacaoFinal);

        SceneManager.sceneLoaded -= AoCarregarCena;

        Instance = null;
        Destroy(gameObject);

        Time.timeScale = 1f;
        SceneManager.LoadScene("EndGame");
    }


}
