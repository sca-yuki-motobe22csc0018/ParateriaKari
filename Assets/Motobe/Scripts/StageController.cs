using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    float speed;
    GameController controller;

    private void Start()
    {
        controller = FindObjectOfType<GameController>();

    }
    void Update()
    {
        speed = controller.StageSpeed;
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }
}
