using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundManager : MonoBehaviour {
	public static UI_SoundManager Instance;

	public AudioSource audioSource;
	public AudioSource playerAudioSource;

	[Header("UI Button clips")]
	public AudioClip confirmButton;
	public AudioClip cancelButton;

	[Header("UI Animation")]
	public AudioClip windowAppear;
	public AudioClip windowHide;


	public void Awake(){
		Instance = this;
	}

	public void ConfirmButtonPressed(float volume = 1){
		audioSource.PlayOneShot (confirmButton, volume);
	}

	public void CancelButtonPressed(float volume = 1){
		audioSource.PlayOneShot (cancelButton, volume);
	}

	public void WindowAppear(float volume = 1){
        Debug.Log("XD");
        StartCoroutine (ReduceSound ());
        Debug.Log("XD1");
        audioSource.PlayOneShot (windowAppear, volume);
	}

	public void WindowHide(float volume = 1){
		audioSource.PlayOneShot (windowHide, volume);

		StartCoroutine (IncreaseSound ());
	}

	private IEnumerator ReduceSound()
	{
		Debug.Log (playerAudioSource);
		Debug.Log ("XD");
		while (playerAudioSource.volume >= 0.001) {
			playerAudioSource.volume = Mathf.Lerp(playerAudioSource.volume, 0f, Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator IncreaseSound()
	{
		while (playerAudioSource.volume <= 0.9) {
			playerAudioSource.volume = Mathf.Lerp(playerAudioSource.volume, 1f, Time.deltaTime);
			yield return null;
		}
	}
}
