using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNote : MonoBehaviour
{
    // Start is called before the first frame update

    private Renderer render;

    [SerializeField] bool wasHit;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHit()
    {
        render.material.SetColor("_Color", Color.white);
        wasHit = true;
    }

    public void SetMissMaybe()
    {
        if(!wasHit)
            render.material.SetColor("_Color", Color.black);
    }
}
