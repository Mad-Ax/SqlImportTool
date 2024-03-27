namespace SqlImportTool
{
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Windows.Forms;
    using Dapper;
    using SqlImportTool.ImportFormats;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sitelogFileButton_Click(object sender, EventArgs e)
        {
            var importFormat = new RaggedRightImportFormat
            {
                Filepath = @"C:\_bin\37087 - Timesheet Sync Investigation\sitelog_entries.txt",
                HasHeaderRow = true,
                SchemaName = "dbo",
                TableName = "sitelogEntries",
                UpdateMode = UpdateMode.Create
            };

            importFormat.AddColumn(new RaggedRightImportFormat.IntegerColumn { Name = "JobTranId", Width = 12 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateTimeColumn { Name = "EditedDate", Width = 24 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableIntegerColumn { Name = "JTProactId", Width = 12 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableDateTimeColumn { Name = "JTUpdatedDate", Width = 24 });
            importFormat.AddColumn(new RaggedRightImportFormat.StringColumn { Name = "JobCode", Width = 21 });
            importFormat.AddColumn(new RaggedRightImportFormat.StringColumn { Name = "ActivityCode", Width = 13 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateColumn { Name = "TranDate", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.IntegerColumn { Name = "PersonID", Width = 12 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateColumn { Name = "EndDate", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.DecimalColumn { Name = "Hours", Width = 9, Scale = 4 });

            CreateTable(importFormat);

            InsertData(importFormat);
        }

        private void proactButton_Click(object sender, EventArgs e)
        {
            var importFormat = new RaggedRightImportFormat
            {
                Filepath = @"C:\_bin\37087 - Timesheet Sync Investigation\proact_entries.txt",
                HasHeaderRow = true,
                SchemaName = "dbo",
                TableName = "proactEntries",
                UpdateMode = UpdateMode.Create
            };

            importFormat.AddColumn(new RaggedRightImportFormat.IntegerColumn { Name = "TimesheetEntryId", Width = 17 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateTimeColumn { Name = "UpdatedDate", Width = 28 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableIntegerColumn { Name = "SitelogJobTranId", Width = 17 });
            importFormat.AddColumn(new RaggedRightImportFormat.StringColumn { Name = "ProjectCode", Width = 21 });
            importFormat.AddColumn(new RaggedRightImportFormat.StringColumn { Name = "ActivityCode", Width = 21 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateColumn { Name = "EntryDate", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.IntegerColumn { Name = "ResourceId", Width = 12 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableIntegerColumn { Name = "SitelogEmployeeId", Width = 18 });
            importFormat.AddColumn(new RaggedRightImportFormat.DateColumn { Name = "EndDate", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.DecimalColumn { Name = "Hours", Width = 40, Scale = 4 });
            importFormat.AddColumn(new RaggedRightImportFormat.BooleanColumn { Name = "HasTimesheetEntryDetail", Width = 24 });
            importFormat.AddColumn(new RaggedRightImportFormat.BooleanColumn { Name = "IsDeleted", Width = 10 });
            importFormat.AddColumn(new RaggedRightImportFormat.BooleanColumn { Name = "IsApproved", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.BooleanColumn { Name = "IsRejected", Width = 11 });
            importFormat.AddColumn(new RaggedRightImportFormat.BooleanColumn { Name = "IsCorrection", Width = 13 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableStringColumn { Name = "CorrectionType", Width = 51 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableIntegerColumn { Name = "IncorrectEntryId", Width = 17 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableDateColumn { Name = "IncorrectEntryDate", Width = 19 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableDateColumn { Name = "IncorrectEntryPeriodEndDate", Width = 28 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableBooleanColumn { Name = "CorrectedEntryOutsideRange", Width = 27 });
            importFormat.AddColumn(new RaggedRightImportFormat.NullableBooleanColumn { Name = "CorrectedPeriodDifferentToOriginalPeriod", Width = 40 });

            CreateTable(importFormat);

            InsertData(importFormat);
        }

        private void InsertData(IImportFormat importFormat)
        {
            while (importFormat.ReadNext())
            {
                var insertSql = importFormat.GetInsertSql();
                var param = importFormat.GetParamObject();

                using (var connection = GetConnection())
                {
                    connection.Execute(insertSql, param);
                }
            }
        }

        // TODO: this would be in its own service
        private void CreateTable(IImportFormat importFormat)
        {
            var colsSql = new StringBuilder();
            foreach (var column in importFormat.Columns)
            {
                colsSql.AppendLine(column.GetColumnSql() + ",");
            }

            var tableSql =
                @$"DROP TABLE IF EXISTS [{importFormat.SchemaName}].[{importFormat.TableName}];

                CREATE TABLE [{importFormat.SchemaName}].[{importFormat.TableName}]
                    ({colsSql});";

            using (var connection = GetConnection())
            {
                connection.Execute(tableSql);
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Server=.\SQLExpress;Database=TestDatabase;Trusted_Connection=True;");
        }
    }
}
