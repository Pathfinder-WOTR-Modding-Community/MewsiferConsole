using MewsiferConsole.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MewsiferConsole
{
  internal class MessageStackCell : DataGridViewCell
  {
    private Rectangle BaseButtonBounds => new(-38, 2, 32, 16);

    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      if (DataGridView is null) return;

      if (paintParts.HasFlag(DataGridViewPaintParts.ContentBackground))
        graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);
      //base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

      if (value is not LogEventViewModel logEventVM) return;

      if (paintParts.HasFlag(DataGridViewPaintParts.ContentForeground))
      {
        int lineHeight = cellStyle.Font.Height;
        var bounds = new Rectangle(cellBounds.Left, cellBounds.Top, cellBounds.Width - 40, Math.Min(lineHeight, cellBounds.Height));

        int requiredHeight = 0;

        graphics.DrawString(logEventVM.MessageText.Trim(), cellStyle.Font, new SolidBrush(cellStyle.ForeColor), bounds);
        if (logEventVM.Model.StackTrace?.Count > 0)
        {
          Font font = new(cellStyle.Font.FontFamily, cellStyle.Font.Size * 0.8f, cellStyle.Font.Style, cellStyle.Font.Unit);
          var newLineHeight = font.Height;
          bounds = new(cellBounds.Left, cellBounds.Top + lineHeight, cellBounds.Width, newLineHeight);

          foreach (var stackLine in logEventVM.Model.StackTrace)
          {
            if (bounds.Bottom > cellBounds.Bottom)
              break;

            graphics.DrawString(stackLine.Trim(), font, new SolidBrush(cellStyle.ForeColor), bounds);
            bounds.Offset(0, newLineHeight);
          }

          requiredHeight = logEventVM.Model.StackTrace.Count * newLineHeight + 4;
        }

        if (logEventVM.RequiredExtraHeight == -1)
          logEventVM.RequiredExtraHeight = requiredHeight;

        if (requiredHeight > 0)
        {
          var buttonBounds = BaseButtonBounds;
          buttonBounds.Offset(cellBounds.Right, cellBounds.Top);

          if (logEventVM.Expanded)
            graphics.FillRectangle(Brushes.Red, buttonBounds);
          else
            graphics.FillRectangle(Brushes.Green, buttonBounds);
        }
      }

      if (paintParts.HasFlag(DataGridViewPaintParts.Border))
      {
        graphics.DrawLine(new Pen(cellStyle.SelectionBackColor, 1), cellBounds.Left - 1, cellBounds.Top - 1, cellBounds.Right, cellBounds.Top - 1);
        graphics.DrawLine(new Pen(cellStyle.SelectionBackColor, 1), cellBounds.Left - 1, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1);
      }

    }

    private bool HasMouseOver = false;


    protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
    {
      if (DataGridView is null) return;
      if (DataGridView.Columns[e.ColumnIndex].Name != "Message") return;

      if (DataGridView is null) return;
      if (RowIndex == -1) return;

      var row = DataGridView.Rows[RowIndex];
      if (row.DataBoundItem is not LogEventViewModel vm) return;
      if (vm.RequiredExtraHeight <= 0)
      {
        if (DataGridView.Cursor != Cursors.Default)
          DataGridView.Cursor = Cursors.Default;
        return;
      }

      vm.Expanded = !vm.Expanded;
      if (vm.Expanded)
        DataGridView.Rows[RowIndex].Height = DataGridView.RowTemplate.Height + vm.RequiredExtraHeight;
      else
        DataGridView.Rows[RowIndex].Height = DataGridView.RowTemplate.Height;
    }
  }


  internal class MessageStackColumn : DataGridViewColumn
  {
    public MessageStackColumn()
    {
      this.CellTemplate = new MessageStackCell();
    }
  }
}
