using UnityEngine;

public class EllipticalMovement : MonoBehaviour
{
    public float radius = 2.0f; // Raio da elipse
    public float speed = 1.0f; // Velocidade de movimento
    public float noiseStrength = 0.1f; // Força do ruído
    public float noiseFrequency = 1.0f; // Frequência do ruído

    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calcula a posição na elipse com base no tempo
        float time = Time.time * speed;
        float x = originalPosition.x + radius * Mathf.Cos(time);
        float y = originalPosition.y + radius * Mathf.Sin(time);

        // Aplica ruído suave à posição
        float noiseX = Mathf.PerlinNoise(Time.time * noiseFrequency, 0) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0, Time.time * noiseFrequency) - 0.5f;
        x += noiseX * noiseStrength;
        y += noiseY * noiseStrength;

        // Define a nova posição
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
