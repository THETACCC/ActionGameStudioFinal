using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private int levelIndex;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            levelIndex = 0;
            LoadSpecific();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            levelIndex = 1;
            LoadSpecific();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            levelIndex = 2;
            LoadSpecific();
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }



    public void RestartLevel()
    {
        StartCoroutine(LevelRestart());
    }

    public void LoadSpecific()
    {
        StartCoroutine(LoadLevelSpecific());
    }


    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("Start");
    }

    IEnumerator LoadLevelSpecific()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadSceneAsync(levelIndex);
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("Start");
    }


    IEnumerator LevelRestart()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        transitionAnim.SetTrigger("Start");
    }


}
