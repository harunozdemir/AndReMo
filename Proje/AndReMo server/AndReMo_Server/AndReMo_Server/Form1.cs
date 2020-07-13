using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace AndReMo_Server
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool keybd_event(int bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int VkKeyScan(char ch);

        private EndPoint point;
        Socket receiveSocket;

        private byte[] recBuffer;
        private int speed = 2;
        int say = 1;
        
        string durum = "mouse";
        string key;
        
        public const int VK_RMENU = 0xA5;
        public const int VK_LMENU = 0xA4;
        
        const int KEYEVENTF_KEYUP = 0x2;
        const int KEYEVENTF_EXTENDEDKEY = 0x1;


      
        IEnumerable<NetworkInterface> nics = NetworkInterface.GetAllNetworkInterfaces().Where(
     network => network.NetworkInterfaceType == NetworkInterfaceType.Wireless80211);
        
        // Flags for mouse_event api
        [Flags]
        public enum MouseEventFlagsAPI
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void SendClick()
        {
            // Send click to system
            mouse_event((int)MouseEventFlagsAPI.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlagsAPI.LEFTUP, 0, 0, 0, 0);
        }

        private void rigthClick()
        {

            mouse_event((int)MouseEventFlagsAPI.RIGHTDOWN, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlagsAPI.RIGHTUP, 0, 0, 0, 0);

        }
       
        public void printScreenCharacter(string karakter)
        {

            switch (karakter)
            {

                case "a": keybd_event(VkKeyScan('a'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "b": keybd_event(VkKeyScan('b'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "c": keybd_event(VkKeyScan('c'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ç": keybd_event(VkKeyScan('ç'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "d": keybd_event(VkKeyScan('d'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "e": keybd_event(VkKeyScan('e'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "f": keybd_event(VkKeyScan('f'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "g": keybd_event(VkKeyScan('g'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ğ": keybd_event(VkKeyScan('ğ'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "h": keybd_event(VkKeyScan('h'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ı": keybd_event(VkKeyScan('ı'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "i": keybd_event(0xde, 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "j": keybd_event(VkKeyScan('j'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "k": keybd_event(VkKeyScan('k'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "l": keybd_event(VkKeyScan('l'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "m": keybd_event(VkKeyScan('m'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "n": keybd_event(VkKeyScan('n'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "o": keybd_event(VkKeyScan('o'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ö": keybd_event(VkKeyScan('ö'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "p": keybd_event(VkKeyScan('p'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "r": keybd_event(VkKeyScan('r'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "s": keybd_event(VkKeyScan('s'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ş": keybd_event(VkKeyScan('ş'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "t": keybd_event(VkKeyScan('t'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "u": keybd_event(VkKeyScan('u'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "ü": keybd_event(VkKeyScan('ü'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "v": keybd_event(VkKeyScan('v'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "y": keybd_event(VkKeyScan('y'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "z": keybd_event(VkKeyScan('z'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "w": keybd_event(VkKeyScan('w'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "x": keybd_event(VkKeyScan('x'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "q": keybd_event(VkKeyScan('q'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;

                case "A": enableShift(); keybd_event(VkKeyScan('a'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "B": enableShift(); keybd_event(VkKeyScan('b'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "C": enableShift(); keybd_event(VkKeyScan('c'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Ç": enableShift(); keybd_event(VkKeyScan('ç'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "D": enableShift(); keybd_event(VkKeyScan('d'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "E": enableShift(); keybd_event(VkKeyScan('e'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "F": enableShift(); keybd_event(VkKeyScan('f'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "G": enableShift(); keybd_event(VkKeyScan('g'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Ğ": enableShift(); keybd_event(VkKeyScan('ğ'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "H": enableShift(); keybd_event(VkKeyScan('h'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "I": enableShift(); keybd_event(VkKeyScan('ı'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "İ": enableShift(); keybd_event(0xde, 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "J": enableShift(); keybd_event(VkKeyScan('j'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "K": enableShift(); keybd_event(VkKeyScan('k'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "L": enableShift(); keybd_event(VkKeyScan('l'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "M": enableShift(); keybd_event(VkKeyScan('m'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "N": enableShift(); keybd_event(VkKeyScan('n'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "O": enableShift(); keybd_event(VkKeyScan('o'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Ö": enableShift(); keybd_event(VkKeyScan('ö'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "P": enableShift(); keybd_event(VkKeyScan('p'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "R": enableShift(); keybd_event(VkKeyScan('r'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "S": enableShift(); keybd_event(VkKeyScan('s'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Ş": enableShift(); keybd_event(VkKeyScan('ş'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "T": enableShift(); keybd_event(VkKeyScan('t'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "U": enableShift(); keybd_event(VkKeyScan('u'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Ü": enableShift(); keybd_event(VkKeyScan('ü'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "V": enableShift(); keybd_event(VkKeyScan('v'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Y": enableShift(); keybd_event(VkKeyScan('y'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Z": enableShift(); keybd_event(VkKeyScan('z'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "W": enableShift(); keybd_event(VkKeyScan('w'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "X": enableShift(); keybd_event(VkKeyScan('x'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "Q": enableShift(); keybd_event(VkKeyScan('q'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case " ": keybd_event(VkKeyScan(' '), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;

                case "0": keybd_event(VkKeyScan('0'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "1": keybd_event(VkKeyScan('1'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "2": keybd_event(VkKeyScan('2'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "3": keybd_event(VkKeyScan('3'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "4": keybd_event(VkKeyScan('4'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "5": keybd_event(VkKeyScan('5'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "6": keybd_event(VkKeyScan('6'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "7": keybd_event(VkKeyScan('7'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "8": keybd_event(VkKeyScan('8'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "9": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;

                case "!": enableShift(); keybd_event(VkKeyScan('1'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "@": enablealtgr(); keybd_event(VkKeyScan('q'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "#": enablealtgr(); keybd_event(VkKeyScan('3'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "$": enablealtgr(); keybd_event(VkKeyScan('4'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "/": enableShift(); keybd_event(VkKeyScan('7'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "(": enableShift(); keybd_event(VkKeyScan('8'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case ")": enableShift(); keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "&": enableShift(); keybd_event(VkKeyScan('6'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "?": enableShift(); keybd_event(VkKeyScan('?'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case ":": enableShift(); keybd_event(VkKeyScan('.'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case ";": enableShift(); keybd_event(VkKeyScan(','), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case ",": keybd_event(VkKeyScan(','), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ".": keybd_event(VkKeyScan('.'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "*": keybd_event(VkKeyScan('*'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "-": keybd_event(VkKeyScan('-'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "^": enableShift(); for (int i = 0; i < 2; i++) keybd_event(VkKeyScan('3'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;

                case "+": enableShift(); keybd_event(VkKeyScan('4'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "×": keybd_event(VkKeyScan('×'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "÷": keybd_event(0x00FF, 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "=": enableShift(); keybd_event(VkKeyScan('0'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "<": enablealtgr(); keybd_event(VkKeyScan('<'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case ">": enablealtgr(); keybd_event(VkKeyScan('>'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "{": enablealtgr(); keybd_event(VkKeyScan('7'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "}": enablealtgr(); keybd_event(VkKeyScan('0'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "[": enablealtgr(); keybd_event(VkKeyScan('8'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "]": enablealtgr(); keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "€": enablealtgr(); keybd_event(VkKeyScan('e'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "£": enablealtgr(); keybd_event(VkKeyScan('2'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;

                case "%": enableShift(); keybd_event(VkKeyScan('5'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "~": enablealtgr(); keybd_event(VkKeyScan('~'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "`": enablealtgr(); keybd_event(VkKeyScan('`'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "backspace": keybd_event(0x08, 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "enter": keybd_event(0x0D, 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "tektirnak": enableShift(); keybd_event(VkKeyScan('2'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "cifttirnak": keybd_event(VkKeyScan('é'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;

                case "¥": keybd_event(VkKeyScan('¥'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "₩": keybd_event(VkKeyScan('₩'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "_": enableShift(); keybd_event(VkKeyScan('_'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disableShift(); break;
                case "\\": enablealtgr(); keybd_event(VkKeyScan('?'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "|": enablealtgr(); keybd_event(VkKeyScan('|'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); disablealtgr(); break;
                case "《": keybd_event(VkKeyScan('《'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "》": keybd_event(VkKeyScan('》'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "¡": keybd_event(VkKeyScan('¡'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "¿": keybd_event(VkKeyScan('¿'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "¤": keybd_event(VkKeyScan('¤'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "♡": keybd_event(VkKeyScan('♡'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "♥": keybd_event(VkKeyScan('♥'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "°": keybd_event(VkKeyScan('°'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "•": keybd_event(VkKeyScan('•'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "○": keybd_event(VkKeyScan('○'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "●": keybd_event(VkKeyScan('●'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "□": keybd_event(VkKeyScan('□'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "■": keybd_event(VkKeyScan('■'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "◇": keybd_event(VkKeyScan('◇'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "◆": keybd_event(VkKeyScan('◆'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "♧": keybd_event(VkKeyScan('♧'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "♣": keybd_event(VkKeyScan('♣'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "▲": keybd_event(VkKeyScan('▲'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "▼": keybd_event(VkKeyScan('▼'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "▶": keybd_event(VkKeyScan('▶'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "◀": keybd_event(VkKeyScan('◀'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "↑": keybd_event(VkKeyScan('↑'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "↓": keybd_event(VkKeyScan('↓'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "←": keybd_event(VkKeyScan('←'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "→": keybd_event(VkKeyScan('→'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "☆": keybd_event(VkKeyScan('☆'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "★": keybd_event(VkKeyScan('★'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case "▪": keybd_event(VkKeyScan('▪'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ":-)": keybd_event(VkKeyScan(' '), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ";-)": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ":-D": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ":-(": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ":'(": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;
                case ":O": keybd_event(VkKeyScan('9'), 0x45, KEYEVENTF_EXTENDEDKEY, 0); break;


            }

        }

        public void enableShift()
        {

            keybd_event(0x10, 0x45, KEYEVENTF_EXTENDEDKEY, 0); // shift

        }

        public void disableShift()
        {
            keybd_event(0x10, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(0x10, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        public void enablealtgr()
        {

            keybd_event(VK_RMENU, 0x45, KEYEVENTF_EXTENDEDKEY, 0);

        }

        public void disablealtgr()
        {

            keybd_event(VK_RMENU, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(VK_RMENU, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);

        }

        private void MessageReceiveCallback(IAsyncResult result)
        {
            EndPoint r = new IPEndPoint(0, 0);
            string pos = "";

            if (say == 1)
            {
                try
                {

                    pos = Encoding.UTF8.GetString(recBuffer);

                    if (pos.StartsWith("click")) this.SendClick();

                    else if (pos.StartsWith("r.click")) this.rigthClick();

                    else if (pos.StartsWith("keyboard")) durum = "keyboard";

                    else if (pos.StartsWith("mouse")) durum = "mouse";

                    else if (pos.StartsWith("connect"))  cagir();
                    
                    // Otherwise move
                    else
                    {
                        // keyboard event
                        if (durum == "keyboard")
                        {

                            key = pos.Substring(0, pos.IndexOf("son"));
                            printScreenCharacter(key);

                        }

                        if (durum == "mouse") // mouse event   
                        {
                                                        
                             System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                             System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                             int deltaX = (int)float.Parse(pos.Substring(0, pos.IndexOf(","))) * this.speed;
                             int deltaY = (int)float.Parse(pos.Substring(pos.IndexOf(",") + 1, pos.IndexOf("\0")+1 - pos.IndexOf(",") + 1)) * this.speed;
                             
                           
                            // set new point
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(pt.X + deltaX, pt.Y + deltaY);
                            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                        }

                    }
                }
                catch (Exception)
                {
                    Console.Write(pos);
                }
                // End and "begin" for next package
                this.receiveSocket.EndReceiveFrom(result, ref r);
                receiveSocket.BeginReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref point, new AsyncCallback(MessageReceiveCallback), (object)this);

            }

        }

        public void cagir()
        {

            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AndReMo";
            notifyIcon1.BalloonTipTitle = "AndReMo";
            notifyIcon1.BalloonTipText = "Connected client...";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(8000);
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            this.WindowState = FormWindowState.Minimized;
            this.Activate();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            say = 1;
            receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            point = new IPEndPoint(IPAddress.Any, 8000);
            this.recBuffer = new byte[60];
            receiveSocket.Bind(point);
            receiveSocket.BeginReceiveFrom(recBuffer, 0, recBuffer.Length,
                SocketFlags.None, ref point,
                new AsyncCallback(MessageReceiveCallback), (object)this);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblServerState.Text = "Started";
           
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            say = 0;
            receiveSocket.Dispose();
            btnStop.Enabled = false;
            lblServerState.Text = "Stopped";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            btnStop.Enabled = false;

              foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) || (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)) //&& (nic.OperationalStatus == OperationalStatus.Up))
                {
                    comboBoxLanInternet.Items.Add(nic.Description);
                }
            }

  
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.WindowState = FormWindowState.Normal;

            this.Activate();
        }

        private void comboBoxLanInternet_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                {
                    if (nic.Description == comboBoxLanInternet.SelectedItem.ToString())
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            lblIp.Text = ip.Address.ToString();
                        }
                    }
                }
            }




        }

     

      
    }
}
