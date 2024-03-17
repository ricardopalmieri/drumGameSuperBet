using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNote : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers = new List<Renderer>();
    [SerializeField] bool wasHit;

    public void Setup(GameNote note)
    {
        note.OnHit += SetHit;
        note.OnMiss += SetMiss;
    }

    private void OnDisable()
    {

    }

    void Start()
    {
        if (renderers.Count == 0)
        {
            // Procurar os Renderers nos filhos (modelos OBJ)
            Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
            renderers.AddRange(childRenderers);
        }
    }

    public void SetHit()
    {
        //render.material.SetColor("_Color", Color.clear);
        FadeOutNote();
    }

    public void SetMiss()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor("_Color", Color.black);
        }
        StartCoroutine(KillNote());
    }


    void FadeOutNote()
    {
        transform.parent = null;
        StartCoroutine(FadeOut());

    }

    IEnumerator FadeOut()
    {
        float duration = 0.5f;
        float growAmount = 0.3f;

        Vector3 endScale = transform.localScale * (1f + growAmount);

        List<Color> originalColors = new List<Color>();

        // Salvar as cores originais dos materiais dos Renderers
        foreach (Renderer renderer in renderers)
        {
            originalColors.Add(renderer.material.GetColor("_Color"));
        }

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            for (int j = 0; j < renderers.Count; j++)
            {
                Renderer renderer = renderers[j];
                Color originalColor = originalColors[j];

                var color = renderer.material.GetColor("_Color");
                renderer.material.SetColor("_Color", Color.Lerp(originalColor, Color.clear, i / duration));
            }

            transform.localScale = Vector3.Lerp(transform.localScale, endScale, i / duration);

            yield return null;
        }

        // Restaurar as cores originais dos materiais dos Renderers
        for (int j = 0; j < renderers.Count; j++)
        {
            Renderer renderer = renderers[j];
            Color originalColor = originalColors[j];
            renderer.material.SetColor("_Color", originalColor);
        }

        Destroy(gameObject);
    }


    IEnumerator KillNote()
    {

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }



}
