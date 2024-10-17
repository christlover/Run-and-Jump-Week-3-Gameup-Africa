using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    private float speed = 10;
    private PlayerController playerControllerScript; //Variable to Get Player

    private float leftbound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); //Control Player
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) { //Stop Player Movement if Game Over has Occurred
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftbound && gameObject.CompareTag("Obstacle")) {  //If any prefab has the Obstacle Tag, Destroy it when off screen
            Destroy(gameObject);
        }
    }
}
