﻿// <copyright file="frmGrid.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using SEATLibrary;

    /// <summary>
    /// Interaction logic for frmGrid.xaml
    /// </summary>
    public partial class frmGrid : Window
    {
        private Room myroom;
        private Seat[,] seatArray;
        private TextBox[] txtcol;
        private TextBox[] txtrow;
        private int row;
        private int column;
        private ListBox students = new ListBox();
        private List<Seat> seatsSelected;

        public frmGrid()
        {
            InitializeComponent();
            int row = 0;
            int column = 0;
            this.seatArray = new Seat[row, column];
        }

        public frmGrid(Room inroom, bool editable)
        {
            InitializeComponent();
            this.myroom = inroom;
            this.row = this.myroom.Height;
            this.column = this.myroom.Width;
            
            // int seatSize = 45;
            this.seatsSelected = new List<Seat>();
            int width = 45;
            int height = 60;
            txtloc.Text = this.myroom.Location;
            txtnm.Text = this.myroom.RoomName;
            txtdes.Text = this.myroom.Description;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
            if (!editable)
            {
                Options.Width = 0;
            }

            // row = rows;
            // column = columns;
            ContainerVisual newPage = new ContainerVisual();
            this.seatArray = new Seat[this.row, this.column];
            this.txtcol = new TextBox[this.column];
            this.txtrow = new TextBox[this.row];

            griddy.VerticalAlignment = VerticalAlignment.Top;
            for (int i = 0; i < this.row; i++)
            {
                if (editable)
                {
                    Button button1 = new Button();
                    button1.FontSize = 10;
                    button1.Content = "Select";
                    button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonRow_Click));
                    button1.VerticalAlignment = VerticalAlignment.Top;
                    button1.HorizontalAlignment = HorizontalAlignment.Left;
                    button1.Width = width;
                    button1.Height = height;
                    button1.Tag = i;
                    button1.Margin = new Thickness(width, height * i, 0, 0);
                    grdleft.Children.Add(button1);

                    this.txtrow[i] = new TextBox();
                    this.txtrow[i].FontSize = 10;
                    this.txtrow[i].VerticalAlignment = VerticalAlignment.Top;
                    this.txtrow[i].HorizontalAlignment = HorizontalAlignment.Left;
                    this.txtrow[i].Margin = new Thickness(0, height * i, 0, 0);
                    this.txtrow[i].Width = width;
                    this.txtrow[i].Height = height;
                    grdleft.Children.Add(this.txtrow[i]);
                }

                for (int j = 0; j < this.column; j++)
                {
                    if (editable)
                    {
                        Button button2 = new Button();
                        button2.FontSize = 10;
                        button2.Content = "Select";
                        button2.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonColumn_Click));
                        button2.VerticalAlignment = VerticalAlignment.Top;
                        button2.HorizontalAlignment = HorizontalAlignment.Left;
                        button2.Margin = new Thickness(width * j, height, 0, 0);
                        button2.Width = width;
                        button2.Height = height;
                        button2.Tag = j;
                        grdtop2.Children.Add(button2);

                        this.txtcol[j] = new TextBox();
                        this.txtcol[j].FontSize = 10;
                        this.txtcol[j].VerticalAlignment = VerticalAlignment.Top;
                        this.txtcol[j].HorizontalAlignment = HorizontalAlignment.Left;
                        this.txtcol[j].Margin = new Thickness(width * j, 0, 0, 0);
                        this.txtcol[j].Width = width;
                        this.txtcol[j].Height = height;
                        grdtop2.Children.Add(this.txtcol[j]);
                    }

                    this.seatArray[i, j] = new Seat(this.myroom.Chairs[i, j], editable);
                    this.seatArray[i, j].AddHandler(UserControl.MouseLeftButtonUpEvent, new RoutedEventHandler(this.Student_Drop));
                    this.seatArray[i, j].AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(this.CheckBoxSelected_Checked));
                    this.seatArray[i, j].AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(this.CheckBoxSelected_Unchecked));
                    this.seatArray[i, j].Margin = new Thickness(width * j, height * i, 0, 0);
                    griddy.Children.Add(this.seatArray[i, j]);
                }
            }

            if (editable)
            {
                Label lblspace = new Label(); // used as a pad for the scroll bar
                Label lblspace2 = new Label();
                lblspace.Width = lblspace2.Width = 20;
                lblspace.Height = lblspace2.Height = 20;
                lblspace.Content = lblspace2.Content = string.Empty;
                lblspace.Margin = new Thickness(0, height * this.row, 0, 0);
                grdleft.Children.Add(lblspace);
                lblspace2.Margin = new Thickness((width * this.column) + width, 0, 0, 0);
                grdtop2.Children.Add(lblspace2);
                Button button3 = new Button();
                button3.FontSize = 10;
                TextBlock select = new TextBlock();
                select.Text = "Select All";
                select.TextWrapping = TextWrapping.Wrap;
                button3.Content = select;
                button3.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonAll_Click));
                button3.VerticalAlignment = VerticalAlignment.Top;
                button3.HorizontalAlignment = HorizontalAlignment.Left;
                button3.Margin = new Thickness(width, height, 0, 0);
                button3.Width = width;
                button3.Height = height;
                button3.Tag = this.column + " " + this.row;
                grdtop.Children.Add(button3);

                Button btnRbyC = new Button();
                btnRbyC.Content = "R by C";
                btnRbyC.FontSize = 10;
                btnRbyC.VerticalAlignment = VerticalAlignment.Top;
                btnRbyC.HorizontalAlignment = HorizontalAlignment.Left;
                btnRbyC.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.RowByColumn_click));
                btnRbyC.Margin = new Thickness(0, 0, 0, 0);
                btnRbyC.Width = width;
                btnRbyC.Height = height / 2;
                grdtop.Children.Add(btnRbyC);

                Button btnCbyR = new Button();
                btnCbyR.Content = "C by R";
                btnCbyR.FontSize = 10;
                btnCbyR.VerticalAlignment = VerticalAlignment.Top;
                btnCbyR.HorizontalAlignment = HorizontalAlignment.Left;
                btnCbyR.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ColumnByRow_click));
                btnCbyR.Margin = new Thickness(0, height / 2, 0, 0);
                btnCbyR.Width = width;
                btnCbyR.Height = height / 2;
                grdtop.Children.Add(btnCbyR);

                TextBlock naming = new TextBlock();
                naming.TextWrapping = TextWrapping.Wrap;
                naming.Text = "Name rows";
                naming.Width = width;
                naming.VerticalAlignment = VerticalAlignment.Top;
                naming.HorizontalAlignment = HorizontalAlignment.Left;
                naming.Margin = new Thickness(0, height, 0, 0);
                grdtop.Children.Add(naming);

                TextBlock naming2 = new TextBlock();
                naming2.TextWrapping = TextWrapping.Wrap;
                naming2.Text = "Name columns";
                naming2.Width = width;
                naming2.VerticalAlignment = VerticalAlignment.Top;
                naming2.HorizontalAlignment = HorizontalAlignment.Left;
                naming2.Margin = new Thickness(width, 0, 0, 0);
                grdtop.Children.Add(naming2);

                svrgrid.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollGrid_ScrollChanged));
                svrtop.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollTop_ScrollChanged));
                svrleft.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollLeft_ScrollChanged));
            }
            else
            {

                //grdopt.Height = 50;
                ComboBox cmbxvert = new ComboBox();
                cmbxvert.VerticalAlignment = VerticalAlignment.Top;
                cmbxvert.HorizontalAlignment = HorizontalAlignment.Left;
                string[] items = { "Front", "Middle", "Back"};
                foreach (string itemText in items)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = itemText;
                    cmbxvert.Items.Add(item);
                }
                ComboBox cmbxhoriz = new ComboBox();
                string[] items2 = { "Left", "Middle", "Right"};
                foreach (string itemText in items2)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = itemText;
                    cmbxhoriz.Items.Add(item);
                }
                TextBox txtbxX = new TextBox();
                TextBox txtbxY = new TextBox();
                CheckBox chkLeft = new CheckBox();
                chkLeft.Content = "Accomidate Left Handed Students";
                CheckBox chkImp = new CheckBox();
                chkLeft.Content = "Accomidate Visually Impaired Students";
                CheckBox chkCheck = new CheckBox();
                chkLeft.Content = "Checkered Seating";
                grdopt.Children.Add(cmbxvert);
                grdopt.Children.Add(cmbxhoriz);
                grdopt.Children.Add(txtbxX);
                grdopt.Children.Add(txtbxY);
                grdopt.Children.Add(chkLeft);
                grdopt.Children.Add(chkImp);
                grdopt.Children.Add(chkCheck);
                this.students.Width = 150;
                this.students.ItemsSource = this.myroom.RoomStudents;
                grdleft.Children.Add(this.students);
                
                // students
            }

            griddy.MaxHeight = (height * this.row) + 20;
            griddy.MaxWidth = (width * this.column) + 20;
        }

        private void ScrollGrid_ScrollChanged(object sender, EventArgs e)
        {
            svrtop.ScrollToHorizontalOffset(svrgrid.HorizontalOffset);
            svrleft.ScrollToVerticalOffset(svrgrid.VerticalOffset);
        }

        private void ScrollTop_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToHorizontalOffset(svrtop.HorizontalOffset);
        }

        private void ScrollLeft_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToVerticalOffset(svrleft.VerticalOffset);
        }

        private void ButtonColumn_Click(object sender, EventArgs e)
        {
            Button button1 = (Button)sender;
            for (int i = 0; i < this.row; i++)
            {
                if ((string)button1.Content == "Select")
                {
                    if (!(bool)this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked)
                    {
                        this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked = true;
                    }
                }
                else
                {
                    if ((bool)this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked)
                    {
                        this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked = false;
                    }
                }
            }

            if ((string)button1.Content == "Select")
            {
                button1.Content = "Unselect";
            }
            else
            {
                button1.Content = "Select";
            }
        }

        private void ButtonRow_Click(object sender, EventArgs e)
        {
            Button button2 = (Button)sender;
            for (int j = 0; j < this.column; j++)
            {
                if ((string)button2.Content == "Select")
                {
                    if (!(bool)this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked)
                    {
                        this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked = true;
                    }
                }
                else
                {
                    if ((bool)this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked)
                    {
                        this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked = false;
                    }
                }
            }

            if ((string)button2.Content == "Select")
            {
                button2.Content = "Unselect";
            }
            else
            {
                button2.Content = "Select";
            }
        }

        private void ButtonAll_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBlock block = (TextBlock)button.Content;
            string numbers = (string)button.Tag;
            int middle;
            middle = numbers.IndexOf(' ');
            int column = Convert.ToInt32(numbers.Substring(0, middle));
            int row = Convert.ToInt32(numbers.Substring(middle));
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (block.Text == "Select All")
                    {
                        if (!(bool)this.seatArray[i, j].chkSelected.IsChecked)
                        {
                            this.seatArray[i, j].chkSelected.IsChecked = true;
                        }
                    }
                    else
                    {
                        if ((bool)this.seatArray[i, j].chkSelected.IsChecked)
                        {
                            this.seatArray[i, j].chkSelected.IsChecked = false;
                        }
                    }
                }
            }

            if (block.Text == "Select All")
            {
                block.Text = "Unselect All";
            }
            else
            {
                block.Text = "Select All";
            }
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.RoomName = txtnm.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        private void TextBoxLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.Location = txtloc.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.Description = txtdes.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                this.seatsSelected.ElementAt(i).Chair.MustBeEmpty = (bool)chkboxEmpty.IsChecked;
                this.seatsSelected.ElementAt(i).Chair.NonChair = (bool)chkboxNotSeat.IsChecked;
                if ((bool)chkboxNotSeat.IsChecked || (bool)chkboxEmpty.IsChecked)
                {
                    if ((bool)chkboxNotSeat.IsChecked)
                    {
                        this.seatsSelected.ElementAt(i).Background = Brushes.Gray;
                    }
                    else
                    {
                        this.seatsSelected.ElementAt(i).Background = Brushes.LightGray;
                    }
                }
                else
                {
                    this.seatsSelected.ElementAt(i).Background = Brushes.White;
                }

                // seatsSelected.ElementAt(i).chkSelected.IsChecked = false;
            }

            // MessageBox.Show(seatsSelected.Count.ToString());
        }

        private void CheckBoxSelected_Checked(object sender, RoutedEventArgs e)
        {
            Seat seat = (Seat)sender;
            this.seatsSelected.Add(seat);
            if (this.seatsSelected.Count > 1)
            {
                txtnumber.IsEnabled = false;
                btnnumber.IsEnabled = false;
            }
        }

        private void CheckBoxSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            Seat seat = (Seat)sender;
            this.seatsSelected.Remove(seat);
            if (this.seatsSelected.Count < 2)
            {
                txtnumber.IsEnabled = true;
                btnnumber.IsEnabled = true;
            }
        }

        private void RowByColumn_click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.row; i++)
            {
                if (this.txtrow[i].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Rows doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            for (int j = 0; j < this.column; j++)
            {
                if (this.txtcol[j].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Columns doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                for (int i = 0; i < this.row; i++)
                {
                    for (int j = 0; j < this.column; j++)
                    {
                        this.seatArray[i, j].Chair.SeatName = this.txtrow[i].Text + this.txtcol[j].Text;
                        this.seatArray[i, j].lblName.Content = this.txtrow[i].Text + this.txtcol[j].Text;
                    }
                }
            }
        }

        private void ColumnByRow_click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.row; i++)
            {
                if (this.txtrow[i].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Rows doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            for (int j = 0; j < this.column; j++)
            {
                if (this.txtcol[j].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Columns doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                for (int i = 0; i < this.row; i++)
                {
                    for (int j = 0; j < this.column; j++)
                    {
                        this.seatArray[i, j].Chair.SeatName = this.txtcol[j].Text + this.txtrow[i].Text;
                        this.seatArray[i, j].lblName.Content = this.txtcol[j].Text + this.txtrow[i].Text;
                    }
                }
            }
        }

        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".tplt"; // Default file extension
            dlg.Filter = "Room File (.tplt)|*.tplt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Save the template
                this.myroom.WriteRoomTemplate(dlg.FileName);
            }
        }

        private void ButtonHanded_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                this.seatsSelected.ElementAt(i).Chair.LeftHanded = (bool)rbtnLeftH.IsChecked;
                if (this.seatsSelected.ElementAt(i).Chair.LeftHanded)
                {
                    this.seatsSelected.ElementAt(i).lblName.Foreground = Brushes.Red;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).lblName.Foreground = Brushes.Black;
                }
                
                // seatsSelected.ElementAt(i).chkSelected.IsChecked = false;
            }
        }

        private void ButtonHorizontal_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                if ((bool)rbtnLeft.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 0;
                }
                else if ((bool)rbtnRight.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 2;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 1;
                }

                this.seatsSelected.ElementAt(i).chkSelected.Margin = new Thickness(this.seatsSelected.ElementAt(i).Chair.LrPosition * 15, this.seatsSelected.ElementAt(i).Chair.FbPosition * 15, 0, 0);
                
                // seatsSelected.ElementAt(i).chkSelected.IsChecked = false;
            }
        }

        private void ButtonVertical_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                if ((bool)rbtnBack.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 2;
                }
                else if ((bool)rbtnFront.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 0;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 1;
                }

                this.seatsSelected.ElementAt(i).chkSelected.Margin = new Thickness(this.seatsSelected.ElementAt(i).Chair.LrPosition * 15, this.seatsSelected.ElementAt(i).Chair.FbPosition * 15, 0, 0);
                
                // seatsSelected.ElementAt(i).chkSelected.IsChecked = false;
            }
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (txtnumber.Text.Length > 0)
            {
                this.seatsSelected.ElementAt(0).Chair.SeatName = txtnumber.Text;
                this.seatsSelected.ElementAt(0).lblName.Content = txtnumber.Text;
                MessageBox.Show(this.seatsSelected.ElementAt(0).Chair.SeatName);
            }

            // seatsSelected.ElementAt(0).chkSelected.IsChecked = false;
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Student_Drop(object sender, RoutedEventArgs e)
        {
            if (this.students.SelectedItem != null)
            {
                Student student = (Student)this.students.SelectedItem;
                Seat seat = (Seat)sender;
                if (!this.myroom.IsStudentSeated(student))
                {
                    seat.Chair.TheStudent = student;
                }
                else
                {
                    MessageBox.Show(student.FirstName + " " + student.LastName + " is already seated");
                }

                this.students.UnselectAll();
            }
        }
    }
}
