using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Dico.HyperCasualGame.Minigame
{
	public class Cube_Enemy : MonoBehaviour
	{
		//当たり判定系
		//多重当たり判定を消す
		bool isOneHitActive = false;
		//このゲームオブジェクトの当たり判定を消す為の処理
		SphereCollider sphereCollider;

		//cube_Saberの多重当たり判定の判定処理をもってくる為
		[SerializeField]
		Cube_Saber cube_Saber;

		//スコア
		[SerializeField]
		Score score;

		//ランキングスコア
		[SerializeField]
		RankingScore rankingScore;

		//左右移動
		[SerializeField]//変数をprivateにしてもインスペクターから数値を変更する事ができるようにする
		bool isMoveActive = true;
		[SerializeField]
		bool isStartFromTheMoveRight = false;
		[SerializeField]
		float moveSpeed = 20;
		[SerializeField]
		float moveLeftLength = -20;
		[SerializeField]
		float moveRightLength = 20;
		float destinationPosLeftX = 0;
		float destinationPosRightX = 0;
		float currentPosX;

		//ヒットエフェクト
		[SerializeField] GameObject hitEffectPrefab;
		[SerializeField] float hitEffectPrefabDestroyTime = 1.0f;
		[SerializeField] GameObject hitHinokoEffectPrefab;
		[SerializeField] float hitHinokoEffectPrefabDestroyTime = 1.0f;

		//ヒットSE
		[SerializeField]
		GameObject hitSEPrefab;
		[SerializeField]
		float hitSEPrefabDestroyTime = 1.0f;

		//ヒットスコアText
		[SerializeField]
		int scoreNum;
		[SerializeField]
		GameObject hitScoreTextPrefab;

		//スコアUI
		[SerializeField]
		TextMeshPro targetScoreTextMeshPro;

		[SerializeField] Cube_Effect cube_Effect;

		void Start()
		{
			moveSpeed = Random.Range(10, 26); // ※ 10～25の範囲でランダムな整数値が返る
			moveLeftLength = Random.Range(-10, -16);
			moveRightLength = Random.Range(10, 16);
			scoreNum = Random.Range(100, 1001);
			int randomNum = Random.RandomRange(0, 2);
			if (randomNum == 0)
			{
				isStartFromTheMoveRight = false;
			}
			else
			{
				isStartFromTheMoveRight = true;
			}

			StartCoroutine(StartMaterialColor());

			sphereCollider = GetComponent<SphereCollider>();

			if (targetScoreTextMeshPro != null)
			{
				targetScoreTextMeshPro.text = scoreNum.ToString();
			}

			StartMove();
		}

		void LateUpdate()
		{
			//if (cube_Saber.GetisSaverOneHitActive())
			//if (cube_Saber.IsSaberOneHitActive)
			//{
			//    //Debug.Log("ボックスコライダーをOFF");

			//    sphereCollider.enabled = false;
			//    Debug.Log("sphereCollider.enabled" + sphereCollider.enabled);
			//}

			//if (!cube_Saber.IsSaberOneHitActive)
			//{
			UpdateMove();
			//}

		}

		void OnTriggerEnter(Collider hit)
		{
			if (SceneManager.GetActiveScene().name == "3DMiniGameMeshCutScene")
			{
				if (hit.CompareTag("Cube_Saber") || hit.CompareTag("Cube_Saber_Tip") && !isOneHitActive)//メッシュカットテスト用
				{
					//Debug.Log("Cube_Saberに接触したよ");
					isOneHitActive = true;
					isMoveActive = false;

#if UNITY_ANDROID//Androidプラットフォームの時のみ起動する 
					//android端末を振動させる（0.5秒程度の振動が1回だけ行われる）
					if (SystemInfo.supportsVibration)
					{
						Handheld.Vibrate();
					}
#endif//#if UNITY_ANDROID

					//スコア
					score.AddScore(scoreNum);
					if (rankingScore != null)
					{
						rankingScore.AddBestScore(scoreNum);
					}

					if (hitScoreTextPrefab != null)
					{
						StartCoroutine(HitScoreText());
					}

					if (hitEffectPrefab != null && hitHinokoEffectPrefab != null)
					{
						//ヒットエフェクト
						HitEffect();
					}

					if (hitSEPrefab != null)
					{
						HitSE();
					}


					//Destroy(this.gameObject);
				}
			}

			if (SceneManager.GetActiveScene().name == "3DMiniGameScene")
			{
				//if (hit.CompareTag("Cube_Saber_Tip") && isOneHitActive == false && cube_Saber.Get_b_SaverOneHitActive() == true)//使わないやつ
				if (hit.CompareTag("Cube_Saber_Tip") && !isOneHitActive)//本番環境用
				{
					//Debug.Log("Cube_Saberに接触したよ");
					isOneHitActive = true;
					isMoveActive = false;

#if UNITY_ANDROID//Androidプラットフォームの時のみ起動する 
					//android端末を振動させる（0.5秒程度の振動が1回だけ行われる）
					if (SystemInfo.supportsVibration)
					{
						Handheld.Vibrate();
					}
#endif//#if UNITY_ANDROID

					//スコア
					score.AddScore(scoreNum);
					rankingScore.AddBestScore(scoreNum);

					if (hitScoreTextPrefab != null)
					{
						//HitScoreText();
						StartCoroutine(HitScoreText());
					}

					if (hitEffectPrefab != null && hitHinokoEffectPrefab != null)
					{
						//ヒットエフェクト
						HitEffect();
					}

					if (hitSEPrefab != null)
					{
						HitSE();
					}


					//Destroy(this.gameObject);
				}
			}
		}

		// 		private void OnTriggerStay(Collider hit)
		// 		{
		// 			if (hit.CompareTag("Cube_Saber") || hit.CompareTag("Cube_Saber_Tip") && !isOneHitActive)//メッシュカットテスト用
		// 																									//if (hit.CompareTag("Cube_Saber_Tip") && isOneHitActive == false && cube_Saber.Get_b_SaverOneHitActive() == true)//使わないやつ
		// 																									//if (hit.CompareTag("Cube_Saber_Tip") && !isOneHitActive)//本番環境用
		// 			{
		// 				//Debug.Log("Cube_Saberに接触したよ");
		// 				isOneHitActive = true;
		// 				isMoveActive = false;

		// #if UNITY_ANDROID//Androidプラットフォームの時のみ起動する 
		// 				//android端末を振動させる（0.5秒程度の振動が1回だけ行われる）
		// 				if (SystemInfo.supportsVibration)
		// 				{
		// 					Handheld.Vibrate();
		// 				}
		// #endif//#if UNITY_ANDROID

		// 				//スコア
		// 				score.AddScore(scoreNum);

		// 				if (hitScoreTextPrefab != null)
		// 				{
		// 					HitScoreText();
		// 				}

		// 				if (hitEffectPrefab != null)
		// 				{
		// 					//ヒットエフェクト
		// 					HitEffect();
		// 				}

		// 				if (hitSEPrefab != null)
		// 				{
		// 					HitSE();
		// 				}


		// 				//Destroy(this.gameObject);
		// 			}
		// 		}

		IEnumerator StartMaterialColor()
		{
			if (1000 <= scoreNum)
			{
				yield return GetComponent<Renderer>().material.color = new Color32(255, 69, 74, 200);
			}
			else if (500 <= scoreNum)
			{
				yield return GetComponent<Renderer>().material.color = new Color32(235, 97, 0, 200);
			}
			else if (100 <= scoreNum)
			{
				yield return GetComponent<Renderer>().material.color = new Color32(173, 0, 45, 200);
			}
			else
			{
				yield return GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 200);
			}
		}

		void StartMove()
		{
			//左の目的座標
			destinationPosLeftX = moveLeftLength + transform.position.x;
			//右の目的座標
			destinationPosRightX = moveRightLength + transform.position.x;
			//現在の座標
			currentPosX = transform.position.x;
		}

		void UpdateMove()
		{
			if (!isMoveActive)
			{
				return;
			}

			if (!isStartFromTheMoveRight)
			{
				if (destinationPosLeftX < currentPosX)
				{
					currentPosX = transform.position.x - (moveSpeed * Time.deltaTime);
					transform.position = new Vector3(currentPosX, transform.position.y, transform.position.z);
				}
				else if (currentPosX <= destinationPosLeftX)
				{
					isStartFromTheMoveRight = true;
				}

			}
			else if (isStartFromTheMoveRight)
			{
				if (currentPosX < destinationPosRightX)
				{
					currentPosX = transform.position.x + (moveSpeed * Time.deltaTime);
					transform.position = new Vector3(currentPosX, transform.position.y, transform.position.z);
				}
				else if (destinationPosRightX <= currentPosX)
				{
					isStartFromTheMoveRight = false;
				}
			}

		}

		void HitEffect()
		{
			GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
			Destroy(hitEffect, hitEffectPrefabDestroyTime);

			Quaternion rotHinoko = Quaternion.AngleAxis(180, Vector3.up);
			GameObject hitHinokoEffect = Instantiate(hitHinokoEffectPrefab, cube_Effect.transform.position, rotHinoko);
			Destroy(hitHinokoEffect, hitHinokoEffectPrefabDestroyTime);
		}

		void HitSE()
		{
			GameObject hitSE = Instantiate(hitSEPrefab, transform.position, Quaternion.identity);
			Destroy(hitSE, hitSEPrefabDestroyTime);
		}

		IEnumerator HitScoreText(float WaitForSeconds = 0.0f)
		{
			hitScoreTextPrefab.GetComponent<TextMeshPro>().text = "+" + scoreNum.ToString();

			// Vector3.right（x軸を指定したい場合）
			// x軸を軸にして90度、回転させるQuaternionを作成（変数をrotとする）
			Quaternion rot = Quaternion.AngleAxis(90, Vector3.right);

			float startFromTheMoveRight = 1;

			if (!isStartFromTheMoveRight)
			{
				//スコアUIを右上に表示する
				startFromTheMoveRight = 1;
			}
			else if (isStartFromTheMoveRight)
			{
				//スコアUIを左上に表示する
				startFromTheMoveRight = -1;
			}

			float cubePositionX = transform.position.x + (2.0f * startFromTheMoveRight);
			float cubePositionY = transform.position.y;
			float cubePositionZ = transform.position.z + 2.0f;
			Vector3 pos = new Vector3(cubePositionX, cubePositionY, cubePositionZ);

			GameObject hitScoreText = Instantiate(hitScoreTextPrefab, pos, rot);

			yield return new WaitForSeconds(WaitForSeconds);
		}
	}
}