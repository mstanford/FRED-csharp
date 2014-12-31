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

namespace FRED
{
    public class ReleaseDates : Request
    {
        public readonly List<string> Id = new List<string>();
        public readonly List<string> Date = new List<string>();

        public ReleaseDates(string api_key, string release_id) : base("http://api.stlouisfed.org/fred/release/dates?release_id=" + release_id + "&api_key=" + api_key) { }
        public ReleaseDates() { }

        public override void Parse(System.Xml.XmlDocument xmlDocument)
        {
            switch (xmlDocument.DocumentElement.Name)
            {
                case "release_dates":
                    foreach (System.Xml.XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                    {
                        if (xmlNode is System.Xml.XmlElement)
                        {
                            System.Xml.XmlElement xmlElement = (System.Xml.XmlElement)xmlNode;

                            switch (xmlElement.Name)
                            {
                                case "release_date":
                                    Id.Add(xmlElement.Attributes["release_id"].Value);
                                    Date.Add(xmlElement.InnerText);
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                        }
                    }
                    break;
                default:
                    throw new System.Exception();
            }
        }

    }
}
