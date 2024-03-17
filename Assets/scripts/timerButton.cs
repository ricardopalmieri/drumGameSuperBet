using UnityEngine;
using UnityEngine.UI;

public class TimerTrigger : MonoBehaviour
{
    public GameObject gm;
    private GameManager gm1;
    public Image cursorImage1; // Primeira imagem de cursor
    public Image cursorImage2; // Segunda imagem de cursor
    public float overlapTimeThreshold = 2.0f; // Tempo necessário para considerar a sobreposição
    public float fadeDuration = 1f; // Duração do fade da imagem
    public float fadeDelay = 0.5f; // Atraso para iniciar o fade após a sobreposição

    private float overlapTimer = 0f; // Cronômetro para controlar o tempo de sobreposição
    private bool isOverlapping = false; // Flag para indicar se há sobreposição
    private Image image; // Referência à própria imagem

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 1f); // Começa completamente opaco
        gm1 = gm.GetComponent<GameManager>();
    }

    void OnAwake(){
        gameObject.SetActive(true);

    }

    private void Update()
    {
        // Verifica se o cursor de mão está sobre a imagem
        if (IsCursorOverImage(cursorImage1) || IsCursorOverImage(cursorImage2))
        {
            // Se não estava sobreposta anteriormente, inicia o cronômetro
            if (!isOverlapping)
            {
                isOverlapping = true;
                overlapTimer = 0f;
            }

            // Incrementa o cronômetro
            overlapTimer += Time.deltaTime;

            // Se o tempo de sobreposição for maior que o limite
            if (overlapTimer >= overlapTimeThreshold)
            {
                Debug.Log("Mude de fase");
                gm1.ChangeGameState(GameState.MidGame);
                //gm1.ChangeGameState(GameState.Gameplay);
                //gm1.StartPlay();
                gameObject.SetActive(false);
            }

            // Calcula a opacidade com base no tempo de sobreposição
            float alpha = 1f - Mathf.Clamp01(overlapTimer / fadeDuration); // Inverte o valor da opacidade
            // Aplica a opacidade à imagem
            image.color = new Color(1f, 1f, 1f, alpha);
        }
        else
        {
            // Se não está sobreposta, reseta o estado e o cronômetro
            isOverlapping = false;
            overlapTimer = 0f;
            // Reseta a opacidade da imagem
            image.color = new Color(1f, 1f, 1f, 1f); // Retorna à opacidade total
        }
    }

    private bool IsCursorOverImage(Image cursor)
    {
        // Verifica se o cursor de mão está sobre a imagem
        RectTransform cursorRectTransform = cursor.rectTransform;
        RectTransform imageRectTransform = GetComponent<RectTransform>();

        // Obtém os cantos dos retângulos
        Vector3[] cursorCorners = new Vector3[4];
        Vector3[] imageCorners = new Vector3[4];
        cursorRectTransform.GetWorldCorners(cursorCorners);
        imageRectTransform.GetWorldCorners(imageCorners);

        // Verifica se há sobreposição em x e y
        for (int i = 0; i < 4; i++)
        {
            if (cursorCorners[i].x > imageCorners[0].x && cursorCorners[i].x < imageCorners[2].x &&
                cursorCorners[i].y > imageCorners[0].y && cursorCorners[i].y < imageCorners[2].y)
            {
                return true;
            }
        }

        return false;
    }
}
