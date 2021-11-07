using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;

	[Range(.1f, 3f)]
	public float pitch = 1f;


	public bool playOnAwake = false;
	public bool loop = false;
	public bool _3D = false;
	public bool _RollOffLinear = true;
	[Range(0, 500)]
	public float Max_Distance = 500;


	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
