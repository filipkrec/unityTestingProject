using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C_Tooltip
{
    protected TextMeshProUGUI descriptionText;
    protected float lastDescriptionUpdateTime;
    protected GameObject modifierIcon;

    private C_Modifier modifier;
    private float updateTime = 0f;

    private void Update()
    {
        updateTime += Time.smoothDeltaTime;
        if (updateTime > 1f / 60)
            descriptionText.text = modifier.GetDescription();
    }

    public C_Tooltip(C_Modifier mod, string name, string description, Sprite icon)
    {
        modifier = mod;

        GameObject prefab = Globals.GetPrefab(0); //modifierIcon prefab
        if (prefab != null)

        modifierIcon = MonoBehaviour.Instantiate(prefab, Globals.Canvas.transform);
        modifierIcon.GetComponent<Image>().sprite = icon;

        foreach (Transform child in modifierIcon.GetComponentsInChildren<Transform>())
        {
            TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

            if (txt != null)
                if (txt.name == "Name")
                    txt.SetText(name);
                else if (txt.name == "Description")
                {
                    txt.SetText(description);
                    descriptionText = txt;
                }
        }

        Globals.OnUpdate += Update;
    }

    public void Destroy()
    {
        Object.Destroy(modifierIcon);
        Globals.OnUpdate -= Update;
    }

    public void setPosition(Vector2 newPosition)
    {
        if (modifierIcon != null)
            modifierIcon.transform.localPosition = newPosition;
    }

    public void movePosition(Vector2 moveFor)
    {
        if (modifierIcon != null)
            modifierIcon.transform.localPosition += new Vector3(moveFor.x, moveFor.y);
    }

    public void setDescription(string text)
    {
            descriptionText.text = text;
    }
}
