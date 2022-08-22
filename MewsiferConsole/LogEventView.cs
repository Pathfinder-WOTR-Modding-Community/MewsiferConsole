using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MewsiferConsole
{
  public partial class LogEventView : UserControl
  {

    private LogEventViewModel? _ViewModel;
    private BindingList<StackTraceViewModel> StackTraceVM = new();
    public LogEventViewModel? ViewModel
    {
      get => _ViewModel;
      set
      {
        _ViewModel = value;

        StackTraceVM.Clear();

        if (_ViewModel is null)
        {
          messageLabel.Text = "";
          return;
        }

        messageLabel.Text = _ViewModel.MessageText;
        if (_ViewModel.HasStackTrace)
        {
          for (int i = 0; i < _ViewModel.Model.StackTrace.Count; i++)
            StackTraceVM.Add(new(_ViewModel, i));
        }

      }
    }

    public class StackTraceViewModel
    {
      public readonly LogEventViewModel Parent;
      public readonly int StackTraceIndex;

      public StackTraceViewModel(LogEventViewModel parent, int stackTraceIndex)
      {
        Parent = parent;
        StackTraceIndex = stackTraceIndex;
      }

      public string Value => Parent.Model.StackTrace[StackTraceIndex];
    }

    public LogEventView()
    {
      InitializeComponent();

      var column = new DataGridViewTextBoxColumn();
      column.Name = "Value";
      column.DataPropertyName = "Value";
      column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

      stackTraceTable.Columns.Add(column);
      stackTraceTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
      stackTraceTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

      stackTraceTable.DataSource = StackTraceVM;
    }
  }
}
