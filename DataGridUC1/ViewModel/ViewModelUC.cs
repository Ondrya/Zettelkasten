
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DataGridUC1.Data;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Serialization;
using static DataGridUC1.Data.MainDataSet;

namespace DataGridUC1.ViewModel
{
    public partial class ViewModelUC : ObservableRecipient, INotifyPropertyChanged
    {
        internal string path 
            = @System.IO.Directory.GetCurrentDirectory() + "/MyCredits.xml";
        private FileInfo _data;
        private MainDataSet _ds = new MainDataSet();
        private DataGrid _gridEx = new DataGrid();

        public ObservableCollection<ComboBoxItem> ComboBoxItems { get; set; }

        public MainDataSet Ds { get { return _ds; } }
        public DataGrid GridEx { get { return _gridEx; } }

        private UserControl dataGridWindow;
        private CollectionViewSource cvs;
        public CollectionViewSource CVS { get; set; }

        private bool cbCompleteFilter;
        private TextBox searchBox;
        
        public IRelayCommand SaveXml { get; }
        public IRelayCommand ReadXml { get; }
        public IRelayCommand AddsNewRow { get; }

        public IRelayCommand<DataGrid> RelayCmdDataGrid { get; }
        public IRelayCommand<CheckBox> RelayCmdCompleteFilter { get; }
        public IRelayCommand<UserControl> RelayCmdCVS { get; }
        public IRelayCommand<FilterEventArgs> RelayCmdFilter { get; }
        public IRelayCommand<FilterEventArgs> RelayCmdSearch { get; }
        public IRelayCommand<TextBox> RelayCmdSearchBox { get; }

        public ViewModelUC()
        {
            #region  commands

            _gridEx = new DataGrid();
            cvs = new CollectionViewSource();
            cvs = CVS;

            // Create the parameterized commands.
            RelayCmdDataGrid = new RelayCommand<DataGrid>(DoParameterCmdDataGrid);
            RelayCmdCompleteFilter = new RelayCommand<CheckBox>(DoParameterisedCommand);
            RelayCmdCVS = new RelayCommand<UserControl>(DoParameterCmdCVS);
            RelayCmdFilter = new RelayCommand<FilterEventArgs>(DoParameterCmdFilter);
            RelayCmdSearch = new RelayCommand<FilterEventArgs>(DoParameterCmdSearch);
            RelayCmdSearchBox = new RelayCommand<TextBox>(DoParameterCmdSearchBox);

            SaveXml = new RelayCommand(WriteXML);
            ReadXml = new RelayCommand(LoadXML);
            AddsNewRow = new RelayCommand(AddNewRow);

            #endregion

            _data = new FileInfo(path);
            LoadXML();

            foreach (DataTable table in _ds.Tables)
            {
                Debug.Print(table.ToString());
                //foreach (DataRow row in table.Rows)
                //{
                //    foreach (DataColumn column in table.Columns)
                //    {
                //        //Add it to a list instead and bind the list
                //        Debug.Print((row[column]).ToString());
                //        Debug.Print(column.ColumnName.ToString());
                //    }
                //}
            }

// https://stackoverflow.com/questions/5409259/binding-itemssource-of-a-comboboxcolumn-in-wpf-datagrid
            ComboBoxItems = new ObservableCollection<ComboBoxItem>() {
            new ComboBoxItem() { ID = 1, Item = "Average" },
            new ComboBoxItem() { ID = 2, Item = "Good" },
            new ComboBoxItem() { ID = 3, Item = "Excellent" } };
        }

        #region  ParameterisedCommand

        private void DoParameterCmdDataGrid(object parameter)
        {
            if (parameter != null)
            {
                GetDataGrid((DataGrid)parameter, null);
            }
        }

        private void DoParameterisedCommand(object parameter)
        {
            CompleteFilter_Changed((CheckBox)parameter, null);
        }

