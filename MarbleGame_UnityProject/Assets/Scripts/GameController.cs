using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    [Space]
    [Header("Gameobjects")]
    [Space]
    [SerializeField] PlayerController playerController;

    [Space]
    [Header("Level Variables")]
    [Space]
    [SerializeField] public bool hasBeatenLevel;
    [SerializeField] public bool reachedBeatenLevelHitbox;
    [SerializeField] public int sceneIndex;

    [Space]
    [Header("Cameras")]
    [Space]
    [SerializeField] public GameObject[] cameras;

    [Space]
    [Header("UI")]
    [Space]
    [SerializeField] float UISpeed;

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
            StartCoroutine("LevelTransition");
        }
    }

    public IEnumerator LevelTransition()
    {
        Debug.Log("Coroutine 'Level transition' start");

        //remove player control
        playerController.playerActive = false;

        yield return new WaitForSeconds(2f);

        playerController.blackCircle.GetComponent<RectTransform>().position = playerController.blackCircle.GetComponent<RectTransform>().position + new Vector3(Time.deltaTime * UISpeed, 0, 0);
           
    }
}
