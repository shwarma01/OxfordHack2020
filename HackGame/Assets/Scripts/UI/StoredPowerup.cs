using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoredPowerup : MonoBehaviour
{
    [SerializeField] private Sprite[] powerupSprites;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        ShowPowerup("");
    }

    public void ShowPowerup(string powerupName)
    {
        StopAllCoroutines();
        _image.color = Color.white;
        switch (powerupName)
        {
            case "InstaKill":
                _image.sprite = powerupSprites[1];
                break;
            case "PaintNuke":
                _image.sprite = powerupSprites[2];
                break;
            case "InfinitePaint":
                _image.sprite = powerupSprites[3];
                break;
            case "none":
                _image.sprite = powerupSprites[0];
                break;
            default:
                _image.sprite = powerupSprites[0];
                break;
        }
    }

    public void UseLasting()
    {
        StartCoroutine(LastingAnim());
    }

    private IEnumerator LastingAnim()
    {
        while (true)
        {
            for (var i = 255; i >= 0; i -= 2)
            {
                _image.color = new Color32(255, (byte) i, (byte) i, 255);
                yield return new WaitForSeconds(0.0001f);
            }

            for (var i = 0; i <= 255; i += 2)
            {
                _image.color = new Color32(255, (byte) i, (byte) i, 255);
                yield return new WaitForSeconds(0.0001f);
            }
        }
    }
}