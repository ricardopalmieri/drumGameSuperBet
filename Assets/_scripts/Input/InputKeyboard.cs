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

 



    private void Update()
    {

        //coloquei aqui qdo tenho a colisão na UI
        if (Input.GetKeyDown(key1) )
        {
            OnHitNote1();
        }

        else if (Input.GetKeyDown(key2))
        {
            OnHitNote2();
        }
        else if (Input.GetKeyDown(key3) )
        {
            OnHitNote3();
        }
        else if (Input.GetKeyDown(key4) )
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
