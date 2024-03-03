using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {
	public AudioMixer audioMixer;
	public List<AudioClip> BGMList;
	public List<AudioClip> SEList;
	public int MaxSE = 10;

	private AudioSource bgmSource = null;
	private List<AudioSource> seSources = null;
	private Dictionary<string,AudioClip> bgmDict = null;
	private Dictionary<string,AudioClip> seDict = null;

	public AudioMixerGroup audioMixerBgmGroup;
	public AudioMixerGroup audioMixerSeGroup;

	public void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		if(FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
		{
			this.gameObject.AddComponent<AudioListener>();
		}
		
		this.bgmSource = this.gameObject.AddComponent<AudioSource>();
		this.bgmSource.outputAudioMixerGroup = audioMixerBgmGroup;
		this.seSources = new List<AudioSource>();

		this.bgmDict = new Dictionary<string, AudioClip>();
		this.seDict = new Dictionary<string, AudioClip>();

		Action<Dictionary<string,AudioClip>,AudioClip> addClipDict = (dict, c) => {
			if(!dict.ContainsKey(c.name))
			{
				dict.Add(c.name,c); 
			}
		};

		if (BGMList != null && BGMList.Count != 0)
		{
			this.BGMList.ForEach(bgm => addClipDict(this.bgmDict, bgm));
		}

		if (SEList != null && SEList.Count != 0)
		{
			this.SEList.ForEach(se => addClipDict(this.seDict, se));
		}
	}

	public void PlaySE(string seName, bool loop=false)
	{
		if (!this.seDict.ContainsKey(seName))
		{
			Debug.LogWarning($"not found seName = {seName}");
			return;
		}

		AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
		if(source == null)
		{
			if(this.seSources.Count >= this.MaxSE)
			{
				Debug.Log("SE AudioSource is full");
				return;
			}

			source = this.gameObject.AddComponent<AudioSource>();
			source.outputAudioMixerGroup = audioMixerSeGroup;
			this.seSources.Add(source);
		}

		source.clip = this.seDict[seName];
		source.loop = loop;
		source.Play();
	}

	public void StopSE()
	{
		this.seSources.ForEach(s => s.Stop());
	}

	public void PlayBGM(string bgmName, bool loop=true)
	{
		Debug.Log ("PlayBGM name=" + bgmName);
		if(!this.bgmDict.ContainsKey(bgmName)) throw new ArgumentException(bgmName + " not found","bgmName");  
		if(this.bgmSource.clip == this.bgmDict[bgmName]) return;
		this.bgmSource.Stop();
		this.bgmSource.clip = this.bgmDict[bgmName];
		this.bgmSource.Play(); 
		this.bgmSource.loop = loop;
	}

	public void StopBGM()
	{
		this.bgmSource.Stop();
		this.bgmSource.clip = null;
	}

	public void Suspend()
	{
	}
	public void Resume()
	{
	}
}