
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Projétil")]
    [SerializeField] private float velocidade = 3.5f;
    [SerializeField] private float tempoVida = 6f;
    [SerializeField] private int dano = 1;

    private Rigidbody2D rb;
    private Vector2 direcao;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Evita que o projétil fique na cena para sempre
        Destroy(gameObject, tempoVida);
    }

    // Chamado pelo inimigo quando o projétil é criado
    public void Inicializar(Vector2 direcaoDisparo)
    {
        direcao = direcaoDisparo.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + direcao * velocidade * Time.fixedDeltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignora outros projéteis e inimigos
        if (other.GetComponent<Bullet>() != null ||
            other.GetComponentInParent<EnemyDamage>() != null)
            return;

        // Verifica se atingiu o Player
        PlayerHealth player =
            other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.ReceberDano(dano);
            Destroy(gameObject);
            return;
        }

        // Destrói ao atingir parede
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
