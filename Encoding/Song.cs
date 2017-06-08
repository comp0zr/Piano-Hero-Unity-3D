using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Song 
{
    public string title;
    public Measure[] measures;

    public Song(string t, int m)
    {
        title = t;
        measures = new Measure[m];
    }

    public void AddMeasure(Measure bar)
    {
        measures[bar.barNum-1] = bar;
    }

    public Measure GetMeasure(int num)
    {
        return this.measures[num-1]; 
    }

    public int GetNoteCount()
    {
        int count = 0;

        for(int m=0; m<measures.Length; m++)
        {
            count += this.GetMeasure(m+1).notes.Length;
        }
        return count;
    }
}
