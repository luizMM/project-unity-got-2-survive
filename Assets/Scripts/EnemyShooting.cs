
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public enum TipoDisparo
    {
        Simples,
        DuploLeque,
        QuatroDirecoes
    }

    [Header("Referências")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform pontoDisparo;

    [Header("Disparo")]
    [SerializeField] private TipoDisparo tipoDisparo = TipoDisparo.Simples;
    [SerializeField] private float intervaloDisparo = 2f;
    [SerializeField] private float variacaoIntervalo = 0.5f;

    [Header("Disparo em Leque")]
    [SerializeField, Range(0f, 90f)]
    private float anguloDispersao = 25f;

    private float temporizador;

    [Header("Disparo Circular")]
    [SerializeField] private float rotacaoPorDisparo = 30f;

    private float anguloAtual = 0f;

    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somDisparo;

    void Start()
    {
        temporizador = Random.Range(
            0f,
            Mathf.Max(0.1f, intervaloDisparo)
        );
    }

    void Update()
    {
        if (bulletPrefab == null)
            return;

        // Os disparos Simples e DuploLeque precisam do Player
        if (tipoDisparo != TipoDisparo.QuatroDirecoes && player == null)
            return;

        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            Atirar();

            temporizador = Mathf.Max(
                0.1f,
                intervaloDisparo +
                Random.Range(
                    -variacaoIntervalo,
                    variacaoIntervalo
                )
            );
        }
    }

    void Atirar()
    {
        Vector2 origem = pontoDisparo != null
            ? pontoDisparo.position
            : transform.position;

        if (audioSource != null && somDisparo != null)
        {
            audioSource.PlayOneShot(somDisparo);
        }

        if (tipoDisparo == TipoDisparo.QuatroDirecoes)
        {
            for (int i = 0; i < 4; i++)
            {
                float angulo = anguloAtual + (i * 90f);

                float radianos = angulo * Mathf.Deg2Rad;

                Vector2 direcaoCircular = new Vector2(
                    Mathf.Cos(radianos),
                    Mathf.Sin(radianos)
                );

                CriarProjetil(origem, direcaoCircular);
            }

            anguloAtual = (anguloAtual + rotacaoPorDisparo) % 360f;

            return;
        }

        Vector2 destino = player.position;
        Vector2 direcao = (destino - origem).normalized;

        if (tipoDisparo == TipoDisparo.Simples)
        {
            CriarProjetil(origem, direcao);
        }
        else if (tipoDisparo == TipoDisparo.DuploLeque)
        {
            Vector2 direcaoEsquerda =
                RotacionarDirecao(direcao, -anguloDispersao);

            Vector2 direcaoDireita =
                RotacionarDirecao(direcao, anguloDispersao);

            CriarProjetil(origem, direcaoEsquerda);
            CriarProjetil(origem, direcaoDireita);
        }
    }

    Vector2 RotacionarDirecao(Vector2 direcao, float angulo)
    {
        float radianos = angulo * Mathf.Deg2Rad;

        float cos = Mathf.Cos(radianos);
        float sin = Mathf.Sin(radianos);

        return new Vector2(
            direcao.x * cos - direcao.y * sin,
            direcao.x * sin + direcao.y * cos
        ).normalized;
    }

    void CriarProjetil(Vector2 origem, Vector2 direcao)
    {
        GameObject novoProjetil = Instantiate(
            bulletPrefab,
            origem,
            Quaternion.identity
        );

        Bullet bullet = novoProjetil.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Inicializar(direcao);
        }
    }

    public void DefinirPlayer(Transform novoPlayer)
    {
        player = novoPlayer;
    }
}
