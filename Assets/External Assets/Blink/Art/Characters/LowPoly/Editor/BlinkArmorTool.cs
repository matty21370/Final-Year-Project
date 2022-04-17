using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlinkArmorTool : EditorWindow
{
    private ScriptableObject scriptableObj;
    private SerializedObject serialObj;
    
    public SkinnedMeshRenderer[] armorPieces;
    public Transform newArmature;
    public Transform newParent;

    private Vector2 viewScrollPosition;
    
    [MenuItem("BLINK/Armor Tool")]
    private static void OpenWindow()
    {
        var window = (BlinkArmorTool) GetWindow(typeof(BlinkArmorTool), false, "Blink Armor Tool");
        window.minSize = new Vector2(400, 500);
        GUI.contentColor = Color.white;
        window.Show();
    }

    private void OnGUI()
    {
        DrawUpdaterWindow();
    }

    private void OnEnable()
    {
        scriptableObj = this;
        serialObj = new SerializedObject(scriptableObj);
    }

    private void DrawUpdaterWindow()
    {
        viewScrollPosition = EditorGUILayout.BeginScrollView(viewScrollPosition, false, false);
        
        var serialProp = serialObj.FindProperty("armorPieces");
        EditorGUILayout.PropertyField(serialProp, true);
        
        GUILayout.Space(7);
        newArmature = (Transform) EditorGUILayout.ObjectField("New Armature (Hips)", newArmature, typeof(Transform), true);
        GUILayout.Space(7);
        newParent = (Transform) EditorGUILayout.ObjectField("New Parent", newParent, typeof(Transform), true);
        GUILayout.Space(15);
        if (GUILayout.Button("Attach Armor Pieces", GUILayout.MinWidth(150), GUILayout.MinHeight(30),
            GUILayout.ExpandWidth(true)))
        {
            TriggerUpdater();
        }
        
        serialObj.ApplyModifiedProperties();
        
        GUILayout.Space(20);
        GUILayout.EndScrollView();
    }

    private void TriggerUpdater()
    {
        foreach (var t in armorPieces)
        {
            string cachedRootBoneName = t.rootBone.name;
            var newBones = new Transform[t.bones.Length];
            for (var x = 0; x < t.bones.Length; x++)
                foreach (var newBone in newArmature.GetComponentsInChildren<Transform>())
                    if (newBone.name == t.bones[x].name)
                    {
                        newBones[x] = newBone;
                    }

            Transform matchingRootBone = GetRootBoneByName(newArmature, cachedRootBoneName);
            t.rootBone = matchingRootBone != null ? matchingRootBone : newArmature.transform;
            t.bones = newBones;
            Transform transform;
            (transform = t.transform).SetParent(newParent);
            transform.localPosition = Vector3.zero;
        }
        
    }
    
    Transform GetRootBoneByName(Transform parentTransform, string name) {

        foreach (var transformChild in parentTransform.GetComponentsInChildren<Transform>())
        {
            if(transformChild.name != name) continue;
            return transformChild;
        }

        return null;
    }
}
