using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.playOnAwake = s.playOnAwake;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.outputAudioMixerGroup = mixerGroup;

			if (s._3D)
			{
				s.source.spatialBlend = 1;
				s.source.maxDistance = s.Max_Distance;
				/*if (s._RollOffLinear)
				{
					s.source.rolloffMode = AudioRolloffMode.Linear;
				}*/
			}
			else
			{
				s.source.spatialBlend = 0;
			}
		}
		

	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();
	}


	public void Mute(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.mute = true;
	}

}
