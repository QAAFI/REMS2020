using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using WindowsClient.Controls;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages a users current session with a database
    /// </summary>
    public class Session
    {
        /// <summary>
        /// The path to the connected database
        /// </summary>
        public string DB { get; set; }

        /// <inheritdoc/>
        public override string ToString() => Path.GetFileName(DB);
    }
}
