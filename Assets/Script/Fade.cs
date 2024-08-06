using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Dico.HyperCasualGame.Minigame
{
	public class Fade : MonoBehaviour
	{
		[SerializeField]
		protected Image fade;

		[SerializeField]
		protected Cube_Effect cube_Effect;

		protected float colorR = 0.0f;

		protected float colorG = 0.0f;

		protected float colorB = 0.0f;
		protected float alpha = 0.0f;
		[SerializeField]
		protected float speed = 1.0f;
	}
}