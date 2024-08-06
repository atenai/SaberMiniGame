using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dico.HyperCasualGame.Minigame
{
	public class AudioScript : MonoBehaviour
	{
		[SerializeField] AudioClip getSE;
		AudioSource audioSource;
		bool isSEActive = true;

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		void Update()
		{
			if (isSEActive)
			{
				audioSource.PlayOneShot(getSE);
				isSEActive = false;
			}
		}
	}
}