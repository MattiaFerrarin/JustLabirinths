using UnityEngine;
using UnityEngine.Events;

public class PressurePlateHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _parent;
    [SerializeField]
    private float _height = 0f;
    [SerializeField]
    private float _maxSliding = 0.1f;
    [SerializeField]
    private float _step = 0.05f;
    private bool collidingWithPlayer = false;
    private bool fullyPressed = false;

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    public void Update()
    {
        float scale = _parent.transform.localScale.y;

        if (gameObject.transform.position.y > _height * scale)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, _height * scale, gameObject.transform.position.z);
        }

        if (collidingWithPlayer)
        {
            if (gameObject.transform.position.y > (_height - _maxSliding) * scale)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - _step, gameObject.transform.position.z);
            }
            else
            {
                if (!fullyPressed)
                {
                    OnPressed?.Invoke();
                }
                fullyPressed = true;
            }
        }
        else
        {
            if (gameObject.transform.position.y < _height * scale)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _step, gameObject.transform.position.z);
                if (fullyPressed)
                {
                    OnReleased?.Invoke();
                }
                fullyPressed = false;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }
}
