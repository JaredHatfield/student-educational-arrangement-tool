﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEATLibrary;

namespace TestingApplication
{
    class CLInterface
    {

        public CLInterface()
        {
            // Nothing needs to be set up for this function to work.
        }

        public Student getNewStudent()
        {
            return new Student(promptForString("First Name"), promptForString("Last Name"),
                promptForString("Section"), promptForBoolean("Left Handed"), 
                promptForBoolean("Vision Enpairment"));
        }

        public Room getNewRoom()
        {
            return new Room(promptForString("Room Name"), promptForString("Room Location"),
                promptForString("Room Description"), promptForInt("Room Width"), promptForInt("Room Height"));
        }

        public void displayStudentRoster(List<Student> list)
        {
            Console.WriteLine("Displaying Student Roster");
            if (list.Count == 0)
            {
                Console.WriteLine("There are currently no students.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i].ToString());
                }
            }
        }

        public void displayRoomList(List<Room> roomList)
        {
            Console.WriteLine("List of all room names");
            if (roomList.Count == 0)
            {
                Console.WriteLine("There are currently no rooms.");
            }
            else
            {
                for (int i = 0; i < roomList.Count; i++)
                {
                    Console.WriteLine(i + ") " + roomList[i].RoomName);
                }
            }
        }

        public int selectRoomList(List<Room> roomList)
        {
            Console.WriteLine("List of all room names");
            if (roomList.Count == 0)
            {
                Console.WriteLine("There are currently no rooms.");
                return -1;
            }
            else
            {
                Console.WriteLine("Choose room from list: ");
                for (int i = 0; i < roomList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") " + roomList[i].RoomName);
                }
                int n = promptForInt("Selection");
                n--;
                if (n > roomList.Count || n < 0)
                {
                    return -1;
                }
                return n;
            }
        }

        public void updateRoomSeat(Room room)
        {
            // Have the user select one of the seats in the room
            Console.Clear();
            this.displayRoom(room);
            Console.WriteLine();
            int x = 0, y = 0;
            while (x < 1 || x > room.Width)
            {
                x = this.promptForInt("Width");
            }
            x--;
            while (y < 1 || x > room.Height)
            {
                y = this.promptForInt("Height");
            }
            y--;
            Chair c = room.Chairs[x, y];

            // Tell the user about the current seat
            int input = -1;
            while (input != 0)
            {
                Console.Clear();
                Console.Write("Enter 0 to not update any values.");
                Console.WriteLine("1) Left Handed: " + c.LeftHanded);
                Console.WriteLine("2) Non Chair: " + c.NonChair);
                Console.WriteLine("3) Must Be Empty: " + c.MustBeEmpty);
                Console.WriteLine("4) Seat Number: " + c.SeatNumber);
            }

            waitForUserEnter();
        }

        public void displayRoom(Room r)
        {
            Console.Write("\t");
            for (int i = 0; i < r.Width; i++)
            {
                Console.Write((i + 1) + "\t");
            }
            Console.WriteLine();
            for (int i = 0; i < r.Height; i++)
            {
                Console.Write((i + 1) + "\t");
                for (int j = 0; j < r.Width; j++)
                {
                    Console.Write(r.Chairs[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public void waitForUserEnter()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private String promptForString(String prompt)
        {
            Console.Write(prompt + ": ");
            String response = Console.ReadLine();
            return response;
        }

        private int promptForInt(String prompt)
        {
            String response = promptForString(prompt);
            return Int32.Parse(response);
        }

        private Boolean promptForBoolean(String prompt)
        {
            String response = promptForString(prompt);
            if (response.Equals("Yes") || response.Equals("yes") || response.Equals("YES"))
            {
                return true;
            }
            else if (response.Equals("Y") || response.Equals("y"))
            {
                return true;
            }
            else if (response.Equals("True") || response.Equals("true") || response.Equals("TRUE"))
            {
                return true;
            }
            else if (response.Equals("T") || response.Equals("t"))
            {
                return true;
            }
            if (response.Equals("No") || response.Equals("no") || response.Equals("NO"))
            {
                return true;
            }
            else if (response.Equals("N") || response.Equals("n"))
            {
                return true;
            }
            else if (response.Equals("False") || response.Equals("false") || response.Equals("FALSE"))
            {
                return true;
            }
            else if (response.Equals("F") || response.Equals("f"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
