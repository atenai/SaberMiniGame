using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Dico.Helper;//本番環境用

namespace Dico.HyperCasualGame.Minigame
{
	public class HitScoreText : MonoBehaviour
	{
		//α値は最大値が1
		float alphaColor = 1.0f;
		[SerializeField]
		float minusAlphaColor = 0.4f;

		[SerializeField]
		float moveSpeed = 1.0f;
		float currentPosZ;

		TextMesh hitScoreTextMesh;

		IEnumerator Start()
		{
			//GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, GetComponent<TextMesh>().color.a);
			hitScoreTextMesh = GetComponent<TextMesh>();
			yield return hitScoreTextMesh.color = new Color(hitScoreTextMesh.color.r, hitScoreTextMesh.color.g, hitScoreTextMesh.color.b, hitScoreTextMesh.color.a);
		}

		void Update()
		{
			alphaColor -= minusAlphaColor * Time.deltaTime;
			//Debug.Log(f_AlfaColor);
			//GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, alphaColor);
			hitScoreTextMesh.color = new Color(hitScoreTextMesh.color.r, hitScoreTextMesh.color.g, hitScoreTextMesh.color.b, alphaColor);

			//α値が0になったらこのゲームオブジェクトをデストロイする
			if (alphaColor <= 0)
			{
				Destroy(gameObject);
			}

			currentPosZ = transform.position.z + (moveSpeed * Time.deltaTime);
			//Debug.Log(f_CurrentPosZ);
			transform.position = new Vector3(transform.position.x, transform.position.y, currentPosZ);//テストプロジェクト用
		}
	}
}