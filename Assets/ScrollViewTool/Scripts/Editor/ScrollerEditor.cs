using UnityEngine;
using System.Collections;
using UnityEditor;

namespace yoyohan
{
    // 确定我们需要自定义编辑器的组件
    [CustomEditor(typeof(CellViewBase), true)]
    public class CellViewBaseEditor : Editor
    {
        // 序列化对象
        private SerializedObject so;

        // 序列化属性
        private SerializedProperty cellIdentifier;
        private SerializedProperty scrollerCtrl;
        private SerializedProperty gridCount;
        private SerializedProperty gridPrefab;
        private SerializedProperty needInstantiate;
        private SerializedProperty lisCellGrid;

        void OnEnable()
        {
            // 获取当前的序列化对象（target：当前检视面板中显示的对象）
            so = new SerializedObject(target);

            // 抓取对应的序列化属性
            cellIdentifier = so.FindProperty("cellIdentifier");
            scrollerCtrl = so.FindProperty("scrollerCtrl");
            gridCount = so.FindProperty("gridCount");
            gridPrefab = so.FindProperty("gridPrefab");
            needInstantiate = so.FindProperty("needInstantiate");
            lisCellGrid = so.FindProperty("lisCellGrid");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            // 从物体上抓取最新的信息
            so.Update();

            EditorGUI.BeginDisabledGroup(true);
            string scriptPath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(target.GetType().Name)[0]);
            EditorGUILayout.ObjectField("Script", AssetDatabase.LoadAssetAtPath<TextAsset>(scriptPath), typeof(TextAsset), true);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(cellIdentifier, new GUIContent("CellIdentifier", "Cell的id 用于区分对象池"));
            EditorGUILayout.PropertyField(scrollerCtrl);
       
            if (((ScrollerCtrlBase)scrollerCtrl.objectReferenceValue).isGridModel)
            {
                EditorGUILayout.PropertyField(gridCount);
                EditorGUILayout.PropertyField(needInstantiate);
                if (needInstantiate.boolValue == true)
                {
                    EditorGUILayout.PropertyField(gridPrefab);
                }
                else
                {
                    EditorGUILayout.PropertyField(lisCellGrid, true);
                }
            }


            so.ApplyModifiedProperties();
        }
    }



}