using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelFontRenderer : MonoBehaviour
{

    private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!:.";
    public Sprite[] sprites; // Assign the sprites in the inspector
    public GameObject characterPrefab; // Assign a prefab in the inspector. It should have a SpriteRenderer component

    public bool rightBound = false;
    public float scale = 1.0F;

    public string text = "";
    private string lastRenderedText = "";

    private List<GameObject> instantiatedSprites = new List<GameObject>();

    void Update()
    {
        this.transform.localScale = new Vector3(scale, scale, 1);

        if (this.text != this.lastRenderedText)
        {
            this.RenderText(this.text);
            this.lastRenderedText = this.text;
        }
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    private void RenderText(string text)
    {
        while (instantiatedSprites.Count < text.Length)
        {
            GameObject newCharacter = Instantiate(characterPrefab, transform);
            newCharacter.transform.parent = this.gameObject.transform;
            instantiatedSprites.Add(newCharacter);
        }
        while (instantiatedSprites.Count > text.Length)
        {
            GameObject extraCharacter = instantiatedSprites[instantiatedSprites.Count - 1];
            instantiatedSprites.RemoveAt(instantiatedSprites.Count - 1);
            Destroy(extraCharacter);
        }

        float offset = 0.0F;
        float sign = rightBound ? -1 : 1;

        for (int i = 0; i < text.Length; i++)
        {
            int index = rightBound ? text.Length - 1 - i : i;
            int spriteIndex = characters.IndexOf(char.ToUpper(text[index]));

            if (text == "MOWER")
            {
                Debug.Log(text[index]);

            }

            if (spriteIndex >= 0)
            {
                SpriteRenderer spriteRenderer = instantiatedSprites[i].GetComponent<SpriteRenderer>();
                Sprite sprite = sprites[spriteIndex];
                spriteRenderer.sprite = sprites[spriteIndex];
                instantiatedSprites[i].transform.position = this.gameObject.transform.position + new Vector3(offset, 0, 0) * sign;
                offset += sprite.bounds.size.x * scale - (1.0F / 32.0F) * scale;
            } else if (text[index] == ' ')
            {
                offset += (8.0F / 32.0F) * scale - (1.0F / 32.0F) * scale;
            }
         }
    }
}
