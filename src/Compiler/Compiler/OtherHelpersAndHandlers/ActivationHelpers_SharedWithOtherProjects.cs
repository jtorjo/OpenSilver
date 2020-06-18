﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetForHtml5
{
    internal static class ActivationHelpers
    {
        internal static bool IsFeatureEnabled(string featureId)
        {
            object value = RegistryHelpers.GetSetting("Feature_" + featureId, null);
            if (value != null)
                return true; //todo: to prevent registry tampering, we should retrieve the activation key and check with the server that it is ok.
            else
                return false;
        }

        internal static string GetActivationAppPath()
        {
#if BRIDGE
            // the activation app should be in the package, in the same directory as the assembly of the simulator
            string assemblyPath = Path.GetDirectoryName(PathsHelper.GetPathOfThisVeryAssembly());
            return Path.Combine(assemblyPath, "CSharpXamlForHtml5.Activation.exe");
#else
            // otherwise we use the activation app of internal stuff
            return PathsHelper.GetActivationAppPath();
#endif
        }

        internal static void DisplayActivationApp(string activationAppPath, string missingFeatureId, string messageForMissingFeature)
        {
            string fullPathToActivationApp = Path.Combine(Directory.GetCurrentDirectory(), activationAppPath);
            //string arguments = "/custom " + missingFeatureId + " " + messageForMissingFeature;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path.GetFileName(fullPathToActivationApp);
            psi.WorkingDirectory = Path.GetDirectoryName(fullPathToActivationApp);
            //psi.Arguments = arguments;
            Process proc = Process.Start(psi);
            proc.WaitForExit();
        }
    }
}
