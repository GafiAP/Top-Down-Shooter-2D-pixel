using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image progressBarImage;

    private float target;



    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScenes(sceneName));
        Time.timeScale = 1f;
    }
    IEnumerator LoadScenes(string sceneName)
    {
        target = 0;
        progressBarImage.fillAmount = 0f;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingCanvas.SetActive(true);
        do
        {
            yield return new WaitForSeconds(1f);
            target = scene.progress;

        } while (scene.progress < 0.9f);
        yield return new WaitForSeconds(1f);
        scene.allowSceneActivation = true;
        yield return new WaitForSeconds(1f);
        
        loadingCanvas.SetActive(false);
        
    }
    private void Update()
    {
        progressBarImage.fillAmount = Mathf.MoveTowards(progressBarImage.fillAmount, target, 3 * Time.deltaTime);
    }
}
