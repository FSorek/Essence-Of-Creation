using System;
using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObjects.Attacks;
using Monobehaviors.Essences;
using Monobehaviors.Essences.Attacks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class OdinEssenceCreatorWindow : OdinEditorWindow
    {
        [OnValueChanged(nameof(EssenceNameChanged))]
        public string essenceName;
        private GameObject selectedEssence;
        
        private Transform essencesParent;
        
        [EnumToggleButtons][OnValueChanged(nameof(TierChanged))]
        public TiersEnum tier;
        [AssetSelector(Paths = "Assets/ScriptableObjects")]
        public AttackBehaviour attackBehaviour;

        private GameObject originalEssence;
        private Scene essenceCreatorScene;
        private int essenceCount;
        private float distanceOffset = 10f;
        

        private void TierChanged()
        {
            if (selectedEssence == null)
                return;
            var tierParent = essencesParent.Find(tier.ToString());
            selectedEssence.transform.SetParent(tierParent);
            EditorSceneManager.MarkSceneDirty(essenceCreatorScene);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            originalEssence = AssetDatabase.LoadAssetAtPath<GameObject>(@"Assets\Prefabs\ForgedEssences\Originals\Essence.prefab");
        
            essenceCreatorScene = SceneManager.GetSceneByName("EssenceCreator");
            foreach (var gameObject in essenceCreatorScene.GetRootGameObjects())
            {
                if (gameObject.name == "Essences")
                    essencesParent = gameObject.transform;
            }

            essenceCount = 0;
            for (int i = 0; i < essencesParent.childCount; i++)
            {
                var child = essencesParent.GetChild(i);
                if (child.GetComponent<Essence>()) continue;
                for (int j = 0; j < child.childCount; j++)
                {
                    var childOfChild = child.GetChild(j);
                    if (!childOfChild.GetComponent<Essence>()) continue;
                    essenceCount++;
                }
            }
        }

        [MenuItem("Essence/OdinCreator")]
        public static void OpenWindow()
        {
            GetWindow<OdinEssenceCreatorWindow>("Essence Creator");
        }

        [OnInspectorGUI]
        private void DrawHierarchy()
        {
            for (int i = 0; i < essencesParent.childCount; i++)
            {
                var child = essencesParent.GetChild(i);
                if (!child.GetComponent<Essence>())
                {
                    GUILayout.Label(">" + child.name);
                    for (int j = 0; j < child.childCount; j++)
                    {
                        var childOfChild = child.GetChild(j);
                        if (childOfChild.GetComponent<Essence>())
                        {
                            if (GUILayout.Button(childOfChild.name, GUILayout.Width(150)))
                            {
                                selectedEssence = childOfChild.gameObject;
                                essenceName = string.Join(" ", selectedEssence.name.Split(' ').Skip(2));
                            }
                        }
                    }
                }
            }
        }

        [Button("+")]
        private void AddNewEssence()
        {
            selectedEssence = Instantiate(originalEssence);
            var verticalPos = Mathf.Floor(essenceCount / 6f) * distanceOffset;
            var horizontalPos = (essenceCount % 6) * distanceOffset;
            selectedEssence.transform.position = new Vector3(horizontalPos, 2f, verticalPos);
            SceneManager.MoveGameObjectToScene(selectedEssence, essenceCreatorScene);
            var tierParent = essencesParent.Find(tier.ToString());
            selectedEssence.transform.SetParent(tierParent);
            essenceCount++;
        }

        [EnumToggleButtons] [SerializeField] [OnValueChanged(nameof(AttackTypeChanged))] private AttackType attackType;
        [SerializeField][InlineEditor(InlineEditorObjectFieldModes.Hidden)] private AttackBehaviour selectedAttack;
        private enum AttackType
        {
            SingleTarget,
            AreaOfEffect,
        }
        private void AttackTypeChanged()
        {
            switch (attackType)
            {
                case AttackType.SingleTarget:
                    selectedAttack = CreateInstance<SingleAttackBehaviour>();
                    break;
                case AttackType.AreaOfEffect:
                    selectedAttack = CreateInstance<AreaAttackBehaviour>();
                    break;
            }
        }

        private string attackFilePath = @"Assets\ScriptableObjects\ForgedEssence\";
        [Button("Save Prefab")]
        private void SavePrefab()
        {
            var currentAttackFilePath = attackFilePath + tier + $"/Eo{essenceName}";
            if (!AssetDatabase.IsValidFolder(attackFilePath))
            {
                AssetDatabase.CreateFolder(attackFilePath + tier, $"Eo{essenceName}");
            }
            AssetDatabase.CreateAsset(selectedAttack, currentAttackFilePath + $"/Eo{essenceName} {attackType} .asset");
        }
        
        private void EssenceNameChanged()
        {
            if (selectedEssence != null)
                selectedEssence.name = "Essence of " + essenceName;
        }
        
        public enum TiersEnum
        {
            Tier1,
            Tier2,
            Tier3,
            Tier4,
        }
    }
}
