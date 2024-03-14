using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPerfectSpawner : MonoBehaviour
{
    [SerializeField] private int noteId;
    [SerializeField] private GameObject prefab;
    [SerializeField] private MusicGamplay musicGamplay;

    private void OnEnable()
    {
        musicGamplay.OnNoteHit+= SpawnNote;
    }

    private void OnDisable()
    {
        musicGamplay.OnNoteHit -= SpawnNote;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SpawnNote();
        }
    }

    void SpawnNote(int id, NoteHitType hitType)
    {
        if (noteId == id &&  hitType == NoteHitType.Perfect)
            SpawnNote();
    }

    private void SpawnNote()
    {
        Instantiate(prefab, transform);
    }
}
