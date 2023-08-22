using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    TerrainGenerationController terrainGenerationController;

    public GameObject targetMap;

    void Start()
    {
        terrainGenerationController = FindObjectOfType<TerrainGenerationController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            terrainGenerationController.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (terrainGenerationController.currentChunk == targetMap)
            {
                terrainGenerationController.currentChunk = null;
            }
        }
    }
}
