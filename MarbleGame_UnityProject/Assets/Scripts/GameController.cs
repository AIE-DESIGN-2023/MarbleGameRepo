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
    }

    public IEnumerator LevelTransition()
    {
        UIAnimator.SetTrigger("Transition Exit");

        yield return new WaitForSeconds(3f);

        //load new level
        SceneManager.LoadScene(playerController.sceneIndex++);

        //move marble to new prespawn location
        UIAnimator.SetTrigger("Transition Enter");

        yield return new WaitForSeconds(1f);

        playerController.playerActive = true;
    }
}
