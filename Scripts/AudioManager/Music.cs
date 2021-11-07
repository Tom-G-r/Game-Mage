using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Music : MonoBehaviour
{
	public static Music instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public PlayerStats player;

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
			}
			else
			{
				s.source.spatialBlend = 0;
			}

			GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
			if (playerObj != null)
            {
				player = playerObj.GetComponent<PlayerStats>();
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

		s.source.mute = false;
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

	void Start()
	{
		PlayBackgroundMusic();
	}

	public void PlayBackgroundMusic()
	{
		Play("BackgroundMusic");
		Mute("BattleMusic");
	}

	public void PlayBattleMusic()
	{
		Play("BattleMusic");
		Mute("BackgroundMusic");
	}

}

