using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dico.HyperCasualGame.Minigame
{
	public class ButtonActive : MonoBehaviour
	{

		[SerializeField] Cube_Saber cubeSaber;

		void Start()
		{
			this.gameObject.SetActive(true);
		}

		void Update()
		{
			if (cubeSaber.IsOneClick)
			{
				this.gameObject.SetActive(false);
			}
		}
	}
}