
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject prefab;

        [Min(0)]
        public int peso = 1;
    }

    [Header("Inimigos disponíveis")]
    [SerializeField] private EnemySpawnData[] inimigos;

    [Header("Configurações de Spawn")]
    [SerializeField] private int limiteInimigos = 4;
    [SerializeField] private float intervaloSpawn = 5f;
    [SerializeField] private int inimigosIniciais = 2;

    [Header("Área de Spawn")]
    [SerializeField] private Vector2 tamanhoArea = new Vector2(12f, 7f);
    [SerializeField] private float distanciaMinPlayer = 3f;
    [SerializeField] private float raioVerificacao = 0.5f;
    [SerializeField] private LayerMask camadasBloqueadas;

    [Header("Referências")]
    [SerializeField] private Transform player;

    private List<GameObject> inimigosAtivos = new List<GameObject>();
    private float temporizador;

    void Start()
    {
        if (player == null)
        {
            PlayerHealth health =
                FindFirstObjectByType<PlayerHealth>();

            if (health != null)
                player = health.transform;
        }

        for (int i = 0; i < inimigosIniciais; i++)
        {
            SpawnarInimigo();
        }

        temporizador = intervaloSpawn;
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            SpawnarInimigo();
            temporizador = intervaloSpawn;
        }
    }

    void SpawnarInimigo()
    {
        // Remove referências a inimigos destruídos
        inimigosAtivos.RemoveAll(inimigo => inimigo == null);

        if (inimigosAtivos.Count >= limiteInimigos)
            return;

        GameObject prefab = EscolherInimigo();

        if (prefab == null)
            return;

        Vector2 posicao;

        if (!EncontrarPosicaoValida(out posicao))
            return;

        GameObject novoInimigo = Instantiate(
            prefab,
            posicao,
            Quaternion.identity
        );

        inimigosAtivos.Add(novoInimigo);

        // Configura o Player para o disparo
        EnemyShooting shooting =
            novoInimigo.GetComponent<EnemyShooting>();

        if (shooting != null && player != null)
        {
            shooting.DefinirPlayer(player);
        }
    }

    GameObject EscolherInimigo()
    {
        int pesoTotal = 0;

        foreach (EnemySpawnData inimigo in inimigos)
        {
            if (inimigo.prefab != null)
                pesoTotal += Mathf.Max(0, inimigo.peso);
        }

        if (pesoTotal <= 0)
            return null;

        int sorteio = Random.Range(0, pesoTotal);

        foreach (EnemySpawnData inimigo in inimigos)
        {
            if (inimigo.prefab == null)
                continue;

            int peso = Mathf.Max(0, inimigo.peso);

            if (sorteio < peso)
                return inimigo.prefab;

            sorteio -= peso;
        }

        return null;
    }

    bool EncontrarPosicaoValida(out Vector2 posicao)
    {
        for (int tentativa = 0; tentativa < 30; tentativa++)
        {
            float x = Random.Range(
                -tamanhoArea.x / 2f,
                tamanhoArea.x / 2f
            );

            float y = Random.Range(
                -tamanhoArea.y / 2f,
                tamanhoArea.y / 2f
            );

            Vector2 ponto = (Vector2)transform.position +
                            new Vector2(x, y);

            // Evita spawn próximo do Player
            if (player != null &&
                Vector2.Distance(ponto, player.position)
                < distanciaMinPlayer)
            {
                continue;
            }

            // Evita paredes e outros objetos bloqueadores
            Collider2D colisao = Physics2D.OverlapCircle(
                ponto,
                raioVerificacao,
                camadasBloqueadas
            );

            if (colisao != null)
                continue;

            posicao = ponto;
            return true;
        }

        posicao = Vector2.zero;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(tamanhoArea.x, tamanhoArea.y, 0f)
        );
    }
}
