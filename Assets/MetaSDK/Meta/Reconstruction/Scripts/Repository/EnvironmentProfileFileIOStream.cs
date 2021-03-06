// Copyright Â© 2018, Meta Company.  All rights reserved.
// 
// Redistribution and use of this software (the "Software") in binary form, without modification, is 
// permitted provided that the following conditions are met:
// 
// 1.      Redistributions of the unmodified Software in binary form must reproduce the above 
//         copyright notice, this list of conditions and the following disclaimer in the 
//         documentation and/or other materials provided with the distribution.
// 2.      The name of Meta Company (â€œMetaâ€) may not be used to endorse or promote products derived 
//         from this Software without specific prior written permission from Meta.
// 3.      LIMITATION TO META PLATFORM: Use of the Software is limited to use on or in connection 
//         with Meta-branded devices or Meta-branded software development kits.  For example, a bona 
//         fide recipient of the Software may incorporate an unmodified binary version of the 
//         Software into an application limited to use on or in connection with a Meta-branded 
//         device, while he or she may not incorporate an unmodified binary version of the Software 
//         into an application designed or offered for use on a non-Meta-branded device.
// 
// For the sake of clarity, the Software may not be redistributed under any circumstances in source 
// code form, or in the form of modified binary code â€“ and nothing in this License shall be construed 
// to permit such redistribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL META COMPANY BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
using System.IO;
using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Uses a file to reads/writes the environment profiles data.
    /// </summary>
    public class EnvironmentProfileFileIOStream : IEnvironmentProfileIOStream
    {
        private readonly string _path;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileFileIOStream"/> class.
        /// </summary>
        /// <param name="path">Path of the file used to read/write the environment profiles data.</param>
        public EnvironmentProfileFileIOStream(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            _path = path;
        }

        /// <summary>
        /// Reads the environment profiles data from a file.
        /// </summary>
        /// <returns>The environment profiles data</returns>
        public string Read()
        {
            Debug.Assert(!string.IsNullOrEmpty(_path));
            try
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    string fileContent = reader.ReadToEnd();
                    return fileContent;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Writes a file with environment profiles data.
        /// </summary>
        public void Write(string content)
        {
            Debug.Assert(!string.IsNullOrEmpty(_path));

            // Creates the directory if it doesn't exists.
            FileInfo fileInfo = new FileInfo(_path);
            if (fileInfo.Directory != null)
            {
                fileInfo.Directory.Create();
            }

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(content);
            }
        }
    }
}
