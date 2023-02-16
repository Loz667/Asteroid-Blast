using UnityEngine;

public class Spawn : MonoBehaviour
{
    void Update()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        if (Random.Range(0, 250) < 5)
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
