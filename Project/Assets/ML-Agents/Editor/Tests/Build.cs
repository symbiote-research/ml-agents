using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

namespace Unity.MLAgents
{
    public class Build
    {
        const string k_EnvironmentName = "-env";

        public static void BuildEnv()
        {
            var env = "";
            var scenePath = "";

            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == k_EnvironmentName)
                {
                    env = args[i + 1];
                    scenePath = $"Assets/ML-Agents/Examples/{env}/Scenes/{env}.unity";
                    System.Console.WriteLine($"Using scene from {scenePath}");
                }
            }

            if (env == "" || scenePath == "") {
                System.Console.WriteLine("Env is empty!!!");
                EditorApplication.Exit(1);
            }

            var outputPath = $"Linux64/{env}";
            System.Console.WriteLine($"outputPath: {scenePath}");

            string[] scenes = { scenePath };
            var buildResult = BuildPipeline.BuildPlayer(
                scenes,
                outputPath,
                BuildTarget.StandaloneLinux64,
                BuildOptions.None
            );
            var isOk = buildResult.summary.result == BuildResult.Succeeded;
            var error = "";
            foreach (var stepInfo in buildResult.steps)
            {
                foreach (var msg in stepInfo.messages)
                {
                    if (msg.type != LogType.Log && msg.type != LogType.Warning)
                    {
                        error += msg.content + "\n";
                    }
                }
            }
            if (isOk)
            {
                EditorApplication.Exit(0);
            }
            else
            {
                Console.Error.WriteLine(error);
                EditorApplication.Exit(1);

            }
            Debug.Log(error);

        }

    }
}
