using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandller : MonoBehaviour
{
    //Time to restart/switch level
    [SerializeField] float invokeTime = 1f;
    [SerializeField] AudioClip crashSound, finishSound;
    [SerializeField] ParticleSystem crashParticles, finishParticles;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }
    //Debug Codes (Next Level and disabling collision)
    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            //Toggle Collision
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        //Preventing multiple Finish/Crash Sequences
        if (isTransitioning || collisionDisabled){return;}
        switch (other.gameObject.tag)
        {
            //What did we hit
            case "Friendly":
            break;

            case "Finish":
            StartFinishSequence();
            break;
            
            //Obstacle
            default:
            StartCrashSequence();
            break;
        }
    }

    void StartCrashSequence()
    {
        //Preventing End Sequences to play mutliple times
        isTransitioning = true;
        //SFX and VFX
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",invokeTime);
    }

    void StartFinishSequence()
    {
        //Preventing End Sequences to play mutliple times
        isTransitioning = true;
        //SFX and VFX
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",invokeTime);
    }

        //Level handler
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
