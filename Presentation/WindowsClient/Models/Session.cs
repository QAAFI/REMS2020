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

        /// <summary>
        /// If information has been loaded in the session
        /// </summary>
        public bool HasInformation { get; set; } = false;

        /// <summary>
        /// If experiments have been loaded in the session
        /// </summary>
        public bool HasExperiments { get; set; } = false;

        /// <summary>
        /// If experiments have been loaded in the session
        /// </summary>
        public bool HasData { get; set; } = false;

        /// <inheritdoc/>
        public override string ToString() => Path.GetFileName(DB);
    }
}
