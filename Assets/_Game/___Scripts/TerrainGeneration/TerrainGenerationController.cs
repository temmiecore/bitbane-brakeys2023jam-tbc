using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerationController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject currentChunk;
    public float checkerRadius;
    public LayerMask terrainMask;

    private Vector3 noTerrainPosition;
    private Transform player;
    private Vector3 playerLastPosition;
    private Dictionary<Vector2, string> compass;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public float maxOptimizationDistance;
    public float optimizerCooldownDur;

    private GameObject latestChunk;
    private float opDist;
    private float optimizerCooldown;

    private void Start()
    {
        player = GameManager.Instance.playerMover.transform;

        compass = new Dictionary<Vector2, string>();

        compass.Add(Vector2.up, "Up");
        compass.Add(Vector2.down, "Down");
        compass.Add(Vector2.left, "Left");
        compass.Add(Vector2.right, "Right");
        compass.Add(new Vector2(1, 1), "RightUp");
        compass.Add(new Vector2(1, -1), "RightDown");
        compass.Add(new Vector2(-1, 1), "LeftUp");
        compass.Add(new Vector2(-1, -1), "LeftDown");
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimzer();
    }

    private void ChunkChecker()
    {
        if (!currentChunk)
            return;

        Vector3 playerDirection = player.position - playerLastPosition;
        playerLastPosition = player.position;

        string directionString = GetDirection(playerDirection);

        if (directionString != null)
            CheckAndSpawnChunk(directionString);

        switch (directionString)
        {
            default: { break; }
            case "LeftUp": { CheckAndSpawnChunk("Left"); CheckAndSpawnChunk("Up"); break; }
            case "LeftDown": { CheckAndSpawnChunk("Left"); CheckAndSpawnChunk("Down"); break; }
            case "RightUp": { CheckAndSpawnChunk("Right"); CheckAndSpawnChunk("Up"); break; }
            case "RightDown": { CheckAndSpawnChunk("Right"); CheckAndSpawnChunk("Down"); break; }
        }
    }

    private string GetDirection(Vector3 vector)
    {
        if (vector == Vector3.zero)
            return null;

        float maxDot = -Mathf.Infinity;
        string result = "";

        foreach (KeyValuePair<Vector2, string> direction in compass)
        {
            var dot = Vector2.Dot(direction.Key, vector);
            if (dot > maxDot)
            {
                result = direction.Value;
                maxDot = dot;
            }
        }

        return result;
    }

    private void CheckAndSpawnChunk(string direction)
    {
        Transform anchor = currentChunk.transform.Find(direction);

        if (!Physics2D.OverlapCircle(anchor.position, checkerRadius, terrainMask))
        {
            noTerrainPosition = anchor.position;
            SpawnChunk();
        }
    }

    private void ChunkOptimzer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
            optimizerCooldown = optimizerCooldownDur;
        else
            return;

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(GameManager.Instance.playerMover.transform.position, chunk.transform.position);
            if (opDist > maxOptimizationDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }

    private void SpawnChunk()
    {
        int randomChunk = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[randomChunk], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
}
