using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("All good");
                break;
            
            case "Finish":
                Debug.Log("CONGRATS, you finished!");
                break;

            case "Hostile":
                ReloadLevel();
                break;

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
