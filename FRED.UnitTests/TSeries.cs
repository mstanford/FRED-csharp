// ------------------------------------------------------------------------
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>
// 
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FRED
{
    [TestFixture]
    public class TSeries
    {

        [Test]
        public void Parse01()
        {
            System.IO.Stream stream = LoadResource("series.xml");
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(stream);

            Series series = new Series();
            series.Parse(xmlDocument);

            Assert.AreEqual(1, series.Id.Count);
            Assert.AreEqual(1, series.Title.Count);
            Assert.AreEqual(1, series.ObservationStart.Count);
            Assert.AreEqual(1, series.ObservationEnd.Count);
            Assert.AreEqual(1, series.Frequency.Count);
            Assert.AreEqual(1, series.FrequencyShort.Count);
            Assert.AreEqual(1, series.Units.Count);
            Assert.AreEqual(1, series.UnitsShort.Count);
            Assert.AreEqual(1, series.SeasonalAdjustment.Count);
            Assert.AreEqual(1, series.SeasonalAdjustmentShort.Count);
            Assert.AreEqual(1, series.LastUpdated.Count);
        }

        private static System.IO.Stream LoadResource(string name) { return typeof(TSeries).Assembly.GetManifestResourceStream("ALEO." + typeof(TSeries).Namespace + ".samples." + name); }

    }
}
