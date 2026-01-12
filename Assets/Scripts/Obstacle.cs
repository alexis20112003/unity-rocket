using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float vitesse;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(vitesse * Time.deltaTime, 0, 0);
    }
}
