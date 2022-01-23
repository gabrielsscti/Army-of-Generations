using UnityEngine;
using System.Collections;

public class TocarSom : MonoBehaviour
{
	private AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot( audioSource.clip );
	}

	void Update () 
	{
		if ( !audioSource.isPlaying )
		{
			Destroy( gameObject );
		}
	}

	public static void Tocar( GameObject objeto, AudioClip clip )
	{
		GameObject gobj = new GameObject( clip.name );
		gobj.transform.position = objeto.transform.position;
		AudioSource source = gobj.AddComponent<AudioSource>();
		source.playOnAwake = false;
		source.clip = clip;
		gobj.AddComponent<TocarSom>();
	}
}
