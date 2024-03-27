namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class DateType
    {
        public static DateTime GetValue(string input)
        {
            return Convert.ToDateTime(input);
        }

        public static string GetColumnSql()
        {
            return "DATE NOT NULL";
        }
    }
}
