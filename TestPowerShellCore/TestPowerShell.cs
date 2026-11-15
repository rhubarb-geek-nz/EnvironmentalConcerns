// Copyright (c) 2026 Roger Brown.
// Licensed under the MIT License.

#if NETCOREAPP
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace RhubarbGeekNz.EnvironmentalConcerns
{
    [TestClass]
    public class UnitTests
    {
        readonly InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
        public UnitTests()
        {
            foreach (Type t in new Type[] {
                typeof(ExpandEnvironmentVariables)
            })
            {
                CmdletAttribute ca = t.GetCustomAttribute<CmdletAttribute>();

                if (ca == null) throw new NullReferenceException();

                initialSessionState.Commands.Add(new SessionStateCmdletEntry($"{ca.VerbName}-{ca.NounName}", t, ca.HelpUri));
            }

            initialSessionState.Variables.Add(new SessionStateVariableEntry("ErrorActionPreference", ActionPreference.Stop, "Stop action"));
        }

        [TestMethod]
        public void TestInputString()
        {
            using (PowerShell powerShell = PowerShell.Create(initialSessionState))
            {
                powerShell.AddCommand("Expand-EnvironmentVariables").AddParameter("InputString","%PATH%");

                var outputPipeline = powerShell.Invoke();

                Assert.AreEqual(1, outputPipeline.Count);

                var result = outputPipeline[0];

                Assert.AreEqual(Environment.GetEnvironmentVariable("PATH"), result.BaseObject);
            }
        }

        [TestMethod]
        public void TestArgument()
        {
            using (PowerShell powerShell = PowerShell.Create(initialSessionState))
            {
                powerShell.AddScript("Expand-EnvironmentVariables %PATH%");

                var outputPipeline = powerShell.Invoke();

                Assert.AreEqual(1, outputPipeline.Count);

                var result = outputPipeline[0];

                Assert.AreEqual(Environment.GetEnvironmentVariable("PATH"), result.BaseObject);
            }
        }

        [TestMethod]
        public void TestStringArgument()
        {
            using (PowerShell powerShell = PowerShell.Create(initialSessionState))
            {
                powerShell.AddScript("Expand-EnvironmentVariables '%PATH%'");

                var outputPipeline = powerShell.Invoke();

                Assert.AreEqual(1, outputPipeline.Count);

                var result = outputPipeline[0];

                Assert.AreEqual(Environment.GetEnvironmentVariable("PATH"), result.BaseObject);
            }
        }
    }
}
