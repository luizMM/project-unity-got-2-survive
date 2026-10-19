using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Projétil")]
    [SerializeField] private float velocidade = 3.5f;
    [SerializeField] private float tempoVida = 6f;
    [SerializeField] private int dano = 1;

    [Header("Efeitos")]
    [SerializeField] private GameObject efeitoImpacto;

    private Rigidbody2D rb;
    private Vector2 direcao;

    private bool destruindo = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, tempoVida);
    }

    public void Inicializar(Vector2 direcaoDisparo)
    {
        direcao = direcaoDisparo.normalized;
    }

    void FixedUpdate()
    {
        if (destruindo)
            return;

        rb.MovePosition(
            rb.position + direcao * velocidade * Time.fixedDeltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (destruindo)
            return;

        // Ignora outros projéteis e inimigos
        if (other.GetComponentInParent<Bullet>() != null ||
            other.GetComponentInParent<EnemyDamage>() != null)
        {
            return;
        }

        // Colisão com o Player
        PlayerHealth player =
            other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.ReceberDano(dano);

            DestruirProjetil(false);
            return;
        }

        // Colisão com paredes
        if (other.CompareTag("Wall"))
        {
            DestruirProjetil(true);
        }
    }

    void DestruirProjetil(bool criarImpacto)
    {
        if (destruindo)
            return;

        destruindo = true;

        if (criarImpacto && efeitoImpacto != null)
        {
            Instantiate(
                efeitoImpacto,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}