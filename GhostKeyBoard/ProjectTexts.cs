using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostKeyBoard
{
    public class ProjectTexts
    {
        //TODO[TS] übersetzuung einbauen

        public const string WELCOME = "Welcome User";
        public const string EMPTY_MACRO = "Dein Makro Name ist leer. Er Muss Buchstaben, Zahlen oder Zeichen enthalten.";
        public const string FUNCTION_DONT_WORK = "Funktion außer Betrieb, bitte später erneut versuchen.";

        public static string GetDeletetSuccessfull(string nameOfDeleted)
        {
            return $"{nameOfDeleted} wurde erfolgreich entfernt";
        }
    }
}
