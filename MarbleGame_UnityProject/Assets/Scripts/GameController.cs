using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Space]
    [Header("Gameobjects")]
    [Space]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject board;
    [SerializeField] GameObject marble;

/*    [Space]
    [Header("Colliders")]
    [Space]
    [SerializeField] BoxCollider nextLevelCollider1;
    [SerializeField] BoxCollider nextLevelCollider2;
    [SerializeField] BoxCollider nextLevelCollider3;
    [SerializeField] BoxCollider deathBox;*/

    [Space]
    [Header("Spawn Point")]
    [Space]
    [SerializeField] Vector3 spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        //SetMarblePosition
        marble.transform.position = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
