using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using System.Resources;
using System.Collections.Generic;

public class DisplayNoteQueue : MonoBehaviour
{
    public static Dictionary<int, char> noteVals;
    public List<Measure.Note> noteList;

    public CanvasRenderer rend;
    public Canvas canvas;
    public Text text;
    public Text front;
    public RectTransform r_transform;
    public Rect area;

    public Vector2 timeSignature;
    public List<string> text_data;
    public GameObject textDestroyer;

    GUIStyle style;
    GUIContent content;
    private Vector3[] corners;

    void Awake()
    {            
        noteVals = new Dictionary<int, char>();

        noteVals[1]='W';
        noteVals[2]='H';
        noteVals[4]='Q';
        noteVals[8]='e';
        noteVals[16]='s';
//        noteVals[32]=

        rend = GetComponent<CanvasRenderer>();
        r_transform = GetComponent<RectTransform>();
        style = new GUIStyle();
        timeSignature = new Vector2(2, 4);

        var front_y = front.transform.position.y;
        text.transform.position.Set(text.transform.position.x, front_y, text.transform.position.z);
        textDestroyer = GameObject.Find("Destroyer");
    }

    void Start()
    {
        noteList = GameObject.FindWithTag("Song").GetComponent<EncodedSong>().noteList;
        Debug.Assert(noteList[0].duration > 0);

        text_data = new List<string>();

        float beat = 1.0f;           
        int index=0;

        foreach(Measure.Note N in noteList)
        {       
        		// Convert each element in note list to a string
        		
            float mainBeat = -1.0f;
            float duration = N.duration;
            string name = N.noteName;
            int octave = N.noteOctave;

            if(beat == timeSignature.x+1) beat = 0f;

            if(Mathf.Abs((float)System.Math.Truncate(beat) - (float)beat) <= 0.1)
            {
                mainBeat = Mathf.RoundToInt(beat);
            }

            //	Smaller string above main one should show subdivisions, might even be possible on the same string with
            //	fancy formatting
            
            if(mainBeat != -1.0)
            {                
                text_data.Add(string.Format("{0}| {1}({2}){3} ", mainBeat, name, octave, noteVals[Mathf.RoundToInt(duration)]));
            }
            else
            {
                text_data.Add(string.Format("| {0}({1}){2} ", name, octave, noteVals[Mathf.RoundToInt(duration)]));
            }

            beat += (4/N.duration);
            index++;
        }
        Refresh();
    }


    void Update()
    {
         
    }

    void PopEffect()
    {
        Debug.Log("Correct note played, popping from queue...");
        
        if(!textDestroyer.GetComponent<DestroyText>().IsInvoking("DestroyFront"))
        {
            textDestroyer.GetComponent<DestroyText>().Invoke("DestroyFront", 0);
        }
    }


    void Refresh()
    {                
        if(text_data.Count == 0)
        {
        		// If the queue is empty, trigger completion code.
        		
            Complete();
            return;
        }

        front.text = text_data[0];
        text.text = string.Empty;

        for(int i=1; i<text_data.Count; i++) text.text += text_data[i];
    }


    void Complete()
    {                 
        front.text = "\t\tSONG COMPLETE";
    }
}