        private void DoParameterCmdCVS(object parameter)
        {
            if (parameter != null)
            {
                UserControl uc = (UserControl)parameter;
                cvs = (CollectionViewSource)uc.Resources["cvsTasks"];
            }
        }

        private void DoParameterCmdFilter(object parameter)
        {
            if (parameter != null)
            {
                CollectionViewSource_Filter(null, (FilterEventArgs)parameter);
            }
        }

        private void DoParameterCmdSearch(object parameter)
        {
            if (parameter != null)
            {
                CollectionViewSource_Search(null, (FilterEventArgs)parameter);
            }
        }

        private void DoParameterCmdSearchBox(object parameter)
        {
            if (parameter != null)
            {
                SearchBox_Changed((TextBox)parameter, null);
            }
        }

        #endregion

        [RelayCommand]
        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        [RelayCommand]
        private void CommitEdit()
        {
            if (_gridEx != null)
            {
                var selectedRow = _gridEx.SelectedItem;
  
                this._gridEx.RowDetailsVisibilityMode = 
                    DataGridRowDetailsVisibilityMode.Visible;
                _gridEx.CommitEdit(DataGridEditingUnit.Row, true);
                _gridEx.SelectedItem = selectedRow;
                _gridEx.RowDetailsVisibilityMode = 
                    DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
        }

        [RelayCommand]
        private void Ungroup()
        //private void UngroupButton_Click()
        {
            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(_ds.Credits);
            if (cvTasks != null)
            {
                //cvTasks.GroupDescriptions.Clear();
                cvs.View.GroupDescriptions.Clear();
            }
        }

        [RelayCommand]
        private void Group()
        //private void GroupButton_Click()
        {
            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(cvs.Source);
            if (cvTasks != null && cvTasks.CanGroup == true)
            {
                //MessageBox.Show("XXXX");
                //cvTasks.GroupDescriptions.Clear();
                //cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("Item"));
                //cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("Check"));

                cvs.View.GroupDescriptions.Clear();
                cvs.View.GroupDescriptions.Add(new PropertyGroupDescription("Item"));
                cvs.View.GroupDescriptions.Add(new PropertyGroupDescription("Check"));
            }
        }

// https://www.codeproject.com/Questions/5380961/How-do-I-fix-net-8-process-start-url-issue
        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process? process = 
                Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri)
            {
                UseShellExecute = true
            });

