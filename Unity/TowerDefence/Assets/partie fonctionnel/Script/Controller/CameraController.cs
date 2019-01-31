using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]
    private float minCamera;
    [SerializeField]
    private float maxCamera;


    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x >= minCamera && Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(- speed * Time.deltaTime, 0));
        }

        if (this.gameObject.transform.position.x <= maxCamera && Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
    }
}
