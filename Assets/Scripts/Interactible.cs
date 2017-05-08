using UnityEngine;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;

    //private Material[] defaultMaterials;
    private float ds = 0.1f;

    void Start()
    {
        //defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    void GazeEntered()
    {
       /* for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .25f);
        }*/
    }

    void GazeExited()
    {
       /* for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", 0f);
        }*/
    }

    void OnSelect()
    {
        //ds = -ds;
        //transform.Translate(Vector3.forward*ds);

        if (!GestureManager.Instance.IsNavigating)
        {
            Debug.Log("IsNavigating OnSelect....");
            GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
        }
        else
        {
            Debug.Log("ManipulationRecognizer OnSelect....");
            GestureManager.Instance.Transition(GestureManager.Instance.NavigationRecognizer);
        }
            
    }
}