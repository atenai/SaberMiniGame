using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dico.HyperCasualGame.Minigame
{
	public class UILineRenderer : MonoBehaviour
	{
		[SerializeField] GameObject cube_Saber;

		[SerializeField] GameObject UI;

		LineRenderer renderer;
		IEnumerator Start()
		{
			renderer = gameObject.GetComponent<LineRenderer>();
			// 線の幅
			renderer.SetWidth(0.1f, 0.1f);
			// 頂点の数
			renderer.SetVertexCount(2);
			yield return null;
		}

		void Update()
		{
			// 頂点を設定
			Vector3 saber = cube_Saber.transform.position;
			renderer.SetPosition(0, saber);

			Vector3 ui = UI.transform.position;
			renderer.SetPosition(1, ui);
		}
	}
}