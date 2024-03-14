using UnityEngine;

public class JumpingAnimation : MonoBehaviour
{
    public float jumpHeight = 1.0f; // Altura do pulo
    public float jumpSpeed = 1.0f; // Velocidade do pulo
    public float noiseStrength = 0.1f; // Força do ruído
    public float noiseFrequency = 1.0f; // Frequência do ruído

    private Vector3 originalScale;
    private float startY;

    void Start()
    {
        originalScale = transform.localScale;
        startY = transform.position.y;
    }

    void Update()
    {
        // Calcula a escala atual com base na posição Y
        float currentScale = 1 + Mathf.PingPong(Time.time * jumpSpeed, jumpHeight);

        // Aplica ruído suave à escala
        float noise = Mathf.PerlinNoise(Time.time * noiseFrequency, 0) - 0.5f;
        currentScale += noise * noiseStrength;

        // Define a nova escala
        transform.localScale = new Vector3(originalScale.x * currentScale, originalScale.y * currentScale, originalScale.z);

        // Movimenta o objeto no eixo Y para simular um pulo
        Vector3 newPosition = transform.position;
        newPosition.y = startY + Mathf.Sin(Time.time * jumpSpeed) * jumpHeight;
        transform.position = newPosition;
    }
}
