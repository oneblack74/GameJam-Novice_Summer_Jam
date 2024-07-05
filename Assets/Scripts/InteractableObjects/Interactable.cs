using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour, IInteractable
{
    private Outline outline;

    private int frameFade = 30;
    private int tmpFading = 30;

    private bool lookedAt = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.SilhouetteOnly;
    }

    public bool Interact()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleOutline()
    {
        lookedAt = true;
        tmpFading = frameFade;
        outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    void FixedUpdate()
    {
        if (lookedAt)
        {
            tmpFading--;
        }
        if (tmpFading == 0)
        {
            lookedAt = false;
            tmpFading = frameFade;
            outline.OutlineMode = Outline.Mode.SilhouetteOnly;
        }
    }
}
