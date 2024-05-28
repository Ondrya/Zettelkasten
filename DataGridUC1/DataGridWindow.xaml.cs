
using DataGridUC1.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace DataGridUC1
{
    /// <summary>
    /// Interaction logic for DataGridWindow.xaml
    /// </summary>
    public partial class DataGridWindow : UserControl
    {
        private CollectionViewSource _cvs;

        public DataGridWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelUC();
        }

        # region view logic that doesn't belong into the view model

        // https://stackoverflow.com/questions/2064848/wpf-datagrid-how-do-i-stop-auto-scrolling-when-a-cell-is-clicked
        private void DataGrid_Documents_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        void ShowHideDetailRows(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                var row = DataGridRow.GetRowContainingElement(expander);

                row.DetailsVisibility = expander.IsExpanded ? Visibility.Visible
                                                            : Visibility.Collapsed;
            }
        }

        #endregion

   }
}



