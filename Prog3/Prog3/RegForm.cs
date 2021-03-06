﻿// Program 3
// CIS 199-01
// Due: 11/9/2017
// By: A4431

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2018 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 3";   // 1st day of registration
            const string DAY2 = "November 6";   // 2nd day of registration
            const string DAY3 = "November 7";   // 3rd day of registration
            const string DAY4 = "November 8";   // 4th day of registration
            const string DAY5 = "November 9";   // 5th day of registration
            const string DAY6 = "November 10";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            char[] lastNameChs = { 'A', 'E', 'J', 'P', 'Z' };//lower limits for senior and junior letters
            string[] times = { TIME2, TIME3, TIME4, TIME5, TIME1 };//parallel array for letter ranges
            int sub = lastNameChs.Length - 1;//goes through the array in reverse

            char[] lastNameChsSophAndFresh = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'Z' };//lower limits for sophomores and freshmen letters
            string[] timesSophAndFresh = { TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2 };//parallel array for letter ranges for registration times
            int subSophAndFresh = lastNameChsSophAndFresh.Length - 1;//goes through the array in reverse

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        while (sub >= 0 && Convert.ToInt32(lastNameLetterCh) < lastNameChs[sub])//goes through letter range for Seniors and Juniors
                            --sub;
                        timeStr = times[sub];
                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }
                        while (subSophAndFresh >= 0 && Convert.ToInt32(lastNameLetterCh) < lastNameChsSophAndFresh[subSophAndFresh])//foes through the letter range for Sophomores and Freshmen
                            --subSophAndFresh;
                        timeStr = timesSophAndFresh[subSophAndFresh];

                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
