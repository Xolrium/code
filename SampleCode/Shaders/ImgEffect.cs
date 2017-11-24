/* Script made by: Ernesto Hernandez 
		Portfolio http://ernestohh.weebly.com/
												*/


using UnityEngine;

public class ImgEffect : MonoBehaviour
{
	public Material EffectMaterial;

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, EffectMaterial);
	}
}
