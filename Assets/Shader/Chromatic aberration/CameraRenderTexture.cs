using UnityEngine;

[ExecuteInEditMode]
public class CameraRenderTexture : MonoBehaviour
{
	public Material Mat;

	public bool isCameraRenderActive = false;

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, Mat);
	}
}