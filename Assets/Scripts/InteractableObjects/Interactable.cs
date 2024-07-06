using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    private Outline outline;

    private int frameFade = 3;
    private int tmpFading = 3;

    private bool lookedAt = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color(0, 0, 1);
    }

    public bool Interact()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleOutline()
    {
        lookedAt = true;
        tmpFading = frameFade;
        outline.OutlineWidth = 10;
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
            outline.OutlineWidth = 0;
        }
    }
}
