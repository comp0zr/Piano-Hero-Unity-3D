using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Encode : MonoBehaviour {

    public Song song;

    public enum Action {Comment=0, Change, Default};
    public TextAsset code;

    public string text;
    string title;
    int numMeasures;

    [SerializeField] private string[] lines;
    private Measure[] bars;

    void Awake ()
    {
        text = code.text;
        lines = text.Trim().Split(new char[] {'\\', '\n'});

        if(lines[0].Contains("TITLE"))
        {
            string[] split = lines[0].Split('=');
            title = split[1];      
        }
        if(lines[1].Contains("MEASURES"))
        {
            string[] split = lines[1].Split('=');
            numMeasures = int.Parse(split[1]);                   
        }
        bars = new Measure[numMeasures];
        song = new Song(title, numMeasures);
    }
        

	void Start () 
    {
        Action action = Action.Default;
        int ts_numBeats=0, ts_beatVal=0;
        int measure = 1;
        int totalNotes = 0;
        int noteIndex = 0;
//        bool change_ts, change_clef;

        foreach(string L in lines)
        {           
            if(L.StartsWith("%"))
            {
                if(L.Contains("END"))
                {
                    song.measures = bars;
                    BroadcastMessage("GetSongData", song, SendMessageOptions.RequireReceiver);
                    return;
                }
            }
            if(L.Length==0 || L.Equals(" ")) continue;

            if(L.StartsWith("#"))
            {                
                string[] split = L.Split(new char[] {':', '.'});

                if(split.Length == 3)
                {
                    ts_numBeats = int.Parse(split[1]);
                    ts_beatVal = int.Parse(split[2]);
                }
                continue;
            }
            if(L.StartsWith("{") && L.EndsWith("}"))
            {                
                string[] split = L.Split(new char[] {':', '{', '}'});
                measure = int.Parse(split[1]);
                totalNotes = int.Parse(split[2]);
                action = Action.Default;
                bars[measure-1] = new Measure(measure, ts_numBeats, ts_beatVal, totalNotes);
                noteIndex=0;
            }


            switch(action)
            {
                case Action.Default: 
                    
                    if(noteIndex < totalNotes)
                    {
                        if(L.EndsWith("|"))
                        {
                            string[] split = L.Split(new char[] {'.', '|', '(', ')'});
                            string n_name = split[0];
                            int octave = int.Parse(split[1]);
                            float dur = int.Parse(split[2]);
                        
                            bars[measure-1].Add(new Measure.Note(n_name, octave, dur), noteIndex);
                            noteIndex++;
                        }
                    }
                    break;                
            }

        }
    }
}