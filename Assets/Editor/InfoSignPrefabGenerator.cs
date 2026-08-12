using UnityEngine;
using UnityEditor;
using System.IO;

public class InfoSignPrefabGenerator : EditorWindow
{
    private GameObject basePrefab;
    private string soFolderPath = "Assets/Placas";
    private string prefabDestFolder = "Assets/Prefabs/PlacasProntas";

    [MenuItem("Ferramentas/Gerador de Prefabs de Placas")]
    public static void ShowWindow()
    {
        GetWindow<InfoSignPrefabGenerator>("Gerar Prefabs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Gerar Prefabs Preenchidos", EditorStyles.boldLabel);
        GUILayout.Space(10);

        basePrefab = (GameObject)EditorGUILayout.ObjectField("Prefab Molde (Base):", basePrefab, typeof(GameObject), false);

        GUILayout.Space(5);
        soFolderPath = EditorGUILayout.TextField("Pasta dos Textos (SOs):", soFolderPath);
        prefabDestFolder = EditorGUILayout.TextField("Salvar Prefabs na Pasta:", prefabDestFolder);

        GUILayout.Space(20);

        if (GUILayout.Button("Gerar Prefabs!", GUILayout.Height(40)))
        {
            GerarPrefabs();
        }
    }

    private void GerarPrefabs()
    {
        if (basePrefab == null)
        {
            Debug.LogWarning("Por favor, arraste o Prefab base da placa.");
            return;
        }

        if (basePrefab.GetComponent<InfoSign>() == null)
        {
            Debug.LogError("O Prefab base precisa ter o script 'InfoSign' anexado!");
            return;
        }

        if (!AssetDatabase.IsValidFolder(soFolderPath))
        {
            Debug.LogError("A pasta dos Scriptable Objects não foi encontrada: " + soFolderPath);
            return;
        }

        if (!AssetDatabase.IsValidFolder(prefabDestFolder))
        {
            Debug.LogError("A pasta de destino dos Prefabs não existe! Por favor, crie a pasta: " + prefabDestFolder);
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:InfoSignData", new[] { soFolderPath });
        int quantidadeGerada = 0;

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            InfoSignData so = AssetDatabase.LoadAssetAtPath<InfoSignData>(assetPath);

            if (so != null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);

                InfoSign infoSignScript = instance.GetComponent<InfoSign>();
                SerializedObject serializedScript = new SerializedObject(infoSignScript);
                SerializedProperty signDataProperty = serializedScript.FindProperty("signData");

                if (signDataProperty != null)
                {
                    signDataProperty.objectReferenceValue = so;
                    serializedScript.ApplyModifiedProperties();
                }

                instance.name = "Placa_" + so.name;

                string prefabPath = prefabDestFolder + "/" + instance.name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);

                DestroyImmediate(instance);

                quantidadeGerada++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("<color=green><b>Sucesso!</b></color> " + quantidadeGerada + " Prefabs criados e preenchidos na pasta: " + prefabDestFolder);
    }
}