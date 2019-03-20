using System;
using UnityEngine;

namespace FreeLives
{
	// Token: 0x020000DB RID: 219
	public class SoundController : MonoBehaviour
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00024EE8 File Offset: 0x000232E8
		protected void Awake()
		{
			this.globalSounds = base.GetComponent<SoundHolder>();
			if (SoundController.instance != null)
			{
				UnityEngine.Object.DestroyImmediate(SoundController.instance);
			}
			SoundController.instance = this;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00024F16 File Offset: 0x00023316
		public static void PlaySoundEffect(string soundEffectName)
		{
			SoundController.PlaySoundEffect(SoundController.instance.globalSounds, soundEffectName);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00024F28 File Offset: 0x00023328
		public static void PlaySoundEffect(SoundHolder soundHolder, string soundEffectName)
		{
			AudioSource audioSource = SoundController.CreateAudioSource();
			audioSource.spatialize = false;
			SoundInfo soundInfo = SoundController.GetSoundInfo(soundHolder, soundEffectName);
			AudioClip audioClip = soundInfo.clips[UnityEngine.Random.Range(0, soundInfo.clips.Length)];
			audioSource.clip = audioClip;
			audioSource.volume = soundInfo.volumeMod;
			audioSource.pitch = SoundController.GetPitch(soundInfo.pitchVariance);
			audioSource.Play();
			UnityEngine.Object.Destroy(audioSource.gameObject, audioClip.length * audioSource.pitch + 1f);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00024FA8 File Offset: 0x000233A8
		public static void StopMusic()
		{
			if (SoundController.instance.GetComponent<AudioSource>() != null)
			{
				SoundController.instance.GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00024FCE File Offset: 0x000233CE
		public static void PlaySoundEffect(string soundEffectName, float volume)
		{
			SoundController.PlaySoundEffect(SoundController.instance.globalSounds, soundEffectName, volume);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00024FE4 File Offset: 0x000233E4
		public static void PlaySoundEffect(SoundHolder soundHolder, string soundEffectName, float volume)
		{
			SoundInfo soundInfo = SoundController.GetSoundInfo(soundHolder, soundEffectName);
			if (soundInfo != null && soundInfo.clips.Length > 0)
			{
				AudioSource audioSource = SoundController.CreateAudioSource();
				audioSource.spatialize = false;
				AudioClip audioClip = soundInfo.clips[UnityEngine.Random.Range(0, soundInfo.clips.Length)];
				audioSource.clip = audioClip;
				audioSource.volume = soundInfo.volumeMod * volume;
				audioSource.pitch = SoundController.GetPitch(soundInfo.pitchVariance);
				audioSource.Play();
				UnityEngine.Object.Destroy(audioSource.gameObject, audioClip.length * audioSource.pitch + 1f);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0002507A File Offset: 0x0002347A
		public static void PlaySoundEffect(string soundEffectName, float volume, Vector3 pos)
		{
			SoundController.PlaySoundEffect(SoundController.instance.globalSounds, soundEffectName, volume, pos);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00025090 File Offset: 0x00023490
		public static void PlaySoundEffect(SoundHolder soundHolder, string soundEffectName, float volume, Vector3 pos)
		{
			SoundInfo soundInfo = SoundController.GetSoundInfo(soundHolder, soundEffectName);
			if (soundInfo == null || soundInfo.clips.Length == 0)
			{
				Debug.LogError("No sound effects for " + soundEffectName);
				return;
			}
			AudioSource audioSource = SoundController.CreateAudioSource();
			audioSource.transform.position = pos;
			AudioClip audioClip = soundInfo.clips[UnityEngine.Random.Range(0, soundInfo.clips.Length)];
			audioSource.spatialBlend = 1f;
			audioSource.clip = audioClip;
			audioSource.volume = soundInfo.volumeMod * volume;
			audioSource.pitch = SoundController.GetPitch(soundInfo.pitchVariance);
			audioSource.Play();
			UnityEngine.Object.Destroy(audioSource.gameObject, audioClip.length * audioSource.pitch + 1f);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00025146 File Offset: 0x00023546
		public static void PlaySoundEffect(string soundEffectName, float volume, Vector3 pos, float pitch)
		{
			SoundController.PlaySoundEffect(SoundController.instance.globalSounds, soundEffectName, volume, pos, pitch);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0002515C File Offset: 0x0002355C
		public static void PlaySoundEffect(SoundHolder soundHolder, string soundEffectName, float volume, Vector3 pos, float pitch)
		{
			AudioSource audioSource = SoundController.CreateAudioSource();
			audioSource.transform.position = pos;
			SoundInfo soundInfo = SoundController.GetSoundInfo(soundHolder, soundEffectName);
			AudioClip audioClip = soundInfo.clips[UnityEngine.Random.Range(0, soundInfo.clips.Length)];
			audioSource.spatialBlend = 1f;
			audioSource.clip = audioClip;
			audioSource.volume = soundInfo.volumeMod * volume;
			audioSource.pitch = pitch * SoundController.GetPitch(soundInfo.pitchVariance);
			audioSource.Play();
			UnityEngine.Object.Destroy(audioSource.gameObject, audioClip.length * audioSource.pitch + 1f);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000251F4 File Offset: 0x000235F4
		private static SoundInfo GetSoundInfo(SoundHolder soundHolder, string name)
		{
			name = name.ToUpper();
			for (int i = 0; i < soundHolder.sounds.Length; i++)
			{
				if (name.Equals(soundHolder.sounds[i].name.ToUpper()))
				{
					return soundHolder.sounds[i];
				}
			}
			Debug.LogError("Could not find sound: " + name);
			return null;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0002525C File Offset: 0x0002365C
		private static AudioSource CreateAudioSource()
		{
			AudioSource audioSource = new GameObject
			{
				name = "Audio Oneshot"
			}.AddComponent<AudioSource>();
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.dopplerLevel = 0f;
			return audioSource;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00025294 File Offset: 0x00023694
		private static float GetPitch(float variance)
		{
			float num = 1f + variance;
			float min = 1f / num;
			float max = 1f + variance;
			return UnityEngine.Random.Range(min, max);
		}

		// Token: 0x040003DE RID: 990
		private SoundHolder globalSounds;

		// Token: 0x040003DF RID: 991
		private static SoundController instance;
	}
}
