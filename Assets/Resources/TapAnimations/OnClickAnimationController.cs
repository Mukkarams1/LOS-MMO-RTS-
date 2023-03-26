using System.Collections;
using System.Collections.Generic;

 using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class OnClickAnimationController: MonoBehaviour
{
    public float duration;
    [SerializeField] private Sprite[] sprites;
    private Image image;
    private int index = 0;
    private float timer = 0;
    bool isAnimated = true;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            SoundManager.instance.PlaySound("TouchSound");
            index = 0;
            Vector3 mousePos = Input.mousePosition;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            isAnimated = false;
        }

        if (!isAnimated)
        {
            if ((timer += Time.deltaTime) >= (duration / sprites.Length))
            {
                timer = 0;
                image.sprite = sprites[index];
                index = (index + 1);
                if (index >= sprites.Length)
                {
                    isAnimated = true;
                }
            }
        }
    }
}
