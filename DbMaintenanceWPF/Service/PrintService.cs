using ClosedXML.Excel;
using ClosedXML.Report;
using DbMaintenanceWPF.Service.Interface;
using Microsoft.Extensions.FileProviders;
using System;
using System.CodeDom;
using System.IO;

namespace DbMaintenanceWPF.Service
{
    class PrintService : IPrint
    {

        public bool Print(string pattern, string path, string nameDocument, string[] namesVariable, object[] paramets)
        {
            switch (pattern)
            {
                default: throw new NotSupportedException($"Печать типа {nameDocument} не поддерживается");
                case "MaterialStatement": return PrintMaterialStatement(path, nameDocument, namesVariable, paramets);
            }
        }

        bool PrintMaterialStatement(string path, string nameDocument, string[] namesVariable, object[] paramets)
        {
            string pathPattern = Directory.GetCurrentDirectory() + "\\PrintPattern\\PatternMaterialStatement.xlsx";

            if (!File.Exists(pathPattern)) new NotFoundFileInfo("Не найден шаблон: PatternMaterialStatement.xlsx");

            using (var template = new XLTemplate(pathPattern))  
            {
                for (int i = 0; i < namesVariable.Length; i++) template.AddVariable(namesVariable[i], paramets[i]);
                template.Generate();
                template.SaveAs(path + "\\" + nameDocument + ".xlsx");
            }

            return true;
        }
    }
}
