using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btController : MonoBehaviour
{

    private Image theSR;
    public GameObject botao;
    private ImageSwitcher ImSw;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public KeyCode keyToPress;

    public bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<Image>();
        ImSw = botao.GetComponent<ImageSwitcher>();
        isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress) || ImSw.isOverlapping)
        {
            theSR.sprite = pressedImage;
            isOver = true;
        }
        if (Input.GetKeyUp(keyToPress) || !ImSw.isOverlapping)
        {
            theSR.sprite = defaultImage;
            isOver = false;
        }
    }
}
