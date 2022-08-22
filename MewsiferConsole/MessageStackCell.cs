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
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      if (DataGridView is null) return;

      var bg = Selected ? cellStyle.SelectionBackColor : cellStyle.BackColor;


      if (paintParts.HasFlag(DataGridViewPaintParts.ContentBackground))
        graphics.FillRectangle(new SolidBrush(bg), cellBounds);
      //base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

      if (value is not LogEventViewModel logEventVM) return;


      if (paintParts.HasFlag(DataGridViewPaintParts.ContentForeground))
      {
        var fg = Selected ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
        var font = cellStyle.Font;
        if (logEventVM.HasStackTrace)
        {
          fg = Color.DarkRed;
          font = new(font, FontStyle.Bold);
        }

        var bounds = new Rectangle(cellBounds.Left, cellBounds.Top, cellBounds.Width - 40, cellBounds.Height);
        graphics.DrawString(logEventVM.MessageText.Trim(), font, new SolidBrush(fg), bounds);

        if (logEventVM.HasStackTrace)
        {
          int lineHeight = (int)Math.Ceiling(graphics.MeasureString(logEventVM.MessageText.Trim(), font, bounds.Width).Height);
          var buttonBounds = new Rectangle(cellBounds.Right - 38, cellBounds.Top, 32, lineHeight);

          var buttonFont = new Font(cellStyle.Font, FontStyle.Bold);
          graphics.DrawString("!!", buttonFont, Brushes.Black, buttonBounds);

          //if (logEventVM.Expanded)
          //{
          //  //var newLineHeight = font.Height;
          //  bounds = new(cellBounds.Left, cellBounds.Top + lineHeight, cellBounds.Width, cellBounds.Height - lineHeight);
          //  var format = new StringFormat();
          //  graphics.DrawString(logEventVM.StackTraceText, font, new SolidBrush(fg), bounds);
          //}
        }
      }

      if (paintParts.HasFlag(DataGridViewPaintParts.Border))
      {
        graphics.DrawLine(new Pen(this.DataGridView.GridColor, 1), cellBounds.Left - 1, cellBounds.Top - 1, cellBounds.Right, cellBounds.Top - 1);
        graphics.DrawLine(new Pen(this.DataGridView.GridColor, 1), cellBounds.Left - 1, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1);
      }

    }

    //private static Font MakeStackTraceFont(DataGridViewCellStyle cellStyle)
    //{
    //  return new(cellStyle.Font.FontFamily, cellStyle.Font.Size * 0.9f, cellStyle.Font.Style, cellStyle.Font.Unit);
    //}

    //protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
    //{
    //  if (DataGridView is null) return base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
    //  if (DataGridView.Rows[rowIndex].DataBoundItem is not LogEventViewModel vm) return new(10, DataGridView.RowTemplate.Height);

    //  var needed = graphics.MeasureString(vm.MessageText.Trim(), cellStyle.Font, OwningColumn.Width - 40);

    //  int extraNeeded = 0;
    //  if (vm.Expanded)
    //  {
    //    extraNeeded = (int)Math.Ceiling(graphics.MeasureString(vm.StackTraceText, MakeStackTraceFont(cellStyle), OwningColumn.Width).Height);
    //  }

    //  return new((int)Math.Ceiling(needed.Width), (int)Math.Ceiling(needed.Height) + extraNeeded);

    //}


    protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
    {
      if (DataGridView is null) return;
      if (DataGridView.Columns[e.ColumnIndex].Name != "Message") return;

      if (DataGridView is null) return;
      if (RowIndex == -1) return;

      var row = DataGridView.Rows[RowIndex];
      if (row.DataBoundItem is not LogEventViewModel vm) return;

      if (vm.HasStackTrace)
      {
        vm.Expanded = !vm.Expanded;
        DataGridView.InvalidateRow(RowIndex);
      }
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
