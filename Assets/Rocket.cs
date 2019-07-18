using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    AudioSource audioSource;
    Rigidbody rigidBody;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource.PlayOneShot(mainEngine);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) {return;}
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK"); //todo remove print
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstScene", 1f);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            audioSource.UnPause();
            rigidBody.AddRelativeForce(Vector3.up * shipThrust);
        }
        else
        {
            audioSource.Pause();
        }
    }
    void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }

}