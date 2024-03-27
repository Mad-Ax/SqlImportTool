namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public class BooleanType
    {
        public static bool GetValue(string input)
        {
            if (input.Trim() == "0")
            {
                return false;
            }

            if (input.Trim() == "1")
            {
                return true;
            }

            return Convert.ToBoolean(input.Trim());
        }

        public static string GetColumnSql()
        {
            return "BIT NOT NULL";
        }
    }
}
