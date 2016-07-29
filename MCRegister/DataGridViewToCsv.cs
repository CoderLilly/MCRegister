using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCRegister
{
    public class DataGridViewPrinter
    {
        public static string ToCsv(DataGridView grid)
        {
            var sb = new StringBuilder();
            if (grid == null) return string.Empty;
            if (grid.Columns.Count < 1) return string.Empty;
            sb.AppendLine(GetHeaderText(grid));
            var ctr = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (IsLastRowInGrid(grid, ctr)) break;
                sb.AppendLine(GetRowText(row));
                ctr++;
            }
            return sb.ToString();
        }
        private static bool IsLastRowInGrid(DataGridView grid, int index)
        {
            return index == grid.Rows.Count;
        }
        private static string GetHeaderText(DataGridView grid)
        {
            string result = "";
            var ctr = 0;
            // First,Last,Age
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (ctr > 0) result += ",";
                result += "\"" + column.HeaderText + "\"";
                ctr++;
            }
            return result;
        }
        private static string GetRowText(DataGridViewRow row)
        {
            string result = "";
            var ctr = 0;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (ctr > 0) result += ",";
                result += "\"" + cell.Value + "\"";
                ctr++;
            }
            return result;
        }
    }
}
