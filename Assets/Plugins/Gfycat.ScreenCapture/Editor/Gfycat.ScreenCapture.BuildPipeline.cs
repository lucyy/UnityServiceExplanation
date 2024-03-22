
#if UNITY_IOS && UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Gfycat.ScreenCapture
{
	public class BuildPipeline
	{
 		[PostProcessBuild(700)]
     	public static void OnPostProcessBuild(BuildTarget target, string path)
     	{
        	if (target != BuildTarget.iOS)
         	{
             	return;
         	}
 
			string projectPath = PBXProject.GetPBXProjectPath(path);
			PBXProject project = new PBXProject();
			project.ReadFromString(File.ReadAllText(projectPath));
			string targetName = PBXProject.GetUnityTargetName();
			string targetGUID = project.TargetGuidByName(targetName);
			project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC"); 
			File.WriteAllText(projectPath, project.WriteToString());
    	}
	}
}

#endif
