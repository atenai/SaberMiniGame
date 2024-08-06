using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvScroll : MonoBehaviour
{
	[SerializeField] float scrollSpeed_u = 0.5f;
	[SerializeField] float scrollSpeed_v = 0.5f;

	Renderer renderer;
	IEnumerator Start()
	{
		renderer = GetComponent<Renderer>();
		yield return null;
	}

	// Update is called once per frame
	void Update()
	{
		float offset_u = Time.time - scrollSpeed_u;
		float offset_v = Time.time - scrollSpeed_v;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offset_u, offset_v));
	}
}
