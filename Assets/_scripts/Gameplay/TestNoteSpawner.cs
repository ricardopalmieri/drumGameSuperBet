using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoteSpawner : MonoBehaviour
{
    public GameObject prefab;

    public Transform noteParent;

    public MusicGamplay musicGamplay;

    private SO_SongMidi song;

    float noteSpace = 2;

    public Vector3 plusPosition;

    [Space]
    public List<TempNote> notes;

    void Start()
    {
        song = musicGamplay.songMidi;
        noteSpace = musicGamplay.speed;


        SpawnAllNotes();
    }

    [ContextMenu("Spawn notes")]
    public void SpawnAllNotes()
    {
        notes = new List<TempNote>();

        foreach (var note in song.notes)
        {
            var color = GetColorByNumber(note.midi);
            Vector3 pos = new Vector3((note.midi - 60)*1, 0, note.time* noteSpace) + plusPosition;

            var obj = Instantiate(prefab, pos, Quaternion.identity, noteParent);

            obj.GetComponent<Renderer>().material.SetColor("_Color", color);

            notes.Add(obj.GetComponent<TempNote>());
        }
    }

    Color GetColorByNumber(int number)
    {
        switch (number)
        {
            case 60:
                return Color.red;
            case 62:
                return Color.green;
            case 64:
                return Color.yellow;
            case 65:
                return Color.blue;


            default:
                return Color.white;
        }
    }
}
