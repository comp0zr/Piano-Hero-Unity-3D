using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateNoteGraphic : MonoBehaviour {

    public List<Measure.Note> noteList;

    public GameObject quarter_note;
    public GameObject quarter_rest;
    public GameObject eighth_note;
    public GameObject eighth_rest;

    public Transform position;
    public Sprite headNote;

    public int stem_x;
    public int stem_length;
    public float stem_width = 2.0f;
    private bool isComplete = false;

	void Start () 
    {       
        quarter_note = GameObject.Find("Quarter Note");
        eighth_note = GameObject.Find("Eighth Note");


        position = GameObject.FindWithTag("Spacing Guide").transform;
        noteList = GameObject.FindWithTag("Song").GetComponent<EncodedSong>().noteList; 
        //noteList = song.noteList;

//        stem.SetWidth(stem_width, stem_width);
//        stem.sortingLayerName = "Notes";
//        stem.SetWidth(0.5f, 0.5f);
	}
	
	void Update () 
    {
        if(!isComplete)
        {            
            Stack<GameObject> notes = new Stack<GameObject>();
            position.position.Set(position.position.x, transform.position.y, position.position.z);

            for(int i=0; i<noteList.Count; i++)
            {
                GameObject next_note = null;


                if(noteList[i].duration == 4)
                {
                    next_note = Instantiate(quarter_note, position) as GameObject;
                }
                else if(noteList[i].duration == 8)
                {
                    next_note = Instantiate(eighth_note, position) as GameObject;                   
                    next_note.GetComponent<LineRenderer>().SetPositions(new Vector3[2] { position.position, new Vector3(position.position.x, position.position.y + stem_length) });

                    if(i > 0 && noteList[i-1].duration == 8)
                    {
                        notes.Peek().GetComponent<LineRenderer>().SetPosition(3, new Vector3(position.position.x, position.position.y + stem_length, position.position.z));                
                    }
                    position.position = position.transform.FindChild("Eighth Position").localPosition;
                }
                notes.Push(next_note);

            }

//            Vector3 pos = position.position;
//            pos.x += stem_x;
//
//            stem.SetPosition(0, pos);
//            stem.SetPosition(1, new Vector3(pos.x, pos.y+stem_length, pos.z));
        }
	}
}
