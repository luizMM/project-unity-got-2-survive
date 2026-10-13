
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float velocidade = 2f;

    [Header("Tempos")]
    [SerializeField] private float tempoMinMovimento = 1f;
    [SerializeField] private float tempoMaxMovimento = 3f;
    [SerializeField] private float tempoParado = 1f;

    private Rigidbody2D rb;
    private Vector2 direcao;
    private float temporizador;
    private bool movimentando = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        EscolherDirecao();
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            if (movimentando)
            {
                movimentando = false;
                temporizador = tempoParado;
            }
            else
            {
                EscolherDirecao();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movimentando
            ? direcao * velocidade
            : Vector2.zero;
    }

    void EscolherDirecao()
    {
        direcao = Random.insideUnitCircle.normalized;

        if (direcao == Vector2.zero)
            direcao = Vector2.right;

        temporizador = Random.Range(
            tempoMinMovimento,
            tempoMaxMovimento
        );

        movimentando = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Usa a normal da parede para escolher
            // uma direção que se afaste da colisão.
            Vector2 normal = collision.GetContact(0).normal;

            direcao = (
                normal + Random.insideUnitCircle * 0.5f
            ).normalized;

            temporizador = Random.Range(
                tempoMinMovimento,
                tempoMaxMovimento
            );

            movimentando = true;
        }
    }
}
