using UnityEngine;
using UnityEngine.InputSystem;

public class TesteScript : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 movimento;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movimentoX = 0f;
        float movimentoY = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            movimentoX = -1f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            movimentoX = 1f;

        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            movimentoY = -1f;
        else if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            movimentoY = 1f;

        movimento = new Vector2(movimentoX, movimentoY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movimento * velocidade;
    }
}