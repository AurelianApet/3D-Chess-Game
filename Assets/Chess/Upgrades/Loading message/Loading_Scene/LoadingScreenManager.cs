using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
	public Image ProgressImg;
	AsyncOperation operation;
	public static int sceneToLoad = -1;

	public static void LoadScene(int levelNum) 
	{
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		sceneToLoad = levelNum;
		SceneManager.LoadScene(3); // loadingSceneIndex of loading screen
	}

	void Start() 
	{
		if (sceneToLoad < 0)
			return;

		StartCoroutine(LoadAsync(sceneToLoad));
	}

	IEnumerator LoadAsync(int levelNum) 
	{
		ProgressImg.fillAmount = 0f;

		yield return null; 

		StartOperation(levelNum);
		operation.allowSceneActivation = true;	


		float lastProgress = 0;
		while (DoneLoading() == false)
		{
			yield return null;

			if (Mathf.Approximately(operation.progress, lastProgress) == false) 
			{
				ProgressImg.fillAmount = operation.progress;
				lastProgress = operation.progress;
			}
		}

		ProgressImg.fillAmount = 1f;
	}
	bool DoneLoading() 
	{
		return (operation.progress >= 0.9f); 
	}

	void StartOperation(int levelNum) 
	{
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		operation = SceneManager.LoadSceneAsync(levelNum, LoadSceneMode.Single);
		operation.allowSceneActivation = false;	
	}
}