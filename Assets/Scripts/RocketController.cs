using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    public InputActionReference toucheMonte;
    public float vitesseDescente;
    public bool jumpPressed;
    public GameObject canvas;


    // Update is called once per frame
    void Update()
    {
        if (jumpPressed == false)
        {
            transform.position -= new Vector3(0, vitesseDescente * Time.deltaTime, 0);
        }else
        {
            transform.position -= new Vector3(0, (-vitesseDescente) * Time.deltaTime, 0);
        }
    }

    private void OnEnable() {
        toucheMonte.action.Enable();

        toucheMonte.action.performed += OnJumpPressed;
        toucheMonte.action.canceled += OnJumpReleased;
    }

    private void OnDisable() {
        toucheMonte.action.Disable();

        toucheMonte.action.performed -= OnJumpPressed;
        toucheMonte.action.canceled -= OnJumpReleased;
    }

    private void OnJumpPressed(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }
        private void OnJumpReleased(InputAction.CallbackContext context)
    {
        jumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Sol"))
        {
            print("touché !!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Start()
    {
        canvas.SetActive(false);
    }
}
