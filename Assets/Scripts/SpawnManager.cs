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
        // หยุด spawn เมื่อ game over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        // สุ่ม type ของ obstacle (0 = Barrel, 1 = Barrier, 2 = StoneWall)
        int randomType = Random.Range(0, 3);

        // Acquire obstacle จาก pool ตาม type ที่สุ่มได้
        GameObject obstacle = obstacleObjectPool.Acquire(randomType);

        // วาง obstacle ที่ spawnPoint
        obstacle.transform.position = spawnPoint.position;
        obstacle.transform.rotation = Quaternion.identity;

        // ส่ง type ไปให้ MoveLeft เพื่อใช้ตอน Release กลับ pool
        MoveLeft moveLeft = obstacle.GetComponent<MoveLeft>();
        if (moveLeft != null)
        {
            moveLeft.obstacleType = randomType;
            moveLeft.obstaclePool = obstacleObjectPool;
        }
    }
}
