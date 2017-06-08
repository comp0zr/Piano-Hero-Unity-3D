using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class NoteMap : MonoBehaviour 
{

    public GameObject key;
    public int keyNumber;
    public int octave;
    public string keyName;

    public bool isDown;

    Renderer rend;
    Material material;
    Color col;

    public EncodedSong song;
    public DisplayNoteQueue text;
    public DisplayNoteQueue front;

    public Text text_string;

    void Start()
    {
        key = this.gameObject;
        rend = key.GetComponent<Renderer>();
        material = rend.material;
        col = material.color;
        string[] split = key.name.Split('.');
        keyName = split[0];
        octave = int.Parse(split[1]);
        keyNumber = NoteNumber.GetNum(split[0], octave);

        song = GameObject.Find("Song").GetComponent<EncodedSong>();
        text = GameObject.Find("NoteQueue").GetComponent<DisplayNoteQueue>();
        front = GameObject.Find("Front").GetComponent<DisplayNoteQueue>();
        text_string = GameObject.Find("NoteQueue").GetComponent<Text>();
    }

	
	void Update () 
    {    
        if(MidiInput.GetKeyDown(keyNumber+21))
        {        		
            isDown = true;           

            if(keyNumber == song.noteList[0].noteNum)
            {
				    // If the MIDI input matches the current target note...            	                
				    
                material.color = Color.Lerp(material.color, Color.blue, 1-Time.deltaTime);

                if(song.noteList.Count == 1)
                {
                    text.Invoke("Complete", 0);
                }
                else
                {
                	// Pop queue, trigger corresponding effects, update text
                	                	
                  front.Invoke("PopEffect", 0f);
                	song.noteList.RemoveAt(0);
                	text.text_data.RemoveAt(0);
                	text.Invoke("Refresh", 0);
                }
            }
            else
            {
            	 // If note played is incorrect, change color of both the note text and corresponding 
            	 // piano key to red
            	 
                material.color = Color.Lerp(material.color, Color.red, 1-Time.deltaTime);
                front.front.color = Color.Lerp(front.text.color, Color.red, 1-Time.deltaTime);

                Debug.LogFormat("WRONG NOTE! Expected: {0} {1}", song.noteList[0].noteName, song.noteList[0].noteOctave, song.noteList[0].duration); 
            }
        }
        else if(MidiInput.GetKeyUp(keyNumber+21))
        {
        		// Reset colors to default if no note is played
        		
            isDown = false;
            material.color = Color.Lerp(material.color, col, 1-Time.deltaTime);
            front.front.color = Color.Lerp(front.text.color, Color.green, 1-Time.deltaTime);
        }            
    }
}
