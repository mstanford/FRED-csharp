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
    public abstract class Request
    {

        public Request(string url)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            HttpGet(url, memoryStream);
            memoryStream.Position = 0;
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(memoryStream);
            Parse(xmlDocument);
        }

        public Request() { }

        public abstract void Parse(System.Xml.XmlDocument xmlDocument);

        private static System.IO.MemoryStream HttpGet(string url, System.IO.MemoryStream memoryStream)
        {
            System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
            {
                System.IO.Stream responseStream = httpWebResponse.GetResponseStream();

                byte[] buffer = new byte[8192];
                int bytesWritten = 0;
                int length = (int)httpWebResponse.ContentLength;
                if (length == -1)
                {
                    while (true)
                    {
                        int bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            memoryStream.Write(buffer, 0, bytesRead);
                            bytesWritten += bytesRead;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    while (bytesWritten < length)
                    {
                        int bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            memoryStream.Write(buffer, 0, bytesRead);
                            bytesWritten += bytesRead;
                        }
                    }
                }

                //memoryStream.Flush();
                //memoryStream.Close();
            }
            httpWebResponse.Close();

            return memoryStream;
        }

    }
}
