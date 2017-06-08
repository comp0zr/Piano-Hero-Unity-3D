using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EncodedSong : MonoBehaviour
{
    private Song song;
    public List<Measure.Note> noteList;
    

    void Start()
    {
        song = GameObject.Find("Encoder").GetComponent<Encode>().song;               
        noteList = new List<Measure.Note>();
    }
        

    void GetSongData(Song s)
    {
        song = new Song(s.title, s.measures.Length);       
        Debug.Log(s.GetNoteCount());

        for(int i=0; i<s.measures.Length; i++)
        {
            song.AddMeasure(s.measures[i]);

            for(int j=0; j<s.measures[i].notes.Length; j++)
            {
                noteList.Add(s.measures[i].GetNote(j));
                Debug.Log(noteList[j]);
            }
        }
        return;
    }        


    public List<Measure.Note> GetNoteList()
    {
        return this.noteList;
    }

}
