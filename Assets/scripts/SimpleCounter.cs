using UnityEngine;
using TMPro;

public class SimpleCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int count = 100;
    private float timer = 0f;
    public float interval = .001f; // Intervalo em segundos

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            count++;;
            counterText.text = FormatNumberWithThousands(count);
            timer = 0f;
        }
    }

        string FormatNumberWithThousands(int number)
    {
        return number.ToString("N0");
    }
}