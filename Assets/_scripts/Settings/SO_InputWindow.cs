using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_InputWindow : ScriptableObject
{
    public float inputWindowBefore = 0.5f;
    public float inputWindowAfter = 0.5f;
    [Space]
    public float inputWindowBeforePerfect = 0.05f;
    public float inputWindowAfterPerfect = 0.05f;
}
