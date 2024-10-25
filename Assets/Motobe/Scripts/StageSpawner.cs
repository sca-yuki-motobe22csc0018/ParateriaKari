using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private int spawnRandom;
    public int[] stageProbability;
    public int stageAmount;
    public Vector3 spawnPosition;
    public string RespawnTag;
    public string LineTag;
    public string StagePrefab;
    void Start()
    {
        for (int i = 0; i < stageAmount; i++)
        {
            spawnRandom += stageProbability[i];
        }
        int Rand = Random.Range(0, spawnRandom);
        for (int i = 0; i < stageAmount; i++)
        {
            Rand -= stageProbability[i];
            if (Rand < 0)
            {
                Stage(spawnPosition, i);
                break;
            }
        }
    }
    private void Stage(Vector3 pos, int num)
    {
        GameObject Stage_prefab = Resources.Load<GameObject>(StagePrefab + num);
        GameObject Stage = Instantiate(Stage_prefab, pos, Quaternion.identity);
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(RespawnTag))
        {
            int Rand = Random.Range(0, spawnRandom);
            for (int i = 0; i < stageAmount; i++)
            {
                Rand -= stageProbability[i];
                if (Rand < 0)
                {
                    Stage(spawnPosition, i);
                    break;
                }
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag(LineTag))
        {
            Destroy(other.gameObject);
        }
    }
}
