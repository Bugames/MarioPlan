using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class Editor : ScriptableObject
{
    [MenuItem("Assets/Create/C#MyDefineScript/NewSingleCaseScript", false, 1)]
    static void CreateNewSingleCaseScripts()
    {
        CreateFromFileName("NewSingleCaseScript.cs.txt");
    }

    [MenuItem("Assets/Create/C#MyDefineScript/NewBehaveScript", false, 2)]
    static void CreateNewBehaviourScripts()
    {
        CreateFromFileName("NewBehaveScript.cs.txt");
    }

    [MenuItem("Assets/Create/C#MyDefineScript/NewEmptyScript", false, 3)]
    static void CreateNewEmptyScripts()
    {
        CreateFromFileName("NewEmptyScript.cs.txt");
    }

    [MenuItem("Assets/Create/C#MyDefineScript/NewEmptyClass", false, 4)]
    static void CreateNewEmptyClass()
    {
        CreateFromFileName("NewEmptyClass.cs.txt");
    }

    private static void CreateFromFileName(string ScriptTemplateName)
    {
        string ScriptDefaultName = Regex.Replace(ScriptTemplateName, ".txt", "");
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            CreateInstance<CreateAssetAction>(),
            GetSelectedPath() + "/" + ScriptDefaultName, null,
            GetPath() + "/ScriptTemplates/" + ScriptTemplateName);
    }

    private static string GetSelectedPath()
    {
        // 默认路径为Assets
        string selectedPath = "Assets";

        // 获取选中的资源
        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);

        // 遍历选中的资源以返回路径
        foreach (UnityEngine.Object obj in selection)
        {
            selectedPath = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(selectedPath) && File.Exists(selectedPath))
            {
                selectedPath = Path.GetDirectoryName(selectedPath);
                break;
            }
        }

        return selectedPath;
    }
    
    private static string GetPath()
    {
        var s = MonoScript.FromScriptableObject(CreateInstance<Editor>());
        var path = AssetDatabase.GetAssetPath(s);
        return Directory.GetParent(Path.GetDirectoryName(path)).FullName;
    }
}
    