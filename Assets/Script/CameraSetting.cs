using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
	[Tooltip("画角（Field of View）の値はアスペクト比によって変更されます。カメラのFOVを自動で制御しないと、縦長の端末で左右が切れます。縦長の端末ほど、視界が狭くなりゲーム体験が悪くなります。")]
	[SerializeField] float hFov = 35;

	public Camera Camera { get => cameraComponent; }

	Camera cameraComponent;

	void Awake()
	{
		cameraComponent = GetComponentInChildren<Camera>();

		cameraComponent.fieldOfView = hFov / ((float)cameraComponent.pixelWidth / cameraComponent.pixelHeight);
	}

	void LateUpdate()
	{
		cameraComponent.fieldOfView = hFov / ((float)cameraComponent.pixelWidth / cameraComponent.pixelHeight);
	}
}
