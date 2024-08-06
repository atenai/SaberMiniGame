using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;//JsonUtilityを使用する為にファイルの読み書きをする必要がある為
public class RankingScore : MonoBehaviour
{
	[SerializeField] Text bestScoreText;
	public int bestScore;

	int currentScore;

	//データロードする仕組み
	void Start()
	{
		//Application.persistentDataPathの場所は、Windowsだと\usr\AppData\LocalLow\CompanyNameになります。
		string path = Application.persistentDataPath + "/BestScoreSaveFile.json";
		//ファイルの有無を確認する、有ればtrue,無ければfalse
		if (File.Exists(path))
		{
			//Application.persistentDataPathのsavefile.jsonファイルをFile.ReadAllTextで読み込みます。
			string json = File.ReadAllText(path);
			//読み込んだJSON形式のデータをJsonUtility.FromJson<>()でSaveDataクラスの形式に変換します。
			RankingScoreSaveData data = JsonUtility.FromJson<RankingScoreSaveData>(json);// JSON形式から変換

			bestScore = data.bestScore;
		}
		else
		{
			bestScore = 0;
		}

		bestScoreText.text = "BestScore: " + bestScore;

		currentScore = 0;
	}

	//データセーブする仕組み
	public void AddBestScore(int num)
	{
		currentScore += num;

		//現在のスコアがベストスコアより高ければ中身を実行する
		if (bestScore < currentScore)
		{
			bestScoreText.text = "BestScore: " + currentScore;

			RankingScoreSaveData data = new RankingScoreSaveData();//JSON形式のクラスをインスタンス
			data.bestScore = currentScore;

			string json = JsonUtility.ToJson(data);// JSON形式へ変換

			//File.WriteAllText()は第1引数で指定されたファイルに第2引数で指定された変数を書き込みます。
			//Application.persistentDataPathの場所は、Windowsだと\usr\AppData\LocalLow\CompanyNameになります。
			//今回の場合はc/ユーザー/AppData/LocalLow/DefaultCompany/Saber3DMiniGameの中のsavefile.jsonになる
			File.WriteAllText(Application.persistentDataPath + "/BestScoreSaveFile.json", json);
		}
	}
}

//JSON形式でセーブするデータを格納するクラスを作成します。
//クラスの前に記述した[System.Serializable]は、クラスデータをJSON形式に変換する際に必要となります。
[System.Serializable]//定義したクラスをJSONデータに変換できるようにする
class RankingScoreSaveData
{
	public int bestScore;
}