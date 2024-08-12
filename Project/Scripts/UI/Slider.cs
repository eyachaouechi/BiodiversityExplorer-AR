using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.SimpleSlider.Scripts
{
	/// <summary>
	/// Creates banners and paginator by given banner list.
	/// </summary>
	public class Slider : MonoBehaviour
	{
		[Header("Settings")]
		public List<Banner> Banners;
		public bool Random;
		public bool Elastic = true;


		[Header("UI")]
		public Transform BannerGrid;
		public Button BannerPrefab;
		public Transform PaginationGrid;
		public Toggle PagePrefab;
		public HorizontalScrollSnap HorizontalScrollSnap;

		public void OnValidate()
		{
			GetComponent<ScrollRect>().content.GetComponent<GridLayoutGroup>().cellSize = GetComponent<RectTransform>().sizeDelta;
		}

		public IEnumerator Start()
		{

			foreach (Transform child in BannerGrid)
			{
				if (!child.GetComponent<RawImage>())
					break;
				Destroy(child.gameObject);
			}

			foreach (Transform child in PaginationGrid)
			{

				Destroy(child.gameObject);
			}


			//add video to Grid content and add one toggle for it 
			if(SceneManager.GetActiveScene().name == "Load")
            {
				var togglevideo = Instantiate(PagePrefab, PaginationGrid);

				togglevideo.group = PaginationGrid.GetComponent<ToggleGroup>();
			} else
            {
                for (int i = 0; i < 3; i++)
                {
					var togglevideo = Instantiate(PagePrefab, PaginationGrid);

					togglevideo.group = PaginationGrid.GetComponent<ToggleGroup>();
				}

            }
			



			foreach (var banner in Banners)
			{
				var instance = Instantiate(BannerPrefab, BannerGrid);
				//var button = instance.GetComponent<Button>();

				//button.onClick.RemoveAllListeners();

				//if (string.IsNullOrEmpty(banner.Url))
				//{
				//	button.enabled = false;
				//}
				//else
				//{
				//	button.onClick.AddListener(() => { Application.OpenURL(banner.Url); });
				//}

				instance.GetComponent<RawImage>().texture = banner.Sprite;

				if (Banners.Count > 1)
				{
					var toggle = Instantiate(PagePrefab, PaginationGrid);

					toggle.group = PaginationGrid.GetComponent<ToggleGroup>();
				}
			}

			yield return null;

			HorizontalScrollSnap.Initialize(Random);
			HorizontalScrollSnap.GetComponent<ScrollRect>().movementType = Elastic ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Clamped;
		}
	}
}