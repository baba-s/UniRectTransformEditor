using KoganeUnityLib.Internal;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace KoganeUnityLib
{
	[CustomEditor( typeof( RectTransform ), true )]
	public class RectTransformEditor : Editor
	{
		private const string RECORD_NAME = nameof( RectTransformEditor );

		private Editor     editorInstance;
		private Type       nativeEditor;
		private MethodInfo onSceneGui;
		private MethodInfo onValidate;

		public override void OnInspectorGUI()
		{
			editorInstance.OnInspectorGUI();

			var rectTransform = target as RectTransform;

			if ( rectTransform == null ) return;

			var oldEnabled = GUI.enabled;

			using ( new EditorGUILayout.HorizontalScope( EditorStyles.helpBox ) )
			{
				GUI.enabled = rectTransform.localRotation != Quaternion.identity;
				if ( GUILayout.Button( "Reset Rotation" ) )
				{
					Undo.RecordObject( rectTransform, RECORD_NAME );
					rectTransform.localRotation = Quaternion.identity;
				}

				GUI.enabled = rectTransform.localScale != Vector3.one;
				if ( GUILayout.Button( "Reset Scale" ) )
				{
					Undo.RecordObject( rectTransform, RECORD_NAME );
					rectTransform.localScale = Vector3.one;
				}

				GUI.enabled =
					rectTransform.anchorMin != Vector2.zero ||
					rectTransform.anchorMax != Vector2.one ||
					rectTransform.offsetMin != Vector2.zero ||
					rectTransform.offsetMax != Vector2.zero ||
					rectTransform.pivot != new Vector2( 0.5f, 0.5f ) ||
					rectTransform.rotation != Quaternion.identity ||
					rectTransform.localScale != Vector3.one
					;

				if ( GUILayout.Button( "Fill" ) )
				{
					Undo.RecordObject( rectTransform, RECORD_NAME );

					rectTransform.anchorMin  = Vector2.zero;
					rectTransform.anchorMax  = Vector2.one;
					rectTransform.offsetMin  = Vector2.zero;
					rectTransform.offsetMax  = Vector2.zero;
					rectTransform.pivot      = new Vector2( 0.5f, 0.5f );
					rectTransform.rotation   = Quaternion.identity;
					rectTransform.localScale = Vector3.one;
				}

				GUI.enabled =
					rectTransform.localPosition.HasAfterDecimalPoint() ||
					rectTransform.sizeDelta.HasAfterDecimalPoint() ||
					rectTransform.offsetMin.HasAfterDecimalPoint() ||
					rectTransform.offsetMax.HasAfterDecimalPoint() ||
					rectTransform.localScale.HasAfterDecimalPoint()
					;

				if ( GUILayout.Button( "Round" ) )
				{
					Undo.RecordObject( rectTransform, RECORD_NAME );

					rectTransform.localPosition = rectTransform.localPosition.Round();
					rectTransform.sizeDelta     = rectTransform.sizeDelta.Round();
					rectTransform.offsetMin     = rectTransform.offsetMin.Round();
					rectTransform.offsetMax     = rectTransform.offsetMax.Round();
					rectTransform.localScale    = rectTransform.localScale.Round();
				}
			}

			var creator = new ComponentButtonCreator( rectTransform.gameObject );

			using ( new EditorGUILayout.HorizontalScope( EditorStyles.helpBox ) )
			{
				creator.Create<CanvasGroup, CanvasGroup>( "CanvasGroup Icon" );
				creator.Create<HorizontalLayoutGroup, LayoutGroup>( "HorizontalLayoutGroup Icon" );
				creator.Create<VerticalLayoutGroup, LayoutGroup>( "VerticalLayoutGroup Icon" );
				creator.Create<GridLayoutGroup, LayoutGroup>( "GridLayoutGroup Icon" );
				creator.Create<ContentSizeFitter, ContentSizeFitter>( "ContentSizeFitter Icon" );
			}

			GUI.enabled = oldEnabled;
		}

		private void OnEnable()
		{
			if ( nativeEditor == null ) Initialize();

			nativeEditor.GetMethod( "OnEnable", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic )?.Invoke( editorInstance, null );
			onSceneGui = nativeEditor.GetMethod( "OnSceneGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
			onValidate = nativeEditor.GetMethod( "OnValidate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
		}

		private void OnSceneGUI()
		{
			onSceneGui.Invoke( editorInstance, null );
		}

		private void OnDisable()
		{
			nativeEditor.GetMethod( "OnDisable", BindingFlags.NonPublic | BindingFlags.Instance )?.Invoke( editorInstance, null );
		}

		private void Awake()
		{
			Initialize();
			nativeEditor.GetMethod( "Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic )?.Invoke( editorInstance, null );
		}

		private void Initialize()
		{
			nativeEditor   = Assembly.GetAssembly( typeof( Editor ) ).GetType( "UnityEditor.RectTransformEditor" );
			editorInstance = CreateEditor( target, nativeEditor );
		}

		private void OnDestroy()
		{
			nativeEditor.GetMethod( "OnDestroy", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic )?.Invoke( editorInstance, null );
		}

		private void OnValidate()
		{
			if ( nativeEditor == null ) Initialize();

			onValidate?.Invoke( editorInstance, null );
		}

		private void Reset()
		{
			if ( nativeEditor == null ) Initialize();

			nativeEditor.GetMethod( "Reset", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic )?.Invoke( editorInstance, null );
		}

		private sealed class ComponentButtonCreator
		{
			private readonly GameObject m_gameObject;

			public ComponentButtonCreator( GameObject gameObject ) => m_gameObject = gameObject;

			public void Create<T1, T2>( string iconName )
				where T1 : Component
				where T2 : Component
			{
				var hasComponent = m_gameObject.GetComponent<T2>() != null;

				GUI.enabled = !hasComponent;

				if ( GUILayout.Button( EditorGUIUtility.IconContent( iconName ) ) )
				{
					Undo.AddComponent<T1>( m_gameObject );
				}
			}
		}
	}
}

//	[CustomEditor( typeof( RectTransform ) )]
//	internal sealed class RectTransformEditor : Editor
//	{
//		private const string RECORD_NAME = nameof( RectTransformEditor );

//		private static readonly Type DEFAULT_EDITOR_TYPE = typeof( Editor )
//			.Assembly
//			.GetType( "UnityEditor.RectTransformEditor" );

//		private Editor m_defaultEditor;

//		public override void OnInspectorGUI()
//		{
//			if ( m_defaultEditor == null )
//			{
//				m_defaultEditor = CreateEditor( target, DEFAULT_EDITOR_TYPE );
//			}

//			m_defaultEditor.OnInspectorGUI();

//		}

//	}
//}