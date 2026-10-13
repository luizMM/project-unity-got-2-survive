
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 3;

    [Header("Invulnerabilidade")]
    [SerializeField] private float tempoInvulnerabilidade = 1.5f;

    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somDano;

    private int vidaAtual;
    private bool invulneravel = false;

    private SpriteRenderer spriteRenderer;

    public int VidaAtual => vidaAtual;

    void Awake()
    {
        vidaAtual = vidaMaxima;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void DefinirVidas(int vidas)
    {
        vidaAtual = Mathf.Clamp(vidas, 0, vidaMaxima);
    }


    public void ReceberDano(int dano)
    {
        if (vidaAtual <= 0 || invulneravel || dano <= 0)
            return;

        vidaAtual = Mathf.Max(vidaAtual - dano, 0);

        if (audioSource != null && somDano != null)
        {
            audioSource.PlayOneShot(somDano);
        }

        // Atualiza a HUD
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AtualizarVidas(vidaAtual);
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
        else
        {
            StartCoroutine(Invulnerabilidade());
        }
    }

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;

        float tempo = 0f;

        while (tempo < tempoInvulnerabilidade)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(0.15f);
            tempo += 0.15f;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        invulneravel = false;
    }

    void Morrer()
    {
        Debug.Log("Player morreu!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        gameObject.SetActive(false);
    }
}
