using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapParticle : MonoBehaviour
{
	public GameObject prefab;
	public float deleteTime = 1.0f;
	[SerializeField] Camera cam;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//マウスカーソルの位置を取得。
			var mousePosition = Input.mousePosition;
			mousePosition.z = 3.0f;
			GameObject clone = Instantiate(prefab, cam.ScreenToWorldPoint(mousePosition), Quaternion.identity);
			Destroy(clone, deleteTime);
		}
	}
}
