using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour, IInputReceiver
{
    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;
    public KeyCode key4;

    [Space]
    public MusicGamplay musicGamplay;

    public GameObject btA;
    public GameObject btB;
    public GameObject btC;
    public GameObject btD;
    private ImageSwitcher bt1;
    private ImageSwitcher bt2;
    private ImageSwitcher bt3;
    private ImageSwitcher bt4;

    private void Start()
    {

        bt1 = btA.GetComponent<ImageSwitcher>();
        bt2 = btB.GetComponent<ImageSwitcher>();
        bt3 = btC.GetComponent<ImageSwitcher>();
        bt4 = btD.GetComponent<ImageSwitcher>();
    }

    private void Update()
    {

        //coloquei aqui qdo tenho a colisão na UI
        if (Input.GetKeyDown(key1) || bt1.isOverlapping)
        {
            OnHitNote1();
        }

        else if (Input.GetKeyDown(key2) || bt2.isOverlapping)
        {
            OnHitNote2();
        }
        else if (Input.GetKeyDown(key3) || bt3.isOverlapping)
        {
            OnHitNote3();
        }
        else if (Input.GetKeyDown(key4) || bt4.isOverlapping)
        {
            OnHitNote4();
        }
    }

    public void OnHitNote1()
    {
        musicGamplay?.OnInputNote1();
    }

    public void OnHitNote2()
    {
        musicGamplay?.OnInputNote2();
    }

    public void OnHitNote3()
    {
        musicGamplay?.OnInputNote3();
    }

    public void OnHitNote4()
    {
        musicGamplay?.OnInputNote4();
    }
}
