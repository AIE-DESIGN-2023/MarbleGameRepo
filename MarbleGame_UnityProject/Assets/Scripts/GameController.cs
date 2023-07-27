using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Space]
    [Header("Gameobjects")]
    [Space]
    [SerializeField] PlayerController playerController;
    [SerializeField] public Animator UIAnimator;

    [Space]
    [Header("Level Variables")]
    [Space]
    [SerializeField] public bool hasBeatenLevel;
    [SerializeField] public bool reachedBeatenLevelHitbox;

    [Space]
    [SerializeField] GameObject devToolCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController)
        {
            playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        }

        if (reachedBeatenLevelHitbox)
        {
            reachedBeatenLevelHitbox = false;
            playerController.playerActive = false;
            StartCoroutine("LevelTransition");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            if(devToolCanvas.activeInHierarchy)
            {
                DevToolsOff();
            }
            else
            {
                DevToolsOn();
            }
        }
    }

    private void DevToolsOn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        devToolCanvas.SetActive(true);
    }

    private void DevToolsOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        devToolCanvas.SetActive(false);
    }

    public IEnumerator LevelTransition()
    {
        UIAnimator.SetTrigger("Transition Exit");

        yield return new WaitForSeconds(3f);

        //load new level
        SceneManager.LoadScene(playerController.sceneIndex + 1);

        //move marble to new prespawn location
        UIAnimator.SetTrigger("Transition Enter");

        yield return new WaitForSeconds(1f);

        playerController.playerActive = true;
    }

    public void LoadNextScene()
    {
        //load new level
        SceneManager.LoadScene(playerController.sceneIndex + 1);
        DevToolsOff();
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
        DevToolsOff();
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
        DevToolsOff();
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
        DevToolsOff();
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
        DevToolsOff();
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
        DevToolsOff();
    }

    public void LoadScene5()
    {
        SceneManager.LoadScene(5);
        DevToolsOff();
    }

    public void LoadScene6()
    {
        SceneManager.LoadScene(6);
        DevToolsOff();
    }
}
