using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dico.HyperCasualGame.Minigame
{
	public class Tap : MonoBehaviour
	{
		[SerializeField] TextMesh tapTextMesh;

		float alpha = 0.0f;
		const float maxAlpha = 1.0f;
		const float minAlpha = 0.0f;
		bool isAlpha = false;
		float plusAlpha = 0.5f;

		[SerializeField]
		Cube_Saber cubeSaber;

		IEnumerator Start()
		{
			yield return StartCoroutine(StartTapTMPColor());
		}

		void Update()
		{
			UpdateTapTMPColor();
		}

		IEnumerator StartTapTMPColor()
		{
			Debug.Log("StartTapTMPColor");
			if (tapTextMesh != null)
			{
				tapTextMesh.color = new Color(255.0f, 255.0f, 255.0f, alpha);
			}
			yield return null;
		}

		void UpdateTapTMPColor()
		{
			if (!cubeSaber.IsOneClick)
			{
				if (tapTextMesh != null)
				{
					tapTextMesh.text = "Flick";
				}
			}
			else
			{
				if (tapTextMesh != null)
				{
					tapTextMesh.text = "";
				}

#if UNITY_EDITOR || UNITY_STANDALONE_WIN//UnityEditorの時とWindowsスタンドアロンアプリケーションの時のみ起動する 

				//シーン遷移（デバッグ）
				if (Input.GetKeyDown(KeyCode.Q))
				{
					SceneManager.LoadScene("3DMiniGameScene");
				}

#endif//#if UNITY_EDITOR || UNITY_STANDALONE_WIN 
			}

			if (maxAlpha <= alpha)
			{
				isAlpha = true;
			}
			if (alpha <= minAlpha)
			{
				isAlpha = false;
			}

			if (isAlpha)
			{
				alpha -= plusAlpha * Time.deltaTime;
			}
			else
			{
				alpha += plusAlpha * Time.deltaTime;
			}

			if (tapTextMesh != null)
			{
				tapTextMesh.color = new Color(255.0f, 255.0f, 255.0f, alpha);
			}
		}
	}
}