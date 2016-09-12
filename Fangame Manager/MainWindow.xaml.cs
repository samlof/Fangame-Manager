﻿using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;

namespace Fangame_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GameManager gameManager;
        public MainWindow()
        {
            InitializeComponent();
            gameManager = new GameManager();
            IOManager.UnpackNewgames();
            gameManager.checkDirectoryForGames(Directory.GetCurrentDirectory());

            gameManager.finishGameLists();

            lbOldGames.ItemsSource = gameManager.Games;
            lbNewGames.ItemsSource = gameManager.NewGames;
            lbRecentGames.ItemsSource = gameManager.RecentGames;
        }
        private void Show_Options_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Show options'");
        }

        private void Quit_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameManager.saveSettings();
        }
                


        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(getSelectedGame());
        }

        #region Listbox functions
        private void lb_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(sender is System.Windows.Controls.ListBox == false)
            {
                MessageBox.Show("lb_MouseDoubleClick Only ListBox should call this function!");
                return;
            }
            string name = (string)(sender as System.Windows.Controls.ListBox).SelectedItem;
            gameManager.StartGame(name);
        }

        public string getSelectedGame()
        {
            int index = lbOldGames.SelectedIndex;
            if (index > -1)
            {
                return (string)lbOldGames.SelectedItem;
            }
            index = lbNewGames.SelectedIndex;
            if (index > -1)
            {
                return (string)lbNewGames.SelectedItem;
            }
            index = lbRecentGames.SelectedIndex;
            if (index > -1)
            {
                return (string)lbRecentGames.SelectedItem;
            }
            return null;
        }

        private void lbOldGames_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lbNewGames.SelectedIndex = -1;
            lbRecentGames.SelectedIndex = -1;
        }

        private void lbNewGames_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lbOldGames.SelectedIndex = -1;
            lbRecentGames.SelectedIndex = -1;
        }

        private void Label_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lbNewGames.SelectedIndex = -1;
            lbOldGames.SelectedIndex = -1;
        }
        #endregion
    }
}