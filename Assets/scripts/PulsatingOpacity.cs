using UnityEngine;
using TMPro;

public class PulsatingOpacity : MonoBehaviour
{
    public float pulseSpeed = 1.0f; // Velocidade do pulso
    public float minOpacity = 0.2f; // Opacidade mínima
    public float maxOpacity = 1.0f; // Opacidade máxima

    private TextMeshProUGUI textMesh;
    private float currentOpacity = 1.0f;
    private bool increasingOpacity = false;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Altera a opacidade com base no tempo
        currentOpacity += (increasingOpacity ? 1 : -1) * pulseSpeed * Time.deltaTime;

        // Garante que a opacidade permaneça dentro do intervalo desejado
        currentOpacity = Mathf.Clamp(currentOpacity, minOpacity, maxOpacity);

        // Atualiza a opacidade do TextMeshProUGUI
        textMesh.alpha = currentOpacity;

        // Inverte a direção do pulso quando atinge os limites
        if (currentOpacity <= minOpacity || currentOpacity >= maxOpacity)
        {
            increasingOpacity = !increasingOpacity;
        }
    }
}