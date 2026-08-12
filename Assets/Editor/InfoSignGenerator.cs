using UnityEngine;
using UnityEditor;
using System.IO;

public class InfoSignGenerator : EditorWindow
{
    private TextAsset arquivoDeTexto;
    private string pastaDestino = "Assets/Prefabs/Placas";

    [MenuItem("Ferramentas/Gerador de Placas")]
    public static void ShowWindow()
    {
        GetWindow<InfoSignGenerator>("Gerador de Placas");
    }

    private void OnGUI()
    {
        GUILayout.Label("Gerar Scriptable Objects em Lote", EditorStyles.boldLabel);
        GUILayout.Space(10);

        arquivoDeTexto = (TextAsset)EditorGUILayout.ObjectField("Arquivo TXT:", arquivoDeTexto, typeof(TextAsset), false);
        pastaDestino = EditorGUILayout.TextField("Salvar na Pasta:", pastaDestino);

        GUILayout.Space(20);

        if (GUILayout.Button("Gerar Tudo!", GUILayout.Height(40)))
        {
            if (arquivoDeTexto != null)
            {
                GerarSOs();
            }
            else
            {
                Debug.LogWarning("Por favor, arraste o arquivo TXT primeiro.");
            }
        }
    }

    private void GerarSOs()
    {
        if (!AssetDatabase.IsValidFolder(pastaDestino))
        {
            Debug.LogError("A pasta " + pastaDestino + " não existe! Crie a pasta no Unity primeiro.");
            return;
        }

        string[] blocosDeTexto = arquivoDeTexto.text.Split(new string[] { "===" }, System.StringSplitOptions.RemoveEmptyEntries);
        int quantidadeGerada = 0;

        foreach (string bloco in blocosDeTexto)
        {
            string[] linhas = bloco.Trim().Split('\n');

            if (linhas.Length >= 2)
            {
                string titulo = linhas[0].Replace("Titulo:", "").Trim();

                string conteudo = "";
                for (int i = 1; i < linhas.Length; i++)
                {
                    conteudo += linhas[i].Replace("Texto:", "").Trim() + "\n";
                }

                InfoSignData novaPlaca = ScriptableObject.CreateInstance<InfoSignData>();
                novaPlaca.signTitle = titulo;
                novaPlaca.signText = conteudo.Trim();

                string nomeArquivo = titulo.Replace(" ", "").Replace("?", "") + ".asset";
                string caminhoFinal = pastaDestino + "/" + nomeArquivo;

                AssetDatabase.CreateAsset(novaPlaca, caminhoFinal);
                quantidadeGerada++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("<color=green><b>Sucesso!</b></color> " + quantidadeGerada + " Scriptable Objects criados na pasta: " + pastaDestino);
    }
}