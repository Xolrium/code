/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
   CMDG LISA ENGELEN
	PORTFOLIO: https://lisaengelen-portfolio.tumblr.com/ */
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour {

	#region Variables
	public List<Feature> features;
	public int curFeature;
	#endregion

	#region Unity Methods
	private void Awake()
	{
		LoadFeature();
	}

	private void OnDisable()
	{
		SaveFeature();
	}

	private void LoadFeature()
	{
		features = new List<Feature>();
		features.Add(new Feature("Face", transform.Find("Face").GetComponent<SpriteRenderer>()));
		features.Add(new Feature("Eyes", transform.Find("Eyes").GetComponent<SpriteRenderer>()));
		features.Add(new Feature("Body", transform.Find("Body").GetComponent<MeshFilter>()));
		features.Add(new Feature("Legs", transform.Find("Legs").GetComponent<MeshFilter>()));

		for (int i = 0; i < features.Count; i++)
		{
			string key = "FEATURE_ " + i;
			if (!PlayerPrefs.HasKey(key))
				PlayerPrefs.SetInt(key, features[i].curIndex);
			features[i].curIndex = PlayerPrefs.GetInt(key);
			features[i].UpdateFeature();
			features[i].UpdateFeature3D();
		}
	}

	public void SaveFeature()
	{
		for (int i = 0; i < features.Count; i++)
		{
			string key = "FEATURE_ " + i;
			PlayerPrefs.SetInt(key, features[i].curIndex);
		}
		PlayerPrefs.Save();
	}

	public void SetCurrent(int index)
	{
		if (features == null)
			return;

		curFeature = index;
	}

	public void NextChoice()
	{
		if (features == null)
			return;
		features[curFeature].curIndex++;
		features[curFeature].UpdateFeature();
		features[curFeature].UpdateFeature3D();
	}

	public void PreviousChoice()
	{
		if (features == null)
			return;
		features[curFeature].curIndex--;
		features[curFeature].UpdateFeature();
		features[curFeature].UpdateFeature3D();
	}
	#endregion
}

[System.Serializable]
public class Feature
{
	public string ID;
	public int curIndex;
	public Sprite[] choices;
	public List<GameObject> choices3D = new List<GameObject>();
	public SpriteRenderer renderer;
	public MeshFilter mRenderer;

	public Feature(string id, SpriteRenderer rend)
	{
		ID = id;
		renderer = rend;
		UpdateFeature();
	}

	public Feature(string id, MeshFilter rend)
	{
		ID = id;
		mRenderer = rend;
		UpdateFeature3D();
	}

	public void UpdateFeature()
	{
		choices = Resources.LoadAll<Sprite>("Customization/" + ID);

		if (choices == null || renderer == null)
			return;

		if (curIndex < 0)
			curIndex = choices.Length - 1;
		if (curIndex >= choices.Length)
			curIndex = 0;

		renderer.sprite = choices[curIndex];
	}

	public void UpdateFeature3D()
	{
		var c = Resources.LoadAll<GameObject>("Customization/") as GameObject[];
		for (int i = 0; i < c.Length; i++)
		{
			if (c[i].name.Contains(ID) && choices3D != null)
				choices3D.Add(c[i]);
		}

		choices3D = choices3D.Distinct().ToList();
		if (choices3D == null || mRenderer == null)
			return;

		if (curIndex < 0)
			curIndex = choices3D.Count;
		if (curIndex >= choices3D.Count)
			curIndex = 0;

		mRenderer.sharedMesh = choices3D[curIndex].GetComponent<MeshFilter>().sharedMesh;
	}
}

