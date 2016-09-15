using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Linq;
using UnityEditor.SceneManagement;

public class CommonTest
{

    /// <summary>
    /// Sceneを開きそのときロードされているオブジェクトから参照破損をチェックする
    /// "/scenePath Assets/Scenes/main.unity"のようにコマンドライン引き数で対象シーンを指定できる
    /// 指定しない場合はすべてのシーンのチェックを行う
    /// </summary>
	[Test]
    public void FindMissingScript()
    {
        var setups = EditorSceneManager.GetSceneManagerSetup();

        var args = System.Environment.GetCommandLineArgs().ToList();
        var idx = args.IndexOf("/scenePath");

        var pathes = (idx >= 0)
            ? new[] { args[idx + 1] }
            : AssetDatabase.FindAssets("t:scene").Select(guid => AssetDatabase.GUIDToAssetPath(guid));

        pathes.ToList().ForEach(path =>
        {
            var scene = EditorSceneManager.OpenScene(path);
            var brokenList = Resources.FindObjectsOfTypeAll<GameObject>().Where(c => c.GetComponents<Component>().Any(o => o == null)).ToList();

            Assert.IsFalse(brokenList.Any(), "Scene:[" + scene.name + "] " + string.Join(",", brokenList.OrderBy(c => c.name).Select(c => c.name).ToArray()));
        });

        if (setups.Any())
        {
            EditorSceneManager.OpenScene(setups.First().path, OpenSceneMode.Single);
            setups.Skip(1).ToList().ForEach(setup => EditorSceneManager.OpenScene(setup.path, OpenSceneMode.Additive));
        }
    }
}