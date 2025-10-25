using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameManager GameManager;

    [Header("- - - - - GridSpawn - - - - -")]
    public float DistanceBetweenBlocks;
    public BlockV1 Block;
    public Vector2 StartGridSpawnPosition;
    public int Columns;
    public int Rows;

    [Header("- - - - - SpiralSpawn - - - - -")]
    public BlockV2 BlockV2;
    public int SpiralBlocksCount;
    public float StartRadius;
    public float StepRadius;
    public float AngleStep;
    public Vector3 StartSpiralSpawnPosition;

    [Header("- - - - - CircleSpawn - - - - -")]
    public BlockV3 BlockV3;
    public int CircleBlocksCount;
    public float Radius;
    public Vector3 StartCircleSpawnPosition;

    [Header("- - - - - ZSpawn - - - - -")]
    public int BlocksPerLine;
    public BlockV4 BlockV4;
    public float BlocksSpacing;
    public Vector3 StartZSpawnPosition;

    private List<GameObject> _blocks = new List<GameObject>();

    public void SpawnBlocks1()
    {
        //GridSpawner
        for (int column = 0; column < Columns; column++)
        {
            for (int row = 0; row < Rows; row++)
            {
                Vector2 spawnPosition = StartGridSpawnPosition + new Vector2(column * DistanceBetweenBlocks, row * DistanceBetweenBlocks);
                var block = Instantiate(Block, spawnPosition, Quaternion.identity);
                _blocks.Add(block.gameObject);
                GameManager.SetBlocksCount(_blocks);
            }
        }
    }

    public void SpawnBlocks2()
    {
        //SpiralSpawner
        float curentAngle = 0f;
        float curentRadius = StartRadius;
        for (int i = 0; i < SpiralBlocksCount; i++)
        {
            float angleRad = curentAngle * Mathf.Deg2Rad;
            float x = curentRadius * Mathf.Cos(angleRad);
            float y = curentRadius * Mathf.Sin(angleRad);
            Vector3 spawnPosition = StartSpiralSpawnPosition + new Vector3(x, y, 0);
            var block = Instantiate(BlockV2, spawnPosition, Quaternion.identity);
            curentRadius += StepRadius;
            curentAngle += AngleStep;
            _blocks.Add(block.gameObject);
            GameManager.SetBlocksCount(_blocks);

        }
    }

    public void SpawnBlocks3()
    {
        //CircleSpawner
        float angleStep = 360 / CircleBlocksCount;
        for (int i = 0; i < CircleBlocksCount; i++)
        {
            float angleDeg = i * angleStep;
            float angleRad = angleDeg * Mathf.Deg2Rad;
            float x = Radius * Mathf.Cos(angleRad);
            float y = Radius * Mathf.Sin(angleRad);
            Vector3 spawnPosition = StartCircleSpawnPosition + new Vector3(x, y, 0);
            var block = Instantiate(BlockV3, spawnPosition, Quaternion.identity);
            _blocks.Add(block.gameObject);
            GameManager.SetBlocksCount(_blocks);
        }
    }

    public void SpawnBlocks4()
    {
        //ZSpawner
        for (int i = 0; i < BlocksPerLine; i++)
        {
            Vector3 startZSpawnPosition = StartZSpawnPosition + new Vector3(i * BlocksSpacing, 0, 0);
            var block = Instantiate(BlockV4, startZSpawnPosition, Quaternion.identity);
            _blocks.Add(block.gameObject);
            GameManager.SetBlocksCount(_blocks);
        }
        for (int i = 1; i < BlocksPerLine; i++)
        {
            float x = StartZSpawnPosition.x + (BlocksPerLine - 1) * BlocksSpacing - i * BlocksSpacing;
            float y = StartZSpawnPosition.y - i * BlocksSpacing;
            Vector3 spawnPosition = new Vector3(x, y, 0);
            var block = Instantiate(BlockV4, spawnPosition, Quaternion.identity);
            _blocks.Add(block.gameObject);
            GameManager.SetBlocksCount(_blocks);
        }
        Vector3 downPosition = StartZSpawnPosition + new Vector3(0, (-BlocksPerLine + 1) * BlocksSpacing, 0);
        for (int i = 1; i < BlocksPerLine; i++)
        {
            Vector3 startZSpawnPosition = downPosition + new Vector3(i * BlocksSpacing, 0, 0);
            var block = Instantiate(BlockV4, startZSpawnPosition, Quaternion.identity);
            _blocks.Add(block.gameObject);
            GameManager.SetBlocksCount(_blocks);
        }
    }
}
