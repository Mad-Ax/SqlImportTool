namespace SqlImportTool.ImportFormats
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SqlImportTool.ImportFormats.DataTypes;

    public class RaggedRightImportFormat : IImportFormat, IDisposable
    {
        public string Filepath { get; set; }

        public bool HasHeaderRow { get; set; }

        private List<IImportColumnDefinition> _columns;

        public IList<IImportColumnDefinition> Columns
        {
            get
            {
                return _columns;
            }
        }

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public UpdateMode UpdateMode { get; set; }

        private StreamReader _streamReader;

        public RaggedRightImportFormat()
        {
            _columns = new List<IImportColumnDefinition>();
        }

        public void AddColumn(IImportColumnDefinition column)
        {
            var col = column as IRaggedRightColumnDefinition;

            if (col != null)
            {
                _columns.Add(col);
            }
        }

        public bool ReadNext()
        {
            if (_streamReader == null)
            {
                _streamReader = new StreamReader(Filepath);

                if (HasHeaderRow)
                {
                    _streamReader.ReadLine();
                }
            }

            var line = _streamReader.ReadLine();
            if (line == null)
            {
                // TODO: set EoF property?
                return false;
            }

            foreach (var column in _columns)
            {
                // TODO: check string length, last column can be shorter or longer
                var raggedRightColumn = column as IRaggedRightColumnDefinition;

                string rawValue;
                if (column == _columns.Last())
                {
                    rawValue = line;
                }
                else
                {
                    rawValue = line.Substring(0, raggedRightColumn.Width);
                    line = line.Substring(raggedRightColumn.Width);
                }

                raggedRightColumn.RawValue = rawValue;
            }

            return true;
        }

        public string GetInsertSql()
        {
            var colDefs = string.Join(',', Columns.Select(c => c.Name));
            var colParams = string.Join(',', Columns.Select(c => $"@{c.Name}"));
            //var colParams = string.Empty;
            //for (var i = 0; i < Columns.Count; i++)
            //{
            //    colParams += $"param{i},";
            //}
            //colParams = colParams.TrimEnd(',');

            var insertSql = @$"INSERT INTO [{SchemaName}].[{TableName}] ({colDefs})
                VALUES ({colParams})";

            return insertSql;
        }

        public object GetParamObject()
        {
            var dict = new Dictionary<string, object>();
            
            foreach (var col in Columns)
            {
                dict.Add(col.Name, col.GetValue());
            }

            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in dict)
            {
                eoColl.Add(kvp);
            }

            dynamic eoDynamic = eo;

            return eoDynamic;
        }

        public void Dispose()
        {
            if (_streamReader != null)
            {
                _streamReader.Close();
            }

            _streamReader = null;
        }

        public interface IRaggedRightColumnDefinition : IImportColumnDefinition
        {
            int Width { get; set; }
        }

        // TODO: do we need?
        public interface IRaggedRightColumnDefinition<T> : IRaggedRightColumnDefinition, IImportColumnDefinition<T>
        { }

        public abstract class ColumnDefinition<T> : IRaggedRightColumnDefinition<T>
        {
            public string Name { get; set; }

            public int Width { get; set; }

            public string RawValue { get; set; }

            public abstract T GetValue();

            public abstract string GetColumnSql();

            object IImportColumnDefinition.GetValue()
            {
                return GetValue();
            }
        }

        public class IntegerColumn : ColumnDefinition<int>
        {
            public override int GetValue()
            {
                return IntegerType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {IntegerType.GetColumnSql()}";
            }
        }

        public class NullableIntegerColumn : ColumnDefinition<int?>
        {
            public override int? GetValue()
            {
                return NullableIntegerType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {NullableIntegerType.GetColumnSql()}";
            }
        }

        public class DateColumn : ColumnDefinition<DateTime>
        {
            public override DateTime GetValue()
            {
                return DateTimeType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {DateType.GetColumnSql()}";
            }
        }

        public class DateTimeColumn : ColumnDefinition<DateTime>
        {
            public override DateTime GetValue()
            {
                return DateTimeType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {DateTimeType.GetColumnSql()}";
            }
        }

        public class NullableDateColumn : ColumnDefinition<DateTime?>
        {
            public override DateTime? GetValue()
            {
                return NullableDateTimeType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {NullableDateType.GetColumnSql()}";
            }
        }

        public class NullableDateTimeColumn : ColumnDefinition<DateTime?>
        {
            public override DateTime? GetValue()
            {
                return NullableDateTimeType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {NullableDateTimeType.GetColumnSql()}";
            }
        }

        public class StringColumn : ColumnDefinition<string>
        {
            public override string GetValue()
            {
                return StringType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {StringType.GetColumnSql(Width)}";
            }
        }

        public class NullableStringColumn : ColumnDefinition<string>
        {
            public override string GetValue()
            {
                return NullableStringType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {NullableStringType.GetColumnSql(Width)}";
            }
        }

        public class DecimalColumn : ColumnDefinition<decimal>
        {
            public int Scale { get; set; }

            public override decimal GetValue()
            {
                return DecimalType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {DecimalType.GetColumnSql(Width, Scale)}";
            }
        }

        public class BooleanColumn : ColumnDefinition<bool>
        {
            public override bool GetValue()
            {
                return BooleanType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {BooleanType.GetColumnSql()}";
            }
        }

        public class NullableBooleanColumn : ColumnDefinition<bool?>
        {
            public override bool? GetValue()
            {
                return NullableBooleanType.GetValue(RawValue);
            }

            public override string GetColumnSql()
            {
                return $"[{Name}] {NullableBooleanType.GetColumnSql()}";
            }
        }
    }
}
