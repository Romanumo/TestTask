using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScroller : MonoBehaviour
{
    const int ITERATION_PER_BLOCK = 5;
    public static GameObject obstacleObj { get; private set; }
    [SerializeField] private GameObject _obstacleObj;
    [SerializeField] private Tile[] tiles;
    [SerializeField] float scrollSpeed = 3f;

    float tileSize;
    float scrollSpeedIncrease;
    float tileReplacementThreshold;
    int currentTile = 0;
    int iteration = 0; // How many bloacks have passed

    void Start()
    {
        obstacleObj = _obstacleObj;
        tileSize = tiles[0].wall.transform.localScale.x * 10;
        tileReplacementThreshold = tileSize;

        foreach (Tile tile in tiles)
            tile.Init();
    }

    public void ScrollScene()
    {
        foreach (Tile tile in tiles)
            tile.wall.transform.position += new Vector3(-(scrollSpeed + scrollSpeedIncrease) * Time.deltaTime, 0, 0);

        if (tiles[currentTile].wall.transform.position.x < -tileReplacementThreshold)
        {
            tiles[currentTile].wall.transform.position += new Vector3(tileSize * 2, 0, 0);
            ObstacleSpawnCheck();
            currentTile = GeneralFunctions.CircleIndex(currentTile + 1, tiles.Length - 1);
        }
    }

    public void RestartScroller()
    {
        foreach (Tile tile in tiles)
            tile.ObstacleState(false);

        iteration = 0;
    }

    public void ScrollSpeed(int difficulty) => scrollSpeedIncrease = scrollSpeed * difficulty;

    void ObstacleSpawnCheck()
    {
        iteration = GeneralFunctions.CircleIndex(iteration + 1, ITERATION_PER_BLOCK - 1);

        if (iteration == 0)
            tiles[GeneralFunctions.CircleIndex(currentTile, tiles.Length - 1)].ObstacleState(false);

        if (iteration == ITERATION_PER_BLOCK - 2)
        {
            tiles[currentTile].ShuffleObstacle();
            tiles[currentTile].ObstacleState(true);
        }
    }

    #region TileClass
    [System.Serializable]
    class Tile
    {
        [SerializeField] public GameObject wall;
        private GameObject obstacle;

        public void Init()
        {
            AddObstacle();
            obstacle.SetActive(false);
        }

        public void ObstacleState(bool state)
        {
            obstacle.SetActive(state);
        }

        public void ShuffleObstacle()
        {
            obstacle.transform.localPosition = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-3, 3f));
        }

        void AddObstacle()
        {
            obstacle = GameObject.Instantiate(obstacleObj);
            obstacle.transform.parent = wall.transform;
            obstacle.transform.localPosition = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-3, 3f));
        }
    }
    #endregion
}
