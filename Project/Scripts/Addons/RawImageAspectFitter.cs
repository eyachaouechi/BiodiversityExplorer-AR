using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RawImageAspectFitter : Singleton<RawImageAspectFitter>
{
	[SerializeField] bool m_adjustOnStart = true;

	protected RawImage m_image;
	protected float m_aspectRatio = 1.0f;
	protected float m_rectAspectRatio = 1.0f;

    private void Start()
    {
		AdjustAspect();
	}

    void SetupImage()
	{
		m_image = GetComponent<RawImage>();
		
		//m_image.SizeToParent();
		CalculateImageAspectRatio();
		CalculateTextureAspectRatio();

	}

	void CalculateImageAspectRatio()
	{
		RectTransform rt = transform as RectTransform;
		m_rectAspectRatio = rt.sizeDelta.x / rt.sizeDelta.y;
	}

	void CalculateTextureAspectRatio()
	{
		if (m_image == null)
		{
			Debug.Log("CalculateAspectRatio: m_image is null");
			return;
		}

		Texture2D texture = (Texture2D)m_image.texture;
		if (texture == null)
		{
			Debug.Log("CalculateAspectRatio: texture is null");
			return;
		}


		m_aspectRatio = (float)texture.width / texture.height;
		//Debug.Log("textW=" + texture.width + " h=" + texture.height + " ratio=" + m_aspectRatio);
	}

	public void AdjustAspect()
	{
		SetupImage();

		bool fitY = m_aspectRatio < m_rectAspectRatio;

		SetAspectFitToImage(m_image, fitY, m_aspectRatio);

	}


	protected virtual void SetAspectFitToImage(RawImage _image,
					 bool yOverflow, float displayRatio)
	{
		if (_image == null)
		{
			return;
		}

		Rect rect = new Rect(0, 0, 1, 1);   // default
		if (yOverflow)
		{

			rect.height = m_aspectRatio / m_rectAspectRatio;
			rect.y = (1 - rect.height) * 0.5f;
		}
		else
		{
			rect.width = m_rectAspectRatio / m_aspectRatio;
			rect.x = (1 - rect.width) * 0.5f;

		}
		_image.uvRect = rect;

	}
}