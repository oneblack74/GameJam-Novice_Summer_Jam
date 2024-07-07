using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Interactable))]
public class ButtonInteractable : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        SoundEffectManager.Instance.PlaySoundEffect("SE_ButtonKeyboard");
        SceneManager.LoadScene("SceneFin");
    }
}