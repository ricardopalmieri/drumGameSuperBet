using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotesGroup : MonoBehaviour
{
    public int currentNoteId = 0;
    public GameNote currentNote;

    public GameNote[] notes;

    bool inputWindowNow = false;
    bool inputWindowNowPerfect = false;


    public Action OnNotesReady;

    //Private stuff
    SO_InputWindow inputWindowSettings;

    public void Setup(SO_InputWindow inputWindow)
    {
        inputWindowSettings = inputWindow;
        currentNote = null;
    }

    public void AddNotes(GameNote[] newNotes)
    {
        notes = newNotes;

        if (notes.Length >= 1)
            currentNote = notes[0];

        OnNotesReady?.Invoke();
    }

    public void CheckInputWindow(float currentTime)
    {
        if (notes.Length <= 0)
            return;

        if(currentNote == null)
        {
            Debug.LogError("No note");
            return;
        }

        if (currentNote.note.time - inputWindowSettings.inputWindowBeforePerfect < currentTime)
        {
            inputWindowNowPerfect = true;
        }

        if (currentNote.note.time - inputWindowSettings.inputWindowBefore < currentTime )
        {
            inputWindowNow = true;
        }

        if (currentTime > currentNote.note.time + inputWindowSettings.inputWindowAfterPerfect)
        {
            inputWindowNowPerfect = false;
        }

        if (currentTime > currentNote.note.time + inputWindowSettings.inputWindowAfter)
        {
            inputWindowNow = false;
            AdvanceNote();
        }

        //if (currentNoteCopy.time - inputWindowTime / 2 < currentTime)


        //if (currentTime >   currentNoteCopy.time + inputWindowTime )
        //    RemoveNote();
        

    }

    void AdvanceNote()
    {
        inputWindowNow = false;
        inputWindowNowPerfect = false;

        if (currentNoteId >= notes.Length - 1)
            return;

        notes[currentNoteId].TrySetMiss();

        currentNoteId++;
        currentNote = notes[currentNoteId];
    }

    public NoteHitType ReceiveInput()
    {
        if (!inputWindowNow)
        {
            return NoteHitType.Miss;
        }

        if (inputWindowNowPerfect)
        {
            currentNote.SetHit();
            AdvanceNote();
            return NoteHitType.Perfect;
        }
            

        currentNote.SetHit();
        AdvanceNote();
        return NoteHitType.Hit;

    }

}
