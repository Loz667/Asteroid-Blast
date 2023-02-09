using UnityEngine;

public class Spawn : MonoBehaviour
{
    void Update()
    {
        if (Random.Range(0, 500) < 5)
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
