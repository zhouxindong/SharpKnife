using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpKnife.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SharpKnife.Diagnostics.Tests
{
    [TestClass()]
    public class ObjectDumperTests
    {
        [TestMethod()]
        public void WriteTest()
        {
            var processes = new List<string>();
            foreach (var process in Process.GetProcesses())
            {
                processes.Add(process.ProcessName);
            }
            ObjectDumper.Write(processes);
        }

        [TestMethod]
        public void WriteTest2()
        {
            var processes = new List<ProcessData>();
            foreach (var process in Process.GetProcesses())
            {
                var data = new ProcessData();
                data.Id = process.Id;
                data.Name = process.ProcessName;
                data.Memory = process.WorkingSet64;
                processes.Add(data);
            }

            ObjectDumper.Write(processes);
        }
    }

    public class ProcessData
    {
        public int Id;
        public long Memory;
        public string Name;
    }
}