using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Dico.HyperCasualGame.Minigame
{
	public class ResultFade : Fade
	{
		void Start()
		{
			StartCoroutine(StartResultColor());
		}

		void Update()
		{
			StartCoroutine(UpdateResultFade());
		}

		IEnumerator StartResultColor(float WaitForSeconds = 0.0f)
		{
			colorR = fade.color.r;
			colorG = fade.color.g;
			colorB = fade.color.b;
			yield return new WaitForSeconds(WaitForSeconds);
		}

		IEnumerator UpdateResultFade(float WaitForSeconds = 0.0f)
		{
			if (cube_Effect.IsFadeActive)
			{
				fade.color = new Color(colorR, colorG, colorB, alpha);
				alpha += speed * Time.deltaTime;
			}

			if (1 <= alpha)
			{
				if (SceneManager.GetActiveScene().name == "3DMiniGameScene")
				{
					SceneManager.LoadScene("3DMiniGameScene");
				}
				if (SceneManager.GetActiveScene().name == "3DMiniGameMeshCutScene")
				{
					SceneManager.LoadScene("3DMiniGameMeshCutScene");
				}
				if (SceneManager.GetActiveScene().name == "TargetPointMoveScene")
				{
					SceneManager.LoadScene("TargetPointMoveScene");
				}
			}

			yield return new WaitForSeconds(WaitForSeconds);
		}
	}
}