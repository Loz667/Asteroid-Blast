using UnityEngine;

public class Drive : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject bulletPrefab;

    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }
    }
}