            process!.WaitForExit();
        }

        private void AddNewRow()
        {
            CreditsRow row;
            row = _ds.Credits.NewCreditsRow();
            row["Check"] = false;
            _ds.BeginInit();
            _ds.Credits.AddCreditsRow(row);
            _ds.EndInit();
            _ds.AcceptChanges();
            cvs.View.Refresh();

            MessageBox.Show("Added NewRow");
        }

        [RelayCommand]
        private void RemoveSelectedRow()
        {
            DataRowView selectedItem = (DataRowView)_gridEx.CurrentItem;
            if (selectedItem != null)
            {
                DataRowView drv =selectedItem;
                drv.Delete();
            }

            _ds.AcceptChanges();
            cvs.View.Refresh();

            MessageBox.Show("Removed Selected Row");
        }

        [RelayCommand]
        private void ShowHideGroupCol()
        {
            if (_gridEx != null && _gridEx.Columns.Count > 0)
            {
                if (_gridEx.Columns[1].Width != 0)
                {
                    _gridEx.Columns[1].Width = 0;
                }
                else
                {
                    _gridEx.Columns[1].Visibility = System.Windows.Visibility.Visible;
                    _gridEx.Columns[1].Width = 120;
                }
            }
            else
            {
                MessageBox.Show("DataGrid == null");
            }
        }

        [RelayCommand]
        private void LoadXML()
        {
            _ds.Clear();
            _ds.ReadXml(_data.FullName);
            _ds.AcceptChanges();
            if (cvs is object)
                cvs.View.Refresh();
        }

        // ds.WriteXml(path);
        private void WriteXML()
        {
            _ds.AcceptChanges();

            _ds.WriteXml(path);
        MessageBox.Show("xml data saved. ");
        }

        private void GetDataGrid(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                _gridEx =(DataGrid)sender;
                _gridEx.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
        }

        private void CompleteFilter_Changed(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                cbCompleteFilter = (bool)((CheckBox)sender).IsChecked;
            }
            // Refresh the view to apply filters.
            cvs.View.Refresh();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            //Task t = e.Item as Task;
            DataRowView drv = e.Item as DataRowView;

            if (e.Item != null)
            {
                drv = (DataRowView)e.Item;

                if (drv != null && cbCompleteFilter != null)
                // If filter is turned on, filter completed items.
                {
                    // if (this.cbCompleteFilter.IsChecked == true && t.Complete == true)
                    if (this.cbCompleteFilter == true && (bool)drv.Row["Check"] == true)
                    {
                        e.Accepted = false;
                    }
                    else
                        e.Accepted = true;
                }
            }
        }

        private void SearchBox_Changed(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                searchBox = (TextBox)sender;
            }
            // Refresh the view to apply filters.
            cvs.View.Refresh();
        }

        private void CollectionViewSource_Search(object sender, FilterEventArgs e)
        {
            DataRowView drv = e.Item as DataRowView;
            if (e.Item != null)
            {
                drv = (DataRowView)e.Item;

                if (drv != null && searchBox != null 
                    && this.cbCompleteFilter == false)
                {
                    if (drv.Row["Item"].ToString().ToLower().Contains(searchBox.Text.ToLower()) == false
                        && drv.Row["Note"].ToString().ToLower().Contains(searchBox.Text.ToLower()) == false)
                    {
                        e.Accepted = false;
                    }
                    else
                        e.Accepted = true;
                }

                if (drv != null && searchBox != null 
                    && this.cbCompleteFilter == true)
                {
                    if (drv.Row["Item"].ToString().ToLower().Contains(searchBox.Text.ToLower()) == false
                        && drv.Row["Note"].ToString().ToLower().Contains(searchBox.Text.ToLower()) == false
                        || (bool)drv.Row["Check"] == true)
                    {
                        e.Accepted = false;
                    }
                    else
                        e.Accepted = true;
                }
            }
        }

        private Style buttonCellTemplate;

        public Style ButtonCellTemplate { get => buttonCellTemplate; set => SetProperty(ref buttonCellTemplate, value); }
    }

    public class ComboBoxItem
    {
        public int ID { get; set; }
        public string Item { get; set; }
    }

    #region  Command Classes

    //// https://www.codeproject.com/Questions/5331445/How-to-pass-a-string-from-view-to-the-viewmodel-in
    /// <summary>
    /// 
    /// </summary>
    public class ParamCommand : ICommand
    {
        private Action<object> _action; // String)

        public ParamCommand(Action<object> action) // String))
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter); // .ToString)
        }
    }

    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => true;

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class ButtonClickCommand : CommandBase
    {
        public ButtonClickCommand(Action<string> callback)
            => _callback = callback;

        private readonly Action<string>? _callback;

        public override void Execute(object? parameter)
            => _callback?.Invoke(parameter as string ?? string.Empty);
    }

    #endregion

    public class DbConverter : MarkupExtension, IValueConverter
    {
        private static readonly Dictionary<object, int> ParameterToColumnMapping;
        static DbConverter()
        {
            ParameterToColumnMapping = new Dictionary<object, int>
            {
                {"PersonName", 0}, {"PersonAddress", 1}
            };
        }
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            DataRow dr = value as DataRow;
            if (dr != null)
            {
                if (ParameterToColumnMapping.ContainsKey(parameter))
                {
                    return dr.ItemArray[ParameterToColumnMapping[parameter]];
                }
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                   System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
