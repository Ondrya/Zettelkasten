﻿using DataGridUC1;
using System.Windows.Input;
using Zettelkasten.DesktopApp.ViewModels;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        private RelayCommand tabSearchCommand;
        private RelayCommand tabPlanningCommand;
        private RelayCommand tabZettelkastenCommand;
        private RelayCommand tabSelectionMapCommand;
        private RelayCommand tabNotepadCommand;
        private RelayCommand tabZettelkastenNewCommand;
        private RelayCommand tabZettelkastenEditCommand;

        private void ShowTab(MenuItems itemToShow)
        {
            IsVisibleTabNotepad = itemToShow == MenuItems.Notepad;
            IsVisibleTabPlanning = itemToShow == MenuItems.Planning;
            IsVisibleTabSearch = itemToShow == MenuItems.Search;
            IsVisibleTabSelectionMap = itemToShow == MenuItems.SelectionMap;
            IsVisibleTabZettelkasten = itemToShow == MenuItems.Zettelkasten;
            IsVisibleTabZettelkastenNew = itemToShow == MenuItems.ZettelkastenNew;
            IsVisibleTabZettelkastenEdit = itemToShow == MenuItems.ZettelkastenEdit;
        }

        private void TabSearch(object obj)
        {
            ShowTab(MenuItems.Search);
            RefreshZettelList(null);
        }
        private void TabPlanning(object obj)
        {
            //ShowTab(MenuItems.Planning);
            var window = new Planning();
            window.Show();
        }
        private void TabZettelkasten(object obj)
        {
            ShowTab(MenuItems.Zettelkasten);
        }
        private void TabSelectionMap(object obj)
        {
            ShowTab(MenuItems.SelectionMap);
        }
        private void TabNotepad(object obj)
        {
            ShowTab(MenuItems.Notepad);
        }
        private void TabZettelkastenNew(object obj)
        {
            ShowTab(MenuItems.ZettelkastenNew);
        }

        private void TabZettelkastenEdit(object obj)
        {
            ShowTab(MenuItems.ZettelkastenEdit);
        }

        public ICommand TabSearchCommand => tabSearchCommand ??= new RelayCommand(TabSearch, (obj) => true);
        public ICommand TabPlanningCommand => tabPlanningCommand ??= new RelayCommand(TabPlanning, (obj) => true);
        public ICommand TabZettelkastenCommand => tabZettelkastenCommand ??= new RelayCommand(TabZettelkasten, (obj) => true);
        public ICommand TabSelectionMapCommand => tabSelectionMapCommand ??= new RelayCommand(TabSelectionMap, (obj) => false);
        public ICommand TabNotepadCommand => tabNotepadCommand ??= new RelayCommand(TabNotepad, (obj) => false);
        public ICommand TabZettelkastenNewCommand => tabZettelkastenNewCommand ??= new RelayCommand(TabZettelkastenNew, (obj) => true);
        public ICommand TabZettelkastenEditCommand => tabZettelkastenEditCommand ??= new RelayCommand(TabZettelkastenEdit, (obj) => true);
    }
}
