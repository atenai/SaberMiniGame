using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	//public CameraShake shake;

	void Update()
	{
		// if (Input.GetKeyDown(KeyCode.Z))
		// {
		// 	//shake.Shake(0.25f, 0.1f);
		// 	Shake(0.25f, 0.1f);
		// }
	}

	//Shake(揺れる時間,揺れる幅)
	public void Shake(float duration, float magnitude)
	{
		StartCoroutine(DoShake(duration, magnitude));
	}

	private IEnumerator DoShake(float duration, float magnitude)
	{
		//揺れる前の座標を入れる
		var pos = transform.localPosition;

		var elapsed = 0f;

		//引数で設定した揺れる時間より小さいなら中身を実行して揺らす
		while (elapsed < duration)
		{
			var x = pos.x + Random.Range(-1f, 1f) * magnitude;
			var y = pos.y + Random.Range(-1f, 1f) * magnitude;
			var z = pos.z + Random.Range(-1f, 1f) * magnitude;

			//揺れた座標を入れる
			transform.localPosition = new Vector3(x, pos.y, z);

			//時間を進める
			elapsed += Time.deltaTime;

			yield return null;
		}

		//揺れる前の座標を入れる
		transform.localPosition = pos;
	}
}
