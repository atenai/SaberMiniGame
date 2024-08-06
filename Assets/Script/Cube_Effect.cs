using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dico.HyperCasualGame.Minigame
{
	public class Cube_Effect : MonoBehaviour
	{
		//多重当たり判定を消す
		bool isOneHitActive = false;
		public bool IsOneHitActive
		{
			get
			{
				return isOneHitActive;
			}
			private set
			{
				isOneHitActive = value;
			}
		}
		[SerializeField]
		BoxCollider boxCollider;

		//エフェクト
		[SerializeField] float particleEffectDestroyTime = 1.0f;
		//エフェクトのモデルチェンジ
		//エフェクトのプレファブを入れる配列
		[SerializeField] GameObject[] cubeArray;
		int effectNumber = 0;
		bool isOneEffectActive = false;

		[SerializeField] Cube_Saber cubeSaber;

		bool isFadeActive = false;
		public bool IsFadeActive
		{
			get
			{
				return isFadeActive;
			}
			private set
			{
				isFadeActive = value;
			}
		}

		//カメラ
		[SerializeField] CameraShake cameraShake;

		//スコア
		[SerializeField] Score score;
		[SerializeField] Shop shop;

		//呼ばれる順番
		//Awake
		//↓
		//OnEnable
		//↓
		//Start

		void Awake()
		{
			//保存データが存在するか?
			if (PlayerPrefs.HasKey("SaveeffectNumber"))
			{
				// 存在する
				Debug.Log("エフェクトのセーブデータがあります。");
			}
			else
			{
				// 存在しない
				Debug.Log("エフェクトのセーブデータがありません。");
			}

			//ロードシステム
			//スコアのロード
			//PlayerPrefs.GetInt関数は「第一引数に指定した名前で保存されている値」を戻り値として取得する関数です。
			//第二引数は、もし保存されていなかった場合、何を返すかの設定になります。
			effectNumber = PlayerPrefs.GetInt("SaveeffectNumber", 0);
			Debug.Log("effectNumber" + effectNumber);
		}

		void Update()
		{
			//EffectChange();
			EffectInstance();

			ResultFadeStart();
		}

		//void OnTriggerEnter(Collider hit)
		//{
		//    //Debug.Log("接触したよ");

		//    if (hit.CompareTag("Cube_Enemy") && !isOneHitActive)
		//    {
		//        isOneHitActive = true;
		//        //このオブジェクトのコライダーをOFFにしないと多重当たり判定のバグが発生してしまう
		//        boxCollider.enabled = false;
		//        Debug.Log("boxCollider.enabled : " + boxCollider.enabled);
		//        //Debug.Log("エネミーに当たったよ");
		//        cubeSaber.SetisSaberRotFalse();
		//        cubeSaber.SetisSaberMoveFalse();
		//    }
		//}

		private void OnTriggerStay(Collider hit)
		{
			if (hit.CompareTag("Cube_Enemy") && !isOneHitActive)
			{
				IsOneHitActive = true;
				//このオブジェクトのコライダーをOFFにしないと多重当たり判定のバグが発生してしまう
				boxCollider.enabled = false;
				//Debug.Log("boxCollider.enabled : " + boxCollider.enabled);
				//Debug.Log("エネミーに当たったよ");
				cubeSaber.SetisSaberRotFalse();
				cubeSaber.SetisSaberMoveFalse();
				if (cameraShake != null)
				{
					cameraShake.Shake(0.25f, 0.1f);
				}
			}
		}

		private void EffectInstance()
		{
			if (cubeSaber.IsOneClick && !isOneHitActive && !isOneEffectActive)
			{
				isOneEffectActive = true;
				// Vector3.right（x軸を指定したい場合）
				// x軸を軸にして90度、回転させるQuaternionを作成（変数をrotとする）
				Quaternion rot = Quaternion.AngleAxis(90, Vector3.right);
				var parent = transform;
				var particleEffect = Instantiate(cubeArray[effectNumber], transform.position, rot, parent);
				//Destroy(particleEffect, particleEffectDestroyTime);
			}
		}

		private void EffectChange()
		{
			if (cubeSaber.IsEffectChange)
			{
				if (!cubeSaber.IsOneClick)
				{
					effectNumber++;
					if (effectNumber == cubeArray.Length)
					{
						effectNumber = 0;
					}
				}
				cubeSaber.SetIsEffectChangeFalse();
			}
		}

		public void EffectChangeButton0()
		{
			if (!cubeSaber.IsOneClick)
			{
				if (100 <= score.GettotalScoreNum())
				{
					if (effectNumber != 0)
					{
						shop.EffectColorChangeButton0();
						score.SubScore(100);
						effectNumber = 0;
					}
					else
					{
						Debug.Log("同じエフェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		public void EffectChangeButton1()
		{
			if (!cubeSaber.IsOneClick)
			{
				if (500 <= score.GettotalScoreNum())
				{
					if (effectNumber != 1)
					{
						shop.EffectColorChangeButton1();
						score.SubScore(500);
						effectNumber = 1;
					}
					else
					{
						Debug.Log("同じエフェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		public void EffectChangeButton2()
		{
			if (!cubeSaber.IsOneClick)
			{
				if (1000 <= score.GettotalScoreNum())
				{
					if (effectNumber != 2)
					{
						shop.EffectColorChangeButton2();
						score.SubScore(1000);
						effectNumber = 2;
					}
					else
					{
						Debug.Log("同じエフェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		private void ResultFadeStart()
		{
			if (isOneHitActive)
			{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN//UnityEditorの時とWindowsスタンドアロンアプリケーションの時のみ起動する 
				if (Input.GetMouseButtonDown(0))
				{
					IsFadeActive = true;
				}
#endif//#if UNITY_EDITOR || UNITY_STANDALONE_WIN 
#if UNITY_ANDROID//Androidプラットフォームの時のみ起動する 
				if (Input.GetMouseButtonUp(0))
				{
					IsFadeActive = true;
				}
#endif//#if UNITY_ANDROID
			}
		}

		// 削除時の処理
		void OnDestroy()
		{
			//セーブシステム
			// スコアを保存
			//PlayerPrefs.SetInt関数を使用してスコアを保存しています。
			//第一引数に保存するデータの名前(これをロード時指定することになります)、第二引数に保存する数値を入れているわけですね。
			//(保存名,スコア変数score_num)
			PlayerPrefs.SetInt("SaveeffectNumber", effectNumber);
			//一つ注意が必要なのは、PlayerPrefsへ保存データの設定が一通り終わったら、からなず最後にPlayerPrefs.Save関数実行する必要があることです。
			//なぜならPlayerPrefs.Save関数が呼び出された時に、PlayerPrefsに登録されている数値を実際に保存しているからです。
			PlayerPrefs.Save();
		}

		public int GeteffectNumber()
		{
			return effectNumber;
		}
	}
}