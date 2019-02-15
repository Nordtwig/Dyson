using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Button previousButton;
    public Button nextButton;
    public Image displayedImage;
    public Sprite[] howToPlayImages;

    private int imageIndex;

    private void Start()
    {
        ResetImage();
    }

    public void ResetImage()
    {
        imageIndex = 0;
        displayedImage.sprite = howToPlayImages[imageIndex];
        nextButton.interactable = true;
        previousButton.interactable = false;
    }

    public void GoToNextImage()
    {
        imageIndex++;
        previousButton.interactable = true;
        if (imageIndex == howToPlayImages.Length - 1)
        {
            nextButton.interactable = false;
        }
        displayedImage.sprite = howToPlayImages[imageIndex];
    }

    public void GoToPreviousImage()
    {
        imageIndex--;
        nextButton.interactable = true;
        if (imageIndex == 0)
        {
            previousButton.interactable = false;
        }
        displayedImage.sprite = howToPlayImages[imageIndex];
    }

}
