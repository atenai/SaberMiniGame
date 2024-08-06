using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
	// 変数
	int frameCount;
	float prevTime;
	float fps;

	void Start()
	{
		// 変数の初期化
		frameCount = 0;
		prevTime = 0.0f;
	}

	void Update()
	{
		frameCount++;
		float time = Time.realtimeSinceStartup - prevTime;

		if (time >= 0.5f)
		{
			fps = frameCount / time;
			//Debug.Log(fps);

			frameCount = 0;
			prevTime = Time.realtimeSinceStartup;
		}
	}

	//OnGUIという関数はGUIを表示させる処理を書き込む関数です。
	//GUIとはグラフィックユーザーインターフェースのことで、ボタンやラベルなどユーザーに見える部分のことを指します。
	//OnGUIという関数はUpdate関数同様、毎フレームごとに呼ばれる関数ですので注意してください。
	//今回はOnGUI関数の中に GUILayout.Label関数を使うことでラベルを画面上に表示させています。
	//GUILayout.Label関数は引数にとったString型の変数をラベルに表示させる関数です。
	// 表示処理
	private void OnGUI()
	{
		//文字を大きくする処理
		GUI.skin.label.fontSize = 10;
		//fpsの数値をstring型にしてUI表示する
		GUILayout.Label("FPS:" + fps.ToString());
	}
}
