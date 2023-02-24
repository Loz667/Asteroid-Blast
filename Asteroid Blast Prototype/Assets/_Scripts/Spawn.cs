using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Drive driveScript;

    private void Start()
    {
        driveScript = GameObject.Find("Ship").GetComponent<Drive>();
    }
    void Update()
    {
        if (!GameManager.Instance.GameOver)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        if (Random.Range(0, 150) < 10)
        {
            GameObject asteroid = Pool.instance.GetObject("Asteroid");
            if (asteroid != null)
            {
                asteroid.transform.position = this.transform.position + new Vector3(Random.Range(-9, 9), 0, 0);
                asteroid.SetActive(true);
            }
        }
    }
}
