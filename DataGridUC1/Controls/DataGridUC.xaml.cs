
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace DataGridUC1.Controls
{
    /// <summary>
    /// Interaktionslogik für DataGridEx.xaml
    /// </summary>
    public partial class DataGridUC1 : UserControl
    {
        public DataGridUC1()
        {
            this.CanDoDragAndSelect = true;
        }

        #region Depndency Properties

        //public static readonly DependencyProperty UnSelectAllByEscapeKeyProperty =
        //        DependencyProperty.Register(
        //            "UnSelectAllByEscapeKey",
        //            typeof(bool),
        //            typeof(DataGridEx));

        ///// <summary>
        ///// 열고정 할 경우, 아래 스크롤에 대한 확장 여부
        ///// </summary>
        //public static readonly DependencyProperty ExtendHorizontalScrollToFrozenColumnsProperty =
        //    DependencyProperty.Register(
        //        "ExtendHorizontalScrollToFrozenColumns",
        //        typeof(bool),
        //        typeof(DataGridEx));

        #endregion

        #region Public Events

        //public event EventHandler<FlexGridCommittedArgs> Committed;

        #endregion

        #region Public Properties

        //public bool UnSelectAllByEscapeKey
        //{
        //    get => (bool)GetValue(UnSelectAllByEscapeKeyProperty);
        //    set => SetValue(UnSelectAllByEscapeKeyProperty, value);
        //}

        //public bool IsEditing
        //{
        //    get
        //    {
        //        var row = (DataGridRow)ItemContainerGenerator.ContainerFromItem(CurrentCell.Item);
        //        if (row == null)
        //            return false;

        //        return row.IsEditing;
        //    }
        //}

        ///// <summary>
        ///// Wenn die Spalte fixiert ist, ob sie zum Scrollen nach unten erweitert wird
        ///// </summary>
        //public bool ExtendHorizontalScrollToFrozenColumns
        //{
        //    get => (bool)GetValue(ExtendHorizontalScrollToFrozenColumnsProperty);
        //    set => SetValue(ExtendHorizontalScrollToFrozenColumnsProperty, value);
        //}

        //#endregion

        #region Internal Properties

        internal bool CanDoDragAndSelect { get; set; }

        #endregion


        //#region Private Methods

        //private void AttachEventHandlers()
        //{
        //    Loaded += OnLoaded;
        //}

        //private void PrepareForSort(DataGridColumn sortColumn)
        //{
        //    if (Keyboard.IsKeyDown(Key.LeftShift)
        //        || !Columns.Contains(sortColumn))
        //        return;

        //    if (Columns != null)
        //    {
        //        foreach (DataGridColumn column in Columns)
        //        {
        //            if (column != sortColumn)
        //                column.SortDirection = null;
        //        }
        //    }
        //}

        //#endregion

        //#region Private EventHandlers

        //private void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    //BandHeadersPresenter = Utils.FindVisualChild<BandHeadersPresenter>(this);

        //    //Columns.Clear();
        //    //SyncColumnsWithBands();
        //}

        //#endregion

        //#region Public Methods

        //// https://www.codeproject.com/Questions/5380961/How-do-I-fix-net-8-process-start-url-issue
        //public void DGEx_Hyperlink_Click(object sender, RoutedEventArgs e)
        //{
        //    Hyperlink link = (Hyperlink)e.OriginalSource;
        //    Process? process = Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri)
        //    {
        //        UseShellExecute = true
        //    });

        //    process!.WaitForExit();
        //}


        //public void PerformSort(DataGridColumn sortColumn)
        //{
        //    if (!CanUserSortColumns)
        //        return;

        //    if (CommitEdit())
        //    {
        //        PrepareForSort(sortColumn);

        //        var args = new DataGridSortingEventArgs(sortColumn);
        //        OnSorting(args);

        //        if (Items.NeedsRefresh)
        //        {
        //            try
        //            {
        //                Items.Refresh();
        //            }
        //            catch
        //            {
        //                Items.SortDescriptions.Clear();
        //            }
        //        }
        //    }
        //}

        //#endregion

        //#region Protected Override Methods

        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);

        //    switch (e.Key)
        //    {
        //        case Key.Enter:
        //            {
        //                // Wenn Sie während der Bearbeitung auf die Eingabetaste klicken
        //                if (IsEditing)
        //                {
        //                    // 적용하고 Enter 이벤트를 처리했다고 표기
        //                    // Apply and mark the Enter event as handful.
        //                    //MessageBox.Show("Enter");
        //                    CommitEdit(DataGridEditingUnit.Row, true);
        //                    e.Handled = true;
        //                }
        //            }
        //            break;
        //        case Key.Tab:
        //            {
        //                // Wenn Sie während der Bearbeitung die Tab Taste klicken
        //                if (IsEditing)
        //                {
        //                    // Apply and mark the Tab event as handful.
        //                    //MessageBox.Show("Tab");
        //                    CommitEdit(DataGridEditingUnit.Row, true);
        //                    e.Handled = true;
        //                }
        //            }
        //            break;
        //    }
        //}

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);

        //    switch (e.Key)
        //    {
        //        case Key.Escape:
        //            {
        //                MessageBox.Show("EscapeKey");
        //                if (UnSelectAllByEscapeKey)
        //                    UnselectAllCells();
        //            }
        //            break;
        //    }
        //}

        //// https://stackoverflow.com/questions/78304588/selection-rectangle-not-moving-along-with-wpf-datagrid-content-during-scroll
        //protected override void OnBeginningEdit(DataGridBeginningEditEventArgs e)
        //{
        //    base.OnBeginningEdit(e);
        //    this.CanDoDragAndSelect = false;
        //}

        //protected override void OnCellEditEnding(DataGridCellEditEndingEventArgs e)
        //{
        //    base.OnCellEditEnding(e);
        //    this.CanDoDragAndSelect = true;
        //}

        #endregion
    }
}
