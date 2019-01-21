using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpKnife.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpKnife.Extensions.Tests
{
    [TestClass()]
    public class StreamExTests
    {
        [TestMethod()]
        public void LinesTest()
        {
            var tmp_file = Path.GetTempFileName();
            var writer = new StreamWriter(tmp_file);

            for (int i = 0; i < 1000000; i++)
            {
                writer.WriteLine($"line{i}");
            }
            writer.Close();

            var reader = new StreamReader(tmp_file);
            var count = 0;
            foreach (var line in reader.Lines())
            {
                Assert.AreEqual($"line{count++}", line);
            }

            reader.Close();
            File.Delete(tmp_file);
        }

        [TestMethod()]
        public void AllLinesTest()
        {
            var tmp_file = Path.GetTempFileName();
            var writer = new StreamWriter(tmp_file);

            for (int i = 0; i < 100; i++)
            {
                writer.WriteLine($"line{i}");
            }
            writer.Close();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var read_from_file = File.ReadAllText(tmp_file);
            watch.Stop();
            Console.WriteLine($"Use File.ReadAllText() {watch.Elapsed}");

            watch.Restart();
            var stream = new StreamReader(tmp_file);
            var read_from_stream = stream.AllText();
            watch.Stop();
            Console.WriteLine($"Use Stream.AllText() {watch.Elapsed}");

            stream.Close();
            File.Delete(tmp_file);
            Assert.AreEqual(read_from_file, read_from_stream);
        }
    }
}