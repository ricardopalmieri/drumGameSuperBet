using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StraightMovement : MonoBehaviour
{
    public Vector2 endPoint; // Ponto final da animação
    public float duration = 1f; // Duração da animação
    public float delayBeforeStart = 0f; // Tempo de espera antes de iniciar a animação

    private RectTransform rectTransform;
    private Vector2 startPoint;
    private bool isMoving = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPoint = rectTransform.anchoredPosition; // Obtém a posição inicial do objeto
        ResetPosition();
        StartCoroutine(StartDelay());
    }

    void ResetPosition()
    {
        // Define a posição inicial do objeto
        rectTransform.anchoredPosition = startPoint;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        StartMovement();
    }

    public void StartMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        float timer = 0f;
        Vector2 startPosition = startPoint;
        Vector2 endPosition = endPoint;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float easedT = EaseInOutQuad(t);
            Vector2 newPosition = Vector2.Lerp(startPosition, endPosition, easedT);
            rectTransform.anchoredPosition = newPosition;
            yield return null;
        }

        // Garante que a posição final seja exatamente igual ao ponto final
        rectTransform.anchoredPosition = endPosition;

        isMoving = false;
    }

    float EaseInOutQuad(float t)
    {
        return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
    }
}
