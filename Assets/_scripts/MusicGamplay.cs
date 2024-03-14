using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGamplay : MonoBehaviour
{
    // Actions
    public Action<int, NoteHitType> OnNoteHit;

    public Action OnMusicEnd;


    public SO_SongMidi songMidi;

    [Header("References")]
    public Transform noteParent;
    public AudioSource audioSource;
    public SO_InputWindow inputWindowSettings;

    [Header("Settings")]
    public float speed = 3;


    [Space]
    public bool playing;
    public float startTime;
    public float currentTime;

    [Space]

    [Header(" Notes Groups")]
    public NotesGroup group1;
    public NotesGroup group2;
    public NotesGroup group3;
    public NotesGroup group4;


    //public int currentNoteId = 0;

    //public Note currentNoteCopy;

    //[Header("Settings")]

    void Awake()
    {
        //PrepareTrack();
    }

    private void PrepareTrack()
    {
        LoadMusic();
        UpdateSettings();
        TestPrepareNotes();
    }

    void UpdateSettings()
    {
        group1.Setup(inputWindowSettings);
        group2.Setup(inputWindowSettings);
        group3.Setup(inputWindowSettings);
        group4.Setup(inputWindowSettings);
    }

    [ContextMenu("LoadMusic")]
    void LoadMusic()
    {
        audioSource.clip = songMidi.audioClip;
       // Debug.Log(" Loaded Music");
    }

    /*
    void UpdateNote()
    {


        currentNoteCopy = songMidi.notes[currentNoteId];
    }

    [ContextMenu("Advance Note")]
    void AdvanceNote()
    {
        noteSpawner.notes[currentNoteId].SetMissMaybe();
          

        if (currentNoteId >= songMidi.notes.Length-1)
            return;

        currentNoteId++;
        UpdateNote();
    }


    [ContextMenu("Remove Note")]
    void RemoveNote()
    {
        currentNoteCopy = null;
    }
    */
    [ContextMenu("StartGame")]
    public void StartGame()
    {
        playing = true;
        startTime = Time.timeSinceLevelLoad;
        audioSource.Play();

        StartCoroutine(WaitEndMusic(audioSource.clip.length));
    }

    IEnumerator WaitEndMusic(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndMusic();
    }

    void EndMusic()
    {
        playing = false;
        OnMusicEnd.Invoke();
    }

    void Update()
    {
        if (playing)
        {
            // Move notes
            noteParent.position += Vector3.back * speed * Time.deltaTime;
            currentTime = Time.timeSinceLevelLoad - startTime;


            #region CheckInputWindow

            group1.CheckInputWindow(currentTime);
            group2.CheckInputWindow(currentTime);
            group3.CheckInputWindow(currentTime);
            group4.CheckInputWindow(currentTime);

            /*
            // Add and remove notes
            if (currentNoteCopy.time - inputWindowBefore < currentTime)
            {
                
                inputWindowNow = true;
            }

            if (currentTime > currentNoteCopy.time + inputWindowAfter)
            {
                inputWindowNow = false;
                AdvanceNote();
            }

            //if (currentNoteCopy.time - inputWindowTime / 2 < currentTime)

                
                //if (currentTime >   currentNoteCopy.time + inputWindowTime )
                //    RemoveNote();
                //
            
             */

            #endregion
        }

    }

    internal void SetTrack(SO_SongMidi newTrack)
    {
        songMidi = newTrack;
        PrepareTrack();
    }

    [ContextMenu("Test Prepare Notes")]
    void TestPrepareNotes()
    {
        //currentNoteCopy = null;

        GetNotesByMidiId(60, group1);
        GetNotesByMidiId(62, group2);
        GetNotesByMidiId(64, group3);
        GetNotesByMidiId(65, group4);

       // Debug.Log("Preparation Done");
    }

    void GetNotesByMidiId(int id, NotesGroup group)
    {
        List<GameNote> notes = new List<GameNote>();

        foreach (var item in songMidi.notes)
        {
            if (item.midi == id)
                notes.Add(new GameNote(item));
        }

        group.AddNotes(notes.ToArray());

       // Debug.Log(string.Format("Get Midi notes {0} Done!", id));
    }

    public void OnInputNote1()
    {
        var hitType = group1.ReceiveInput();
        OnNoteHit?.Invoke(1, hitType);
    }

    public void OnInputNote2()
    {
        var hitType = group2.ReceiveInput();
        OnNoteHit?.Invoke(2, hitType);
    }

    public void OnInputNote3()
    {
        var hitType = group3.ReceiveInput();
        OnNoteHit?.Invoke(3, hitType);
    }

    public void OnInputNote4()
    {
        var hitType = group4.ReceiveInput();
        OnNoteHit?.Invoke(4, hitType);
    }
}



[System.Serializable]
public class GameNote
{
    public NoteStatus status;
    public Note note;

    public Action OnHit;
    public Action OnMiss;

    public GameNote(Note n)
    {
        status = NoteStatus.Waiting;
        note = n;
    }

    public void SetHit()
    {
        status = NoteStatus.Hit;
        OnHit?.Invoke();
        //Debug.Log("Hit");
    }

    public void TrySetMiss()
    {
        if (status == NoteStatus.Waiting)
        {
            status = NoteStatus.Miss;
            OnMiss?.Invoke();
            //Debug.Log("Miss");
        }
    }
}

public enum NoteStatus
{
    Waiting,
    Hit,
    Miss,
    Perfect
}
