using System.Collections; 
using TMPro;
using UnityEngine;
using Utilities.Localization;

public class GetText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private string[] locales = new string[]
    {
        "en-US", "es-ES", "pt-BR"
    };

    IEnumerator Start()
    {
        foreach (var locale in locales)
        {

            Localization.SetLocal(locale);

            textMeshProUGUI.text = Localization.t("text.text1");
            yield return new WaitForSeconds(2.5f);
            textMeshProUGUI.text = Localization.t("text.text2");
            yield return new WaitForSeconds(2.5f);
            textMeshProUGUI.text = Localization.t("text.text3");
            yield return new WaitForSeconds(2.5f);
            textMeshProUGUI.text = Localization.t("text.text4");
            yield return new WaitForSeconds(2.5f); 
        }

    }

}
