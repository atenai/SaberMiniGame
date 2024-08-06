using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//https://nopitech.com/2019/05/22/post-1230/
public class LoadSceneManager : MonoBehaviour
{
	[SerializeField] GameObject panel_Background;
	[SerializeField] Slider slider;

	//タップエフェクトのゲームオブジェクト
	[SerializeField] GameObject gameObject_TapEffect;
	float sliderTimeCount = 0.0f;
	//[SerializeField] GameObject titleSubCamera;
	[SerializeField] GameObject titleCanvas;

	public void NextScene()
	{
		// コルーチンでロード画面を実行
		StartCoroutine("LoadScene");
	}
	IEnumerator LoadScene()
	{
		if (gameObject_TapEffect != null)
		{
			gameObject_TapEffect.SetActive(false);
			yield return new WaitForSeconds(0.5f);// 1秒だけ表示維持
		}

		// スライダーの値更新とロード画面の表示
		slider.value = 0f;//UIのスライダーの値を初期化
		sliderTimeCount = 0.0f;
		panel_Background.SetActive(true);//バックグラウンドのパネルを表示

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("3DMiniGameScene");//ゲームシーンをロード

		//LoadSceneMode.Additiveは現在のゲームシーンにロードした次のシーンをヒエラルキーに追加する処理(正解)
		//AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("3DMiniGameScene", LoadSceneMode.Additive);
		asyncLoad.allowSceneActivation = false;// ロードが完了していても，シーンのアクティブ化は許可しない

		while (true)
		{
			if (asyncLoad.progress < 0.9f && slider.value < 0.9f)
			//if (slider.value < 0.9f)
			{
				sliderTimeCount = (sliderTimeCount + Time.deltaTime) * 1.0f;
				slider.value = sliderTimeCount;
				Debug.Log("<color=red>sliderTimeCount:" + this.sliderTimeCount + "</color>");
				Debug.Log("<color=blue>slider.value:" + this.slider.value + "</color>");
				yield return new WaitForSeconds(0.01f);
			}

			if (asyncLoad.progress < 0.9f && 0.9f <= slider.value)
			{
				slider.value = 0.9f;
				yield return null;//一旦処理を中断して次のフレームで再開する
			}

			if (0.9f <= asyncLoad.progress)//シーン読み込みが完了したら(0.9f以上になったら)中身を実行する
										   //if (0.9f <= asyncLoad.progress && 0.9f <= slider.value)//シーン読み込みが完了したら(0.9f以上になったら)中身を実行する
			{
				slider.value = 1f;//UIのスライダーを1(max)にする

				asyncLoad.allowSceneActivation = true;// ロードが完了したときにシーンのアクティブ化を許可する

				//LoadSceneMode.Additiveを使用する場合に使う
				//titleSubCamera.SetActive(false);
				//titleCanvas.SetActive(false);
				//Destroy(titleSubCamera);

				yield return new WaitForSeconds(0.01f);// ロードバーが100%になっても1秒だけ表示維持
				break;//whileループから抜ける
			}
		}
	}

}
