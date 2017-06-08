using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[Serializable]
public class Measure 
{
    [Serializable]
    public struct Note 
    {    
        public string noteName;
        public int noteNum;
        public int noteOctave;
        public float duration;

        public Note(string name, int octave, float dur)
        {
            noteName = name;
            noteNum = NoteNumber.GetNum(name, octave);
            noteOctave = octave;
            duration = dur;
        }            
    }
    // Polymorphism may be a useful way to implement more varieties of notes (i.e. Note n = new QuarterNote(...) or Note r = new Rest(..));

    public int barNum;
    public int beats;
    public int beatValue;
    public Note[] notes;   

    public Measure(int num, int ts_b, int ts_v, int n)
    {
        barNum = num;
        beats = ts_b;
        beatValue = ts_v;
        notes = new Note[n];
    }        

    public Note GetNote(int index)
    {
        return this.notes[index];
    }

    public void Add(Note N, int index)
    {
        notes[index] = N;
    }

    public Note[] Notes
    {        
        get { return this.notes; }
        set { this.notes = value; }
    }        
}
