namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public class IntegerType
    {
        public static int GetValue(string input)
        {
            return Convert.ToInt32(input);
        }

        public static string GetColumnSql()
        {
            return "INT NOT NULL";
        }
    }
}
