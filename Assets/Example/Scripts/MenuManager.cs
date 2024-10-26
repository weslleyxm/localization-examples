using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Utilities.Localization;

public class MenuManager : MonoBehaviour
{
    private bool subtitles;
    private string currentLanguage;
    private int currentLanguageIndex; 

    [SerializeField] private TextMeshProUGUI onOffTextSubtitles;
    [SerializeField] private TextMeshProUGUI localeTextSelect;

    [SerializeField] private MenuItem onOffSubtitles;
    [SerializeField] private MenuItem localesSelect;
    [SerializeField] private MenuItem volume;
    [SerializeField] private Slider slider; 


    public static MenuManager Instance;
    public Color normalColor, selectedColor;

    private Locale[] locales;

    private void Awake()
    {
        Instance = this;

        locales = Localization.AvaliableLocales();

        onOffSubtitles.OnPressMenuItemEventHandler.AddListener((s) =>
        {
            subtitles = !subtitles;
            onOffTextSubtitles.text = Localization.t(subtitles ? "menu.on" : "menu.off");
        });

        localeTextSelect.text = Localization.t($"locales.{locales[0].localeName.ToLower()}");

        localesSelect.OnPressMenuItemEventHandler.AddListener((s) =>
        {
            if (s == Direction.Left)
                currentLanguageIndex--;
            else
                currentLanguageIndex++;

            if (currentLanguageIndex > locales.Length - 1)
                currentLanguageIndex = 0;
            if (currentLanguageIndex < 0)
                currentLanguageIndex = locales.Length - 1;

            Localization.SetLocal(locales[currentLanguageIndex].abbreviation);
            localeTextSelect.text = Localization.t($"locales.{locales[currentLanguageIndex].localeName.ToLower()}");
            onOffTextSubtitles.text = Localization.t(subtitles ? "menu.on" : "menu.off"); 
        });

        volume.OnPressMenuItemEventHandler.AddListener((s) =>
        {
            if (s == Direction.Left)
                slider.value -= 0.1f;
            else
                slider.value += 0.1f;

            AudioListener.volume = slider.value;
        });
    }

    void Update()
    {

    }
}
