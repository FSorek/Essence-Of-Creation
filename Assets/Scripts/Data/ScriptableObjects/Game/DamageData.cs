using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Data_Types;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data.ScriptableObjects.Game
{
    [CreateAssetMenu(fileName = "Damage Data", menuName = "Essence/TowerAttack/Damage Data")]
    public class DamageData : SerializedScriptableObject
    {
        [OdinSerialize]private Dictionary<DamageType, int> damages;
        public Dictionary<DamageType, int> Damages => damages;
    }

    public class DamageEditor : Editor
    {
        private Dictionary<DamageType, int> damageDictionary;
        

        private List<DamageType> damageTypes;
        private void OnEnable()
        {
            if(damageDictionary == null)
                damageDictionary = new Dictionary<DamageType, int>();
            if (damageTypes == null)
            {
                damageTypes = new List<DamageType>();
                var assets = AssetDatabase.FindAssets("t:DamageType", new[] {@"Assets\ScriptableObjects\Global\DamageTypes"});
                foreach (var guid in assets) 
                {
                    var damageType = AssetDatabase.LoadAssetAtPath<DamageType>(AssetDatabase.GUIDToAssetPath(guid));
                    damageTypes.Add(damageType);
                    damageDictionary.Add(damageType,0);
                }
            }
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            foreach (var damageType in damageTypes)
            {
                damageDictionary[damageType] = EditorGUILayout.IntField(damageType.name, damageDictionary[damageType]);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}