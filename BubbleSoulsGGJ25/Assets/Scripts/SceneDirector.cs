using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public static SceneDirector Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        print("Quitting...");
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Reload(sceneIndex, .2f));
    }

    public void StartReload(int sceneIndex, float waitTime)
    {
        StartCoroutine(Reload(sceneIndex, waitTime));
    }

    public IEnumerator Reload(int sceneIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
