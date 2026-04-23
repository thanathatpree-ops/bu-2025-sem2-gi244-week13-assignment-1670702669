using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;

    // ข้อมูลสำหรับส่งกลับ pool
    [HideInInspector] public int obstacleType = 0;
    [HideInInspector] public ObstacleObjectPool obstaclePool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1.17 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            speed = 0;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -15 && gameObject.CompareTag("Obstacle"))
        {
            if (obstaclePool != null)
            {
                // คืน obstacle กลับไปใน pool ของ type ที่ถูกต้อง
                obstaclePool.Release(gameObject, obstacleType);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
