/*
	If correct note is found, this script controls the "explosion" effect
*/

using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets;
using System.Collections;

public class DestroyText : MonoBehaviour 
{
    public ParticleSystem explosion;
    public AudioClip[] explosionSounds;
    public AudioSource sound;
    public TextMesh text;

    private string[] frontText;
    ParticleSystem.Particle[] particles;


	void Awake() 
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        explosion.playOnAwake = false;
        sound = GetComponent<AudioSource>();
        text = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
    void DestroyFront()
    {           
            Text textToDestroy = GameObject.Find("Front").GetComponent<Text>();
            text.text = textToDestroy.text;
            text.color = textToDestroy.color;
            text.characterSize = 3;

            sound.PlayOneShot(explosionSounds[Random.Range(0, 2)]);
            explosion.Play();

    //        InitializeIfNeeded();
        
            int numParticles = explosion.GetParticles(particles); 


            string[] chars = text.text.Split();

            foreach(string s in chars)
            {                            
                Rigidbody textPiece;
                text.text = s;
                textPiece = Instantiate(text, text.transform.position, Random.rotation) as Rigidbody;
                textPiece.AddForce(textPiece.transform.forward * 5000);
            }
    }

    private void LateUpdate()
    {           
        if(gameObject.name.Contains("Clone"))
        {
            Destroy(gameObject, 2);
        }
    }

    void InitializeIfNeeded()
    {
        if(explosion == null)
        {
            explosion = GetComponent<ParticleSystem>();
        }

        if(particles == null || particles.Length < explosion.maxParticles)
        {
            particles = new ParticleSystem.Particle[explosion.maxParticles];
        }
    }
}
