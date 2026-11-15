// Copyright (c) 2026 Roger Brown.
// Licensed under the MIT License.

using System;
using System.Management.Automation;

namespace RhubarbGeekNz.EnvironmentalConcerns
{
    [Cmdlet(VerbsData.Expand, "EnvironmentVariables")]
    [OutputType(typeof(string))]
    sealed public class ExpandEnvironmentVariables : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "input string", Position = 0)]
        public string InputString;

        protected override void ProcessRecord()
        {
            WriteObject(Environment.ExpandEnvironmentVariables(InputString));
        }
    }
}
