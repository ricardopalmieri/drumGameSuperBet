using UnityEngine;
using UnityEngine.UI;

public class animaHits : MonoBehaviour
{
    public float movementSpeed = 50000f; // Velocidade de movimenta��o
    public float fadeOutTime = 1f; // Tempo de desaparecimento
    public float distanceToMove = 8000f; // Dist�ncia a se mover para cima

    private float initialYPosition; // Posi��o Y inicial
    private float startTime; // Tempo de in�cio do movimento
    private Image image; // Refer�ncia � componente Image
    private RectTransform rectTransform; // Refer�ncia � componente RectTransform

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        initialYPosition = rectTransform.anchoredPosition.y;
        startTime = Time.time; // Armazenar o tempo de in�cio do movimento
    }

    private void Update()
    {
        // Calcular a dist�ncia percorrida
        float distanceCovered = (Time.time - startTime) * movementSpeed;
        // Calcular a propor��o da dist�ncia percorrida em rela��o � dist�ncia total
        float fractionOfDistance = distanceCovered / distanceToMove;
        // Atualizar a posi��o Y do objeto
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initialYPosition + fractionOfDistance);

        // Calcular a opacidade com base no tempo
        float fadePercentage = Mathf.Clamp01((Time.time - startTime) / fadeOutTime);
        // Aplicar a opacidade � imagem
        image.color = new Color(1f, 1f, 1f, 1f - fadePercentage);

        // Verificar se o tempo de fade foi conclu�do
        if (fadePercentage >= 1f)
        {
            Destroy(gameObject); // Destruir o objeto
        }
    }
}
