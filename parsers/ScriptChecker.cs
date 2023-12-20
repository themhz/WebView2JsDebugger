using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Repository;

namespace WebViewJsDebugger.parsers
{
    public static class ScriptChecker
    {

        public static bool checkIfPostMessageExists(string script)
        {
            string pattern = @"window\.chrome\.webview\.postMessage\(JSON\.stringify\([^\)]+\)\);";
            bool containsPattern = Regex.IsMatch(script, pattern);
            return containsPattern;
        }
    }
}
