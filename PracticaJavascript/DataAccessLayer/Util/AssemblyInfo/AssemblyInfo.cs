using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DataAccessLayer.Util.AssemblyInfo
{
    static public class AssemblyInfo
    {
        public static string company { get { return GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company); } }
        public static string product { get { return GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product); } }
        public static string copyright { get { return GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright); } }
        public static string trademark { get { return GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark); } }
        public static string title { get { return GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title); } }
        public static string description { get { return GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); } }
        public static string configuration { get { return GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); } }
        public static string fileVersion { get { return GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version); } }

        public static Version version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public static string versionFull { get { return version.ToString(); } }
        public static string versionMajor { get { return version.Major.ToString(); } }
        public static string versionMinor { get { return version.Minor.ToString(); } }
        public static string versionBuild { get { return version.Build.ToString(); } }
        public static string versionRevision { get { return version.Revision.ToString(); } }

        private static string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }
}
