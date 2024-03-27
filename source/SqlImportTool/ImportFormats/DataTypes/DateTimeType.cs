namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class DateTimeType
    {
        public static DateTime GetValue(string input)
        {
            return Convert.ToDateTime(input);
        }

        public static string GetColumnSql()
        {
            return "DATETIME NOT NULL";
        }
    }
}
