
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int dano = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AplicarDano(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        AplicarDano(other);
    }

    private void AplicarDano(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.ReceberDano(dano);
        }
    }
}
