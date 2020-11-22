using System.IO;
using UGF.Update.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;

namespace UGF.Module.Update.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/OutputPlayerLoopScript")]
    public class OutputPlayerLoopScript : ScriptableObject
    {
        [ContextMenu("Run", false, 1000)]
        public void Run()
        {
            string path = AssetDatabase.GetAssetPath(this);
            string folder = Path.GetDirectoryName(path);
            PlayerLoopSystem playerLoop = PlayerLoop.GetDefaultPlayerLoop();
            string output = playerLoop.Print();
            string outputPath = $"{folder}/{name}.txt";

            File.WriteAllText(outputPath, output);
            AssetDatabase.ImportAsset(outputPath);
        }
    }
}
