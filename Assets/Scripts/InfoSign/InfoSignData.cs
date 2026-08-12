using UnityEngine;

[CreateAssetMenu(fileName = "NewInfoSign", menuName = "Biblioteca/Placa de Informacao")]
public class InfoSignData : ScriptableObject
{
    [Header("Conteúdo da Placa")]
    public string signTitle;

    [TextArea(5, 10)]
    public string signText;
}