﻿using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TypeScriptDefToCSharp;

namespace DotNetForHtml5.Compiler
{
    public class TypeScriptDefToCSharp : Task
    {
        public string InputFiles { get; set; }

        [Required]
        public string OutputDirectory { get; set; }

        [Output]
        public ITaskItem[] GeneratedFiles { get; set; }

        public bool NoRecompile { get; set; }

        public override bool Execute()
        {
            if (InputFiles == null)
                return true;
            ILogger logger = new LoggerThatUsesTaskOutput(this);
            string operationName = "C#/XAML for HTML5: TypeScriptDefToCSharp";
            try
            {
                // Execute the task if needed
                if (NoRecompile == false)
                    ProcessTypeScriptDef.ProcessTypeScriptDefFile(this.InputFiles, this.OutputDirectory, logger);

                // Get the file list from the XML
                List<string> ListOfGeneratedFiles = new List<string>();
                if (File.Exists(OutputDirectory + global::TypeScriptDefToCSharp.Constants.NAME_OF_TYPESCRIPT_DEFINITIONS_CACHE_FILE))
                {
                    // Read the XML
                    string content = File.ReadAllText(OutputDirectory + global::TypeScriptDefToCSharp.Constants.NAME_OF_TYPESCRIPT_DEFINITIONS_CACHE_FILE);
                    // Deserialize
                    TypeScriptDefToCSharpOutput output = Tool.Deserialize<TypeScriptDefToCSharpOutput>(content);

                    // Store every generated file in our list
                    foreach (var file in output.TypeScriptDefinitionFiles)
                    {
                        ListOfGeneratedFiles.AddRange(file.CSharpGeneratedFiles);
                    }
                }
                // Convert them to get the output
                GeneratedFiles = ListOfGeneratedFiles.Select(s => new TaskItem() { ItemSpec = s }).ToArray();

                logger.WriteMessage("Output directory: " + this.OutputDirectory, MessageImportance.High);
            }
            catch (Exception ex)
            {
                logger.WriteError(operationName + " failed: " + ex.ToString());
                return false;
            }
            return true;
        }
    }
}
