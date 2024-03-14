using UnityEngine;

public class TextureMovement : MonoBehaviour
{
    public GameObject referenceObject; // Referência ao GameObject cujo deslocamento controlará a velocidade da textura
    public float speedMultiplier = 1.0f; // Multiplicador de velocidade

    private Vector3 lastPosition;
    private Material mat;

    [SerializeField] private float offset = 0;

    void Start()
    {
        // Salva a posição inicial do objeto de referência
        lastPosition = referenceObject.transform.position;

        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Calcula a diferença de posição do objeto de referência desde o último frame
        Vector3 displacement = referenceObject.transform.position - lastPosition;

        // Calcula a velocidade da textura com base na componente Z do deslocamento do objeto de referência
        float speed = displacement.z * speedMultiplier;// / Time.deltaTime;

        // Aplica o deslocamento à textura do material
        //float offset = (Time.time * speed)%1;
        offset += speed;
        mat.mainTextureOffset = new Vector2(0, offset);

        // Atualiza a posição do objeto de referência para o próximo frame
        lastPosition = referenceObject.transform.position;
    }
}
