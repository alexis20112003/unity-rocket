using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class RocketController : MonoBehaviour
{
    public InputActionReference toucheMonte;
    public float vitesseDescente;
    public bool jumpPressed;
    public GameObject btnRestart;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public ParticleSystem rocketParticles;

    public float angleMontee = -70f;
    public float angleDescente = -110f;
    public float vitesseRotation = 5f;

    private int score;
    private int bestScore;


    private void Start()
    {
        Time.timeScale = 1f;      // Sécurité
        score = 0;
        btnRestart.SetActive(false);  // Cache l’UI au départ
        scoreText.text = "Score : 0";
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Meilleur score : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpPressed == false)
        {
            transform.position -= new Vector3(0, vitesseDescente * Time.deltaTime, 0);
            if (rocketParticles.isPlaying)
                rocketParticles.Stop();
        }else
        {
            transform.position -= new Vector3(0, (-vitesseDescente) * Time.deltaTime, 0);
            if (!rocketParticles.isPlaying)
                rocketParticles.Play();
        }

        float angleCible = jumpPressed ? angleMontee : angleDescente;

        Quaternion rotationCible = Quaternion.Euler(0, 0, angleCible);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rotationCible,
            vitesseRotation * Time.deltaTime
        );
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
            Debug.Log("PERDU !");
            Time.timeScale = 0f;   // Pause le jeu
            btnRestart.SetActive(true); // Affiche le menu restart

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore); // sauvegarde le meilleur score
                PlayerPrefs.Save();
            }

            bestScoreText.text = "Meilleur score : " + bestScore;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            score++;
            scoreText.text = "Score : " + score;

            Destroy(other.gameObject); // 💥 la pièce disparaît
        }
    }

    // 🎯 Appelée par le bouton UI
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
