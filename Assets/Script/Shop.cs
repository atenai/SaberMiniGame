using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dico.HyperCasualGame.Minigame
{
	public class Shop : MonoBehaviour
	{
		[SerializeField] GameObject Panel_Shop;
		[SerializeField] Cube_Saber cube_Saber;

		[SerializeField] GameObject Button_Saber0;
		[SerializeField] GameObject Button_Saber1;
		[SerializeField] GameObject Button_Saber2;


		[SerializeField] Cube_Effect cube_Effect;
		[SerializeField] GameObject Button_Effect0;
		[SerializeField] GameObject Button_Effect1;
		[SerializeField] GameObject Button_Effect2;

		void Start()
		{
			Panel_Shop.SetActive(false);
			StartSaberColorChangeButton();
			StartEffectColorChangeButton();
		}

		public void ShopActiveTrue()
		{
			Panel_Shop.SetActive(true);
			cube_Saber.SetIsFlickFalse();
		}

		public void ShopActiveFalse()
		{
			Panel_Shop.SetActive(false);
			cube_Saber.SetIsFlickTrue();
		}

		public void StartSaberColorChangeButton()
		{
			Debug.Log("オブジェクトの色の初期化！");
			Debug.Log("GetgameObjectNumber" + cube_Saber.GetgameObjectNumber());
			// Button_Saber0.GetComponent<Image>().color = Color.cyan;
			// Button_Saber1.GetComponent<Image>().color = Color.white;
			// Button_Saber2.GetComponent<Image>().color = Color.white;
			if (cube_Saber.GetgameObjectNumber() == 0)
			{
				SaberColorChangeButton0();
			}
			else if (cube_Saber.GetgameObjectNumber() == 1)
			{
				SaberColorChangeButton1();
			}
			else if (cube_Saber.GetgameObjectNumber() == 2)
			{
				SaberColorChangeButton2();
			}
		}

		public void SaberColorChangeButton0()
		{
			Debug.Log("オブジェクトの色の変更！");
			Button_Saber0.GetComponent<Image>().color = Color.cyan;
			Button_Saber1.GetComponent<Image>().color = Color.white;
			Button_Saber2.GetComponent<Image>().color = Color.white;
		}

		public void SaberColorChangeButton1()
		{
			Debug.Log("オブジェクトの色の変更！");
			Button_Saber0.GetComponent<Image>().color = Color.white;
			Button_Saber1.GetComponent<Image>().color = Color.cyan;
			Button_Saber2.GetComponent<Image>().color = Color.white;
		}

		public void SaberColorChangeButton2()
		{
			Debug.Log("オブジェクトの色の変更！");
			Button_Saber0.GetComponent<Image>().color = Color.white;
			Button_Saber1.GetComponent<Image>().color = Color.white;
			Button_Saber2.GetComponent<Image>().color = Color.cyan;
		}

		public void StartEffectColorChangeButton()
		{
			Debug.Log("エフェクトの色の初期化！");
			Debug.Log("GeteffectNumber" + cube_Effect.GeteffectNumber());
			// Button_Effect0.GetComponent<Image>().color = Color.cyan;
			// Button_Effect1.GetComponent<Image>().color = Color.white;
			// Button_Effect2.GetComponent<Image>().color = Color.white;
			if (cube_Effect.GeteffectNumber() == 0)
			{
				EffectColorChangeButton0();
			}
			else if (cube_Effect.GeteffectNumber() == 1)
			{
				EffectColorChangeButton1();
			}
			else if (cube_Effect.GeteffectNumber() == 2)
			{
				EffectColorChangeButton2();
			}
		}

		public void EffectColorChangeButton0()
		{
			Debug.Log("エフェクトの色の変更！");
			Button_Effect0.GetComponent<Image>().color = Color.cyan;
			Button_Effect1.GetComponent<Image>().color = Color.white;
			Button_Effect2.GetComponent<Image>().color = Color.white;
		}

		public void EffectColorChangeButton1()
		{
			Debug.Log("エフェクトの色の変更！");
			Button_Effect0.GetComponent<Image>().color = Color.white;
			Button_Effect1.GetComponent<Image>().color = Color.cyan;
			Button_Effect2.GetComponent<Image>().color = Color.white;
		}

		public void EffectColorChangeButton2()
		{
			Debug.Log("エフェクトの色の変更！");
			Button_Effect0.GetComponent<Image>().color = Color.white;
			Button_Effect1.GetComponent<Image>().color = Color.white;
			Button_Effect2.GetComponent<Image>().color = Color.cyan;
		}
	}
}