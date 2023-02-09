using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Vector3 velocity;

    void Update()
    {
        this.transform.Translate(velocity);
    }
}
