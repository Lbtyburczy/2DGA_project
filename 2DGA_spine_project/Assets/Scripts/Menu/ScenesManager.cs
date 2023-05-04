using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private float target;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public enum Scene { 
        MainMenu,
        Level01
    }

    public void LoadMainMenu() {
        // these are both valid options
        //SceneManager.LoadScene((int)Scene.MainMenu);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public async void LoadSceneAsync() {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            //using System.Threading.Tasks;     
            await Task.Delay(100); // DO NOT ADD THIS LINE!!!!!
            target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000); // DO NOT ADD THIS LINE!!!!!

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * 3);
    }
}
