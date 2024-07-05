using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance = null;

    public Dialogue[] dialogues;
    public TextMeshProUGUI textDialogue;
    public GameObject dialoguePanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [ContextMenu("PlayDialogue")]
    public void PlayDialogueTest()
    {
        PlayDialogue("TestEnregistrementVoix");
    }

    public void PlayDialogue(string key)
    {
        dialoguePanel.SetActive(true);
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.key == key)
            {
                StartCoroutine(DisplayTextCoroutine(dialogue.sentences));
                GameManager.Instance.PlayAudioClip(dialogue.voice);
                Debug.Log("Play dialogue: " + dialogue.key);
                StartCoroutine(PlayDialogueCoroutine(dialogue.voice.length + 1f));
            }
        }
    }

    [ContextMenu("StopDialogue")]
    public void StopDialogue()
    {
        textDialogue.text = "";
        GameManager.Instance.StopAudioClip();
        dialoguePanel.SetActive(false);

        StopCoroutine(PlayDialogueCoroutine(0));
    }

    public IEnumerator PlayDialogueCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        StopDialogue();
    }

    private IEnumerator DisplayTextCoroutine(string text)
    {
        textDialogue.text = ""; // Réinitialiser le texte
        foreach (char letter in text.ToCharArray())
        {
            textDialogue.text += letter;
            yield return new WaitForSeconds(0.05f); // Ajustez le délai pour contrôler la vitesse de l'affichage
        }
    }
}

[System.Serializable]
public class Dialogue
{
    public string key;
    public string sentences;
    public AudioClip voice;
}
