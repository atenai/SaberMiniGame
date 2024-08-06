using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dico.HyperCasualGame.Minigame
{
	public class HitScoreTMP : MonoBehaviour
	{
		float alphaColor = 1.0f;
		[SerializeField]
		float minusAlphaColor = 0.4f;

		[SerializeField]
		float moveSpeed = 1.0f;
		float currentPosZ;

		TextMeshPro hitScoreTextMeshPro;

		void Start()
		{
			StartCoroutine(StartHitScoreTMP());
		}

		void Update()
		{
			StartCoroutine(UpdateHitScoreTMP());
		}

		IEnumerator StartHitScoreTMP(float WaitForSeconds = 0.0f)
		{
			hitScoreTextMeshPro = GetComponent<TextMeshPro>();
			hitScoreTextMeshPro.alpha = 1.0f;
			yield return new WaitForSeconds(WaitForSeconds);
		}

		IEnumerator UpdateHitScoreTMP(float WaitForSeconds = 0.0f)
		{
			alphaColor -= minusAlphaColor * Time.deltaTime;
			hitScoreTextMeshPro.alpha = alphaColor;

			if (alphaColor <= 0)
			{
				Destroy(gameObject);
			}

			currentPosZ = transform.position.z + (moveSpeed * Time.deltaTime);
			//Debug.Log(f_CurrentPosZ);
			transform.position = new Vector3(transform.position.x, transform.position.y, currentPosZ);
			yield return new WaitForSeconds(WaitForSeconds);
		}
	}
}
