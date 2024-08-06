using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dico.HyperCasualGame.Minigame
{
	public class HitFade : Fade
	{
		bool isAlphaMax = false;

		void Start()
		{
			colorR = fade.color.r;
			colorG = fade.color.g;
			colorB = fade.color.b;
		}

		void Update()
		{
			if (cube_Effect.IsOneHitActive && !isAlphaMax)
			{
				fade.color = new Color(colorR, colorG, colorB, alpha);
				alpha += speed * Time.deltaTime;
			}

			if (0.8 <= alpha && !isAlphaMax)
			{
				isAlphaMax = true;
			}

			if (isAlphaMax)
			{
				fade.color = new Color(colorR, colorG, colorB, alpha);
				alpha -= speed * Time.deltaTime;
			}
		}
	}
}
