using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{

    public static void OpenCloseCanvasGroup(CanvasGroup canvas, bool isOpen)
    {
        canvas.alpha = isOpen ? 1:0;
        canvas.blocksRaycasts = isOpen;
        canvas.interactable = isOpen;
    }
}
