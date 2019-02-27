using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Enssi
{
    /// <summary>
    /// 读写配置文件
    /// </summary>
    public class ApiAutuIniConfig
    {
        private static string inipath = AppDomain.CurrentDomain.BaseDirectory + @"\Config.ini";
        //public static IniConfig Singleton = new IniConfig();

        private static string Inipath
        {
            get
            {
                return inipath;
            }
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读
        /// </summary>
        /// <param name="Section">父节点</param>
        /// <param name="key">名称</param>
        /// <returns></returns>
        public static string Read(string Section, string key)
        {
            StringBuilder retVal = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", retVal, 1024, Inipath);
            return retVal.ToString();
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="section">父节点</param>
        /// <param name="key">名称</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static int Write(string section, string key, string val)
        {
            if (!File.Exists(inipath))
            {
                File.WriteAllText(inipath, "");
            }
            return (int)WritePrivateProfileString(section, key, val, inipath);
        }
    }
}
