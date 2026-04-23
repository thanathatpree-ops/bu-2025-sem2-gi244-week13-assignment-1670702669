using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    private ObstacleObjectPool obstacleObjectPool;

    void Start()
    {
        obstacleObjectPool = FindFirstObjectByType<ObstacleObjectPool>();
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        
        int randomType = Random.Range(0, 3);

        
        GameObject obstacle = obstacleObjectPool.Acquire(randomType);

        
        obstacle.transform.position = spawnPoint.position;
        obstacle.transform.rotation = Quaternion.identity;

        
        MoveLeft moveLeft = obstacle.GetComponent<MoveLeft>();
        if (moveLeft != null)
        {
            moveLeft.obstacleType = randomType;
            moveLeft.obstaclePool = obstacleObjectPool;
        }
    }
}
