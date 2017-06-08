using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteNumber : MonoBehaviour {

    public static IDictionary<string, int> noteNumbers = new Dictionary<string, int>();
    string[] noteList = new string[12] {"C", "C#/Db", "D", "D#/Eb", "E", "F", "F#/Gb", "G", "G#/Ab", "A", "A#/Bb", "B"};  

    void Awake () 
    {
        int count=3;

        foreach(string c in noteList)
        {
            noteNumbers[c] = count;
            count++;
        } 
	}

    public static int GetNum(string name, int octave)
    {
        int noteNum = (octave-1)*12 + noteNumbers[name];
        return noteNum;
    }
}
