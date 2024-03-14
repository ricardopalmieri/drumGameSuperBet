using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_SongMidi : ScriptableObject
{
    public AudioClip audioClip;

    public Note[] notes;

    [Space]

    public TextAsset jsonFile;

    [TextArea(3,3)]
    string json;

    MidiHolder midiHolder;

    void ConvertFromJsonOld()
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    [ContextMenu("Convert Json")]
    void ConvertFromJson()
    {
        midiHolder = new MidiHolder();

        if (jsonFile!= null)
        {
            JsonUtility.FromJsonOverwrite(jsonFile.text, midiHolder);
        }
        else
        {
            JsonUtility.FromJsonOverwrite(json, midiHolder);
        }
        notes = midiHolder.GetNotes();
    }


    void NameNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].name = "Note " +(i+1).ToString();
        }
    }

}

[System.Serializable]
public class Track
{
    //"startTime":6.764286841666666,
    //"duration":72.10715487500008,
    //"length":84,

    public float startTime;
    public float duration;
    public int lenght;

    public Note[] notes;


}


[System.Serializable]
public class Note
{
    //"name":"C4",
    //"midi":60,
    //"time":6.764286841666666,
    //"velocity":1,
    //"duration":0.09285715833333352

    public string name;
    public int midi;
    public float time;

}

[System.Serializable]
public class MidiHolder
{
    public Track[] tracks;

    public Note[] GetNotes()
    {
        int lastTrack = tracks.Length - 1;

        return tracks[lastTrack].notes;
    }
}
