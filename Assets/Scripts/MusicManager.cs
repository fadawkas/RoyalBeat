using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern to ensure only one MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Check if the current scene is "Opening" or "Overworld" when the game starts
        UpdateMusicStatus(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from scene changes
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update music status based on the new scene
        UpdateMusicStatus(scene.name);
    }

    private void UpdateMusicStatus(string sceneName)
    {
        if (sceneName == "Opening" || sceneName == "Overworld" || sceneName == "OPCutScene")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
