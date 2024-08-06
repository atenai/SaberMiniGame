using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dico.HyperCasualGame.Minigame
{
	public class Score : MonoBehaviour
	{

		[SerializeField] Text scoreText;
		[SerializeField] TextMesh scoreTextMesh;

		int totalScoreNum = 0;

		IEnumerator Start()
		{
			//保存データが存在するか?
			if (PlayerPrefs.HasKey("SaveTotalScoreNum"))
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
			totalScoreNum = PlayerPrefs.GetInt("SaveTotalScoreNum", 0);



			yield return StartCoroutine(ScoreTMP());
		}

		void Update()
		{
			StartCoroutine(ScoreTMP());
		}

		// 削除時の処理
		void OnDestroy()
		{
			//セーブシステム
			// スコアを保存
			//PlayerPrefs.SetInt関数を使用してスコアを保存しています。
			//第一引数に保存するデータの名前(これをロード時指定することになります)、第二引数に保存する数値を入れているわけですね。
			//(保存名,スコア変数score_num)
			PlayerPrefs.SetInt("SaveTotalScoreNum", totalScoreNum);
			//一つ注意が必要なのは、PlayerPrefsへ保存データの設定が一通り終わったら、からなず最後にPlayerPrefs.Save関数実行する必要があることです。
			//なぜならPlayerPrefs.Save関数が呼び出された時に、PlayerPrefsに登録されている数値を実際に保存しているからです。
			PlayerPrefs.Save();
		}

		public void AddScore(int num)
		{
			totalScoreNum += num;
		}

		public void SubScore(int num)
		{
			totalScoreNum -= num;
		}

		public int GettotalScoreNum()
		{
			return totalScoreNum;
		}

		IEnumerator ScoreTMP(float WaitForSeconds = 0.0f)
		{
			if (scoreText != null)
			{
				yield return scoreText.text = "Score : " + totalScoreNum.ToString();
			}
			if (scoreTextMesh != null)
			{
				yield return scoreTextMesh.text = "Score : " + totalScoreNum.ToString();
			}
		}
	}
}