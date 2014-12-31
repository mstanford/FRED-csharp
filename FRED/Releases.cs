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
    public class Releases : Request
    {
        public readonly List<string> Id = new List<string>();
        public readonly List<string> Name = new List<string>();
        public readonly List<string> Link = new List<string>();

        public Releases(string api_key) : base("http://api.stlouisfed.org/fred/releases?api_key=" + api_key) { }
        public Releases() { }

        public override void Parse(System.Xml.XmlDocument xmlDocument)
        {
            switch (xmlDocument.DocumentElement.Name)
            {
                case "releases":
                    foreach (System.Xml.XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                    {
                        if (xmlNode is System.Xml.XmlElement)
                        {
                            System.Xml.XmlElement xmlElement = (System.Xml.XmlElement)xmlNode;

                            switch (xmlElement.Name)
                            {
                                case "release":
                                    Id.Add(xmlElement.Attributes["id"].Value);
                                    Name.Add(xmlElement.Attributes["name"].Value);
                                    Link.Add(xmlElement.HasAttribute("link") ? xmlElement.Attributes["link"].Value : "");
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
