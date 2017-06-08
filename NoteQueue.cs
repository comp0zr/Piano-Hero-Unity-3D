/*
	Initializes note queue
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteQueue : MonoBehaviour {

    [SerializeField] private Song song;
    [SerializeField] private Measure.Note[] note_queue;


    void Start()
    {   
        song = GameObject.Find("Encoder").GetComponent<Encode>().song;
        note_queue = new Measure.Note[song.GetNoteCount()];
        Debug.Log(song.GetNoteCount());

        int count = 0;
    }
}
