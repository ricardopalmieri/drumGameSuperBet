using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject prefab;

    public Transform noteParent;

    public MusicGamplay musicGamplay;

    public NotesGroup notesGroup;

    [Header("Settings")]
    public bool recolorNote = false;

    Vector3 startPos; // Not in use anymer


    ///
    float noteSpace = 2;


    private void OnEnable()
    {
        notesGroup.OnNotesReady += Prepare;
    }

    private void OnDisable()
    {
        notesGroup.OnNotesReady -= Prepare;
    }

    private void Awake()
    {
        // Disable visual sphere
        GetComponent<Renderer>().enabled = false;
    }

    void Prepare()
    {
        noteSpace = musicGamplay.speed;

        startPos = transform.position;

        SpawnAllNotes();
    }

    [ContextMenu("Spawn notes")]
    public void SpawnAllNotes()
    {

        foreach (var note in notesGroup.notes)
        {
            var color = GetColorByNumber(note.note.midi);

            //Vector3 pos = new Vector3((note.note.midi - 60) * 1, 0, note.note.time * noteSpace) + plusPosition;
            Vector3 pos = startPos + Vector3.forward * note.note.time * noteSpace;

            var obj = Instantiate(prefab, pos, Quaternion.identity, noteParent);

            if(recolorNote) obj.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);    // change this

            obj.GetComponent<VisualNote>()?.Setup(note);
        }

        //Debug.Log("Notes Spawned");
    }

    Color GetColorByNumber(int number)
    {
        switch (number)
        {
            case 60:
                return Color.red;
            case 62:
                return new Color(0,0.5f,0);
            case 64:
                return Color.yellow;
            case 65:
                return Color.blue;


            default:
                return Color.white;
        }
    }

    void OnDrawGizmosSelectedOld()
    {
        Gizmos.color = Color.yellow;
        float multi = 0.2f;

        Gizmos.DrawLine(startPos - Vector3.forward * multi, startPos + Vector3.forward * multi);

        Gizmos.DrawLine(startPos - Vector3.up * multi, startPos + Vector3.up * multi);

        Gizmos.DrawLine(startPos - Vector3.right * multi, startPos + Vector3.right * multi);
    }
}
