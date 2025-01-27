using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIExtentions
{
    public static void Display(this VisualElement element, bool enabled)
    {
        if (element == null) return;
        element.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public static bool IsVisible(this VisualElement element)
    {
        return element.style.display == DisplayStyle.Flex;
    }
}
