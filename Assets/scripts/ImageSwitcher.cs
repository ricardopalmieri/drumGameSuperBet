using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{

    public GameObject inputM;
    private InputKeyboard inputK;
    public Image cursorImage1; // Primeira imagem de cursor
    public Image cursorImage2; // Segunda imagem de cursor
    public Sprite originalSprite; // Sprite original
    public Sprite targetSprite; // Sprite de destino
    public GameObject prefabToInstantiate; // Prefab a ser instanciado

    public bool isOverlapping = false; // Flag para indicar se há sobreposição
    public bool perfecto = false; // Flag para indicar se o cursor está no modo perfeito

    private Image image; // Componente de Image da imagem a ser alterada

    [Space]

    public UnityEvent OnOverlap;

    private void Start()
    {
        image = GetComponent<Image>();
        // Começa com a imagem original
        image.sprite = originalSprite;
        inputK = inputM.GetComponent<InputKeyboard>();
    }

    private void Update()
    {
        
        // Verifica se uma das imagens de cursor se sobrepõe à imagem original
        if (IsCursorOverImage(cursorImage1) || IsCursorOverImage(cursorImage2))
        {
            // Se não estava sobreposta anteriormente
            if (!isOverlapping)
            {

                isOverlapping = true;
                //  Debug.Log("Cursor sobre a imagem");
                // Troca para a sprite de destino
                image.sprite = targetSprite;

                // Execute event enter
                OnOverlap.Invoke();
            }
        }
        else
        {
            // Se não está sobreposta
            if (isOverlapping)
            {
                isOverlapping = false;
                //  Debug.Log("Cursor fora da imagem");
                // Volta para a sprite original
                image.sprite = originalSprite;
            }
        }

        if (perfecto)
        {
            // Instancia o prefab como filho do GameObject que possui o script
            GameObject newPrefab = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            newPrefab.transform.SetParent(transform);
            Debug.Log("perfect");
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
