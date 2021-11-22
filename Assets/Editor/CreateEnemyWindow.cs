using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEditor;
using UnityEngine;

public class CreateEnemyWindow : EditorWindow
{
    private string _name = "";
    private int _level = 0;
    private float _maxHealth = 50;
    private float _movementSpeed = 5.4f;
    
    [MenuItem("Window/Enemy Creator")]
    public static void ShowWindow()
    {
        GetWindow<CreateEnemyWindow>("Enemy creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy stats", EditorStyles.boldLabel);

        _name = EditorGUILayout.TextField("Name", _name);
        _level = EditorGUILayout.IntField("Level", _level);
        _maxHealth = EditorGUILayout.FloatField("Max health", _maxHealth);
        _maxHealth = EditorGUILayout.FloatField("Movement speed", _movementSpeed);

        if (GUILayout.Button("Create"))
        {
            NPCData data = new NPCData(_name, _level, _maxHealth, _movementSpeed);
            EntityManager._allData.Add(data);
        }
    }
}
