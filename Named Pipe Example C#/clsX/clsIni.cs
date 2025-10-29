using System.Runtime.InteropServices;
using System.Text;

namespace Named_Pipe_Example.clsX
{
    public class clsIni
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        public string Path { get; private set; }

        /// <summary>
        /// IniFile Constructor
        /// </summary>
        /// <param name="IniPath">The path to the INI file.</param>
        public clsIni(string IniPath)
        {
            Path = IniPath;
        }

        /// <summary>
        /// Read value from INI file
        /// </summary>
        /// <param name="section">The section of the file to look in</param>
        /// <param name="key">The key in the section to look for</param>
        /// <returns></returns>
        public string ReadValue(string section, string key)
        {
            var sb = new StringBuilder(5000);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, Path);
            return sb.ToString();
        }

        /// <summary>
        /// Write value to INI file
        /// </summary>
        /// <param name="section">The section of the file to write in</param>
        /// <param name="key">The key in the section to write</param>
        /// <param name="value">The value to write for the key</param>
        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }
    }
}
