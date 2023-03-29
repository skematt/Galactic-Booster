using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float deathDelay = 0.7f;
    float nextLevelDelay = 0.7f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other) {

        if (isTransitioning) 
        {
            return;
        }
        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("All good");
                break;
            
            case "Finish":
                SuccessSequence();
                break;

            case "Hostile":
                CrashSequence();
                break;

        }
        
    }

    void CrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, 0.5f);
        Invoke("ReloadLevel", deathDelay);
    }

    void SuccessSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);

        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int numScenes = SceneManager.sceneCountInBuildSettings;

        int nextSceneIndex = currentSceneIndex == (numScenes - 1) ? 0 : currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
