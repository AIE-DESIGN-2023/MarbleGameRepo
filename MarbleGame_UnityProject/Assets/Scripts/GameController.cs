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
<<<<<<< HEAD
    [SerializeField] public int sceneIndex;
    [Space]
    [SerializeField] public GameObject transistionUI;
    [SerializeField] public float transitionX_Current;
    [SerializeField] public float transitionX_Enter;
    [SerializeField] public float transitionX_Middle;
    [SerializeField] public float transitionX_Exit;


=======
>>>>>>> origin/Ru2

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
    }

    public IEnumerator LevelTransition()
    {
<<<<<<< HEAD
        //remove player control
        playerController.playerActive = false;


=======
        UIAnimator.SetTrigger("Transition Exit");
>>>>>>> origin/Ru2

        yield return new WaitForSeconds(3f);

        //load new level
        SceneManager.LoadScene(playerController.sceneIndex++);

        //move marble to new prespawn location
        UIAnimator.SetTrigger("Transition Enter");

        yield return new WaitForSeconds(1f);
<<<<<<< HEAD
=======

        playerController.playerActive = true;
>>>>>>> origin/Ru2
    }
}
