using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject obstacle;

    void Update()
    {
        if (Random.Range(0, 100) < 5)
        {
            Instantiate(obstacle, this.transform.position + 
                                    new Vector3(Random.Range(-10, 10), 0, 0), Quaternion.identity);
        }
    }
}
