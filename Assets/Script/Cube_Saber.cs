using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dico.HyperCasualGame.Minigame
{
	public class Cube_Saber : MonoBehaviour
	{
		//あたり判定系
		//多重当たり判定を消す
		bool isSaberOneHitActive = false;
		public bool IsSaberOneHitActive
		{
			get
			{
				return isSaberOneHitActive;
			}
			private set
			{
				isSaberOneHitActive = value;
			}
		}

		[SerializeField]
		BoxCollider boxCollider;

		bool isOneClick = false;
		public bool IsOneClick
		{
			get
			{
				return isOneClick;
			}
			private set
			{
				isOneClick = value;
			}
		}

		bool isEffectChange = false;
		public bool IsEffectChange
		{
			get
			{
				return isEffectChange;
			}
			private set
			{
				isEffectChange = value;
			}
		}

		//回転
		bool isSaberRot = false;
		[SerializeField]//変数をprivateにしてもインスペクターから数値を変更する事ができるようにする
		float saberRotate = 5000.0f;

		//移動
		bool isSaberMove = false;
		[SerializeField]
		float saberTransrateZ = 50.0f;

		//筒
		[SerializeField]
		Cube_Cylinder cubeCylinder;

		//剣のモデルチェンジ
		//剣のプレファブを入れる配列
		[SerializeField]
		GameObject[] cubeArray;
		int gameObjectNumber = 0;
		GameObject cubeObj;

		//フリック操作
		private Vector3 touchStartPos;
		private Vector3 touchEndPos;

		//スラッシュSE
		AudioSource audioSource;
		bool isSlashSEActive = false;

		bool isFlick = true;
		public bool IsFlick
		{
			get
			{
				return isFlick;
			}
			private set
			{
				isFlick = value;
			}
		}

		float flickTrueTimeConstant = 0.25f;
		float flickTrueTimeCount;

		//スコア
		[SerializeField] Score score;

		[SerializeField] Shop shop;


		void Awake()
		{
			//保存データが存在するか?
			if (PlayerPrefs.HasKey("SavegameObjectNumber"))
			{
				// 存在する
				Debug.Log("セーブデータがあります。");
			}
			else
			{
				// 存在しない
				Debug.Log("セーブデータがありません。");
			}

			//ロードシステム
			//スコアのロード
			//PlayerPrefs.GetInt関数は「第一引数に指定した名前で保存されている値」を戻り値として取得する関数です。
			//第二引数は、もし保存されていなかった場合、何を返すかの設定になります。
			gameObjectNumber = PlayerPrefs.GetInt("SavegameObjectNumber", 0);
		}

		void Start()
		{
			boxCollider = GetComponent<BoxCollider>();
			audioSource = GetComponent<AudioSource>();
			ObjectChangeStart();
			flickTrueTimeCount = flickTrueTimeConstant;
		}

		void Update()
		{
			flickTrueTimeCount = flickTrueTimeCount + Time.deltaTime;
			if (isFlick && flickTrueTimeConstant <= flickTrueTimeCount)
			{
				Flick();
			}

			SaberRotate();

			//Debug.Log(cubeObj);
			//Debug.Log(cubeArray[0].gameObject);
		}

		void LateUpdate()
		{
			SaberMove();
		}

		private void SaberTakeAction()
		{
			if (!IsOneClick)
			{
				IsOneClick = true;
				isSaberRot = true;
				isSaberMove = true;
			}
		}

		private void SaberRotate()
		{
			if (isSaberRot && !cubeCylinder.IsHitCylinder)
			{
				float rotateY = -saberRotate * Time.deltaTime;
				transform.Rotate(0, rotateY, 0);

				if (isSlashSEActive == false)
				{
					SlashSEStart();
					isSlashSEActive = true;
				}
			}
		}

		private void SaberMove()
		{
			if (isSaberMove)
			{
				// 下から上に移動
				transform.position += new Vector3(0, 0, saberTransrateZ) * Time.deltaTime;
			}
		}

		void OnTriggerEnter(Collider hit)
		{
			//Debug.Log("接触したよ");

			if (hit.CompareTag("Cube_Enemy") && !IsSaberOneHitActive)
			{
				IsSaberOneHitActive = true;
				//boxCollider.enabled = false;
				//Debug.Log("boxCollider.enabled : " + boxCollider.enabled);
				//b_SaberRot = false;
				//b_SaberMove = false;
				SlashSEEnd();
			}
		}

		void ObjectChangeStart()
		{
			//セイバーのモデルチェンジ
			var parent = transform;
			cubeObj = Instantiate(cubeArray[gameObjectNumber], parent.transform.position, parent.transform.rotation, parent);
		}

		public void ObjectChangeUpdate()
		{
			if (!IsOneClick)
			{
				var parent = transform;
				Destroy(cubeObj);
				gameObjectNumber++;
				cubeObj = Instantiate(cubeArray[gameObjectNumber], parent.transform.position, parent.transform.rotation, parent);
				//3つあるなら配列は0番目から始まる為(0,1,2)だがArray.Lengthは配列の数を出す為(配列数3つ)となる為、Array.Length - 1をしないといけない
				if (gameObjectNumber == cubeArray.Length - 1)
				{
					gameObjectNumber = -1;
				}
			}
		}

		public void SaberObjectChangeButton0()
		{
			if (!IsOneClick)
			{
				if (100 <= score.GettotalScoreNum())
				{
					if (gameObjectNumber != 0)
					{
						shop.SaberColorChangeButton0();
						score.SubScore(100);
						var parent = transform;
						Destroy(cubeObj);
						gameObjectNumber = 0;
						cubeObj = Instantiate(cubeArray[gameObjectNumber], parent.transform.position, parent.transform.rotation, parent);
						Debug.Log("オブジェクト変更だよ！");
					}
					else
					{
						Debug.Log("同じオブジェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		public void SaberObjectChangeButton1()
		{
			if (!IsOneClick)
			{
				if (500 <= score.GettotalScoreNum())
				{
					if (gameObjectNumber != 1)
					{
						shop.SaberColorChangeButton1();
						score.SubScore(500);
						var parent = transform;
						Destroy(cubeObj);
						gameObjectNumber = 1;
						cubeObj = Instantiate(cubeArray[gameObjectNumber], parent.transform.position, parent.transform.rotation, parent);
					}
					else
					{
						Debug.Log("同じオブジェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		public void SaberObjectChangeButton2()
		{
			if (!IsOneClick)
			{
				if (2000 <= score.GettotalScoreNum())
				{
					if (gameObjectNumber != 2)
					{
						shop.SaberColorChangeButton2();
						score.SubScore(2000);
						var parent = transform;
						Destroy(cubeObj);
						gameObjectNumber = 2;
						cubeObj = Instantiate(cubeArray[gameObjectNumber], parent.transform.position, parent.transform.rotation, parent);
					}
					else
					{
						Debug.Log("同じオブジェクトだよ！");
					}
				}
				else
				{
					Debug.Log("お金がたりないよ！");
				}
			}
		}

		public void SetisSaberRotFalse()
		{
			isSaberRot = false;
		}

		public void SetisSaberMoveFalse()
		{
			isSaberMove = false;
		}

		public void SetIsEffectChangeFalse()
		{
			IsEffectChange = false;
		}

		public void SetIsFlickTrue()
		{
			IsFlick = true;
			flickTrueTimeCount = 0.0f;
		}

		public void SetIsFlickFalse()
		{
			IsFlick = false;
		}

		void Flick()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			}

			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
				GetDirection();
			}
		}

		void GetDirection()
		{
			float directionX = touchEndPos.x - touchStartPos.x;
			float directionY = touchEndPos.y - touchStartPos.y;
			string Direction = "up";

			if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
			{
				if (30 < directionX)
				{
					//右向きにフリック
					Direction = "right";
				}
				else if (-30 > directionX)
				{
					//左向きにフリック
					Direction = "left";
				}
			}
			else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
			{
				if (30 < directionY)
				{
					//上向きにフリック
					Direction = "up";
				}
				else if (-30 > directionY)
				{
					//下向きのフリック
					Direction = "down";
				}
			}
			else
			{
				//タッチを検出
				Direction = "touch";
			}

			switch (Direction)
			{
				case "up":
					//上フリックされた時の処理
					SaberTakeAction();
					break;

				case "down":
					//下フリックされた時の処理
					break;

				case "right":
					//右フリックされた時の処理
					//IsEffectChange = true;
					break;

				case "left":
					//左フリックされた時の処理
					//ObjectChangeUpdate();
					break;

				case "touch":
					//タッチされた時の処理
					break;
			}
		}

		void SlashSEStart()
		{
			if (audioSource != null)
			{
				audioSource.Play();
			}
		}

		void SlashSEEnd()
		{
			if (audioSource != null)
			{
				audioSource.Stop();
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
			PlayerPrefs.SetInt("SavegameObjectNumber", gameObjectNumber);
			//一つ注意が必要なのは、PlayerPrefsへ保存データの設定が一通り終わったら、からなず最後にPlayerPrefs.Save関数実行する必要があることです。
			//なぜならPlayerPrefs.Save関数が呼び出された時に、PlayerPrefsに登録されている数値を実際に保存しているからです。
			PlayerPrefs.Save();
		}

		public int GetgameObjectNumber()
		{
			return gameObjectNumber;
		}
	}
}
