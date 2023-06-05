using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class PathFile
    {
        private static string workingDirectory;
        private static string projectDirectory;
        public static string PathFileDataBase()
        {
            workingDirectory = Environment.CurrentDirectory;
            projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            return $"{projectDirectory}\\DataBase\\Product.json";
        }
        public static string PathFileDataBase1()
        {
            workingDirectory = Environment.CurrentDirectory;
            projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            return $"{projectDirectory}\\DataBase\\Stock.json";
        }

        public static string PathFileDataBaseText()
        {
            workingDirectory = Environment.CurrentDirectory;
            projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            return $"{projectDirectory}\\DataBase\\SalesList.txt";
        }
    }
}
