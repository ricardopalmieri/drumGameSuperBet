using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNote : MonoBehaviour
{
    [SerializeField] private Renderer render;
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
        if(render == null)
            render = GetComponent<Renderer>();
    }

    public void SetHit()
    {
        //render.material.SetColor("_Color", Color.clear);
        FadeOutNote();
    }

    public void SetMiss()
    {
        render.material.SetColor("_Color", Color.black);
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


        Vector3 endScale = transform.localScale  * (1f+growAmount);

        for (float i = 0; i < duration; i+= Time.deltaTime)
        {
            var color = render.material.GetColor("_Color");

            render.material.SetColor("_Color", Color.Lerp(color, Color.clear, i/ duration));

            transform.localScale = Vector3.Lerp(transform.localScale, endScale, i / duration);


            yield return null;
        }
        render.material.SetColor("_Color", Color.clear);

        Destroy(gameObject);
    }
}
