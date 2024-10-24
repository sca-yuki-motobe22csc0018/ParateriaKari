using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Line : MonoBehaviour
{
    float speed;
    public string FinishTag;
    public GameObject Stock;
    GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        Stock= GameObject.FindWithTag("LineStock");
        this.transform.SetParent(Stock.transform);
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = controller.StageSpeed;
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(FinishTag))
        {
            Destroy(this.gameObject);
        }
    }
}
