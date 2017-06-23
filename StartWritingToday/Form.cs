#region Usings
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#endregion

namespace PiszemySlowa
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // todo: dokument howto
            // todo: zmienic katalog windowsforms1
            InitializeComponent();
            InitializeFilesSetup();
        }

        #region Properties

        /// <summary>
        /// Written text in text box.
        /// </summary>
        private string WrittenText
        {
            get
            {
                return Regex.Replace(this.txtBox.Text, @"\r\n?|\n", string.Empty);
            }
        }

        /// <summary>
        /// Struct for files names data.
        /// </summary>
        internal struct FileData
        {
            internal string Name;
            internal string Extension;
            internal string Path;

            public FileData(string name, string ext, string path)
            {
                this.Name = name;
                this.Extension = ext;
                this.Path = path;
            }
        }

        /// <summary>
        /// File list from image folder.
        /// </summary>
        private IEnumerable<FileData> FilesList { get; set; }

        private string CurrentDirectory { get; } = Path.Combine(Directory.GetCurrentDirectory(), "images");

        /// <summary>
        /// Supported image extensions.
        /// The list is sorted cause we have priorities here.
        /// </summary>
        Dictionary<string, int> supportedExtensions = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) {
                    {".gif", 0},
                    {".png", 1},
                    {".jpg", 2},
                    {".jpeg", 3}
        };

        #endregion Properties

        /// <summary>
        /// Load file list with validation.
        /// </summary>
        private void InitializeFilesSetup()
        {
            IEnumerable<FileData> allFiles = null;

            // Enumerate - do not retrieve all the files and then continue working
            try
            {
                allFiles = Directory.EnumerateFiles(CurrentDirectory)
                              .Select(file => new FileData(Path.GetFileNameWithoutExtension(file), Path.GetExtension(file), Path.GetFullPath(file)));
            }
            catch
            {
                MessageBox.Show("There is an error with image folder and the application will close. Please fix the problem with folder and try again.");
                Environment.Exit(1);
            }

            this.FilesList = allFiles
                            .Where(file => supportedExtensions.ContainsKey(file.Extension)) // only supported extensions here!
                            .GroupBy(file => file.Name)
                            .Select(group => group.OrderBy(o => supportedExtensions[o.Extension]).First());// get only one extension in each name group by priorities as in sorted list

            #region Validation (message)

            List<string> validationFails = new List<string>();

            // Validation 1: there are no files loaded
            if (!this.FilesList.Any())
            {
                validationFails.Add("There are no proper files in the images folder. ");
            }

            // Validation 2: ommited files warning
            var ommitedFiles = allFiles.Except(this.FilesList);

            if (ommitedFiles.Any())
            {
                validationFails.Add("There are several files with unsupported extensions: ");
                validationFails.AddRange(ommitedFiles.Select(x => x.Name + x.Extension + " ").ToList());
            }

            if (validationFails.Any())
            {
                MessageBox.Show(string.Join("\n", validationFails));
            }

            #endregion Validation (message)
        }

        /// <summary>
        /// Text Change event.
        /// - Color the vowels.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void TxtBox_TextChanged(object sender, EventArgs e)
        {
            // we will change some stuff here
            txtBox.TextChanged -= TxtBox_TextChanged;

            ColorVowels();

            // changing stuff ended
            txtBox.TextChanged += TxtBox_TextChanged;
        }

        /// <summary>
        /// Color the vowels in red.
        /// </summary>
        private void ColorVowels()
        {
            string tmp = txtBox.Rtf;

            // RTF way, based on https://www.codeproject.com/Articles/15038/C-Formatting-Text-in-a-RichTextBox-by-Parsing-the   
            // 'SELECTION' way has problem with cursor place and selection blinks
            Regex regExp = new Regex("a|e|i|o|u|y|ą|ę|ó|A|E|I|O|U|Y|Ą|Ę|Ó");
            string plainTextToRTF = txtBox.Text;
            int cursorPosition = txtBox.SelectionStart;

            int i = 0;
            int plaintTextlenght = plainTextToRTF.Length;

            // entering vovel, backspace and then consonant - cf1 stays in rtf and the consonant is in red
            // that's why put should put "black" at the beginning:
            plainTextToRTF = plainTextToRTF.Insert(0, "\\cf0 ");

            foreach (Match match in regExp.Matches(plainTextToRTF))
            {
                plainTextToRTF = plainTextToRTF.Insert(match.Index + i + 1, "\\cf0 ");
                plainTextToRTF = plainTextToRTF.Insert(match.Index + i, "\\cf1 ");
                i += 10;
            }

            txtBox.Rtf = "{\\rtf1\\ansi\\lang1045\\ansicpg1250\\deff0\\deflang1045 {\\colortbl ;\\red255\\green0\\blue0;}" + plainTextToRTF + "}";
            txtBox.SelectionStart = cursorPosition;
        }

        /// <summary>
        /// Check text and match the image in the image folder.
        /// First take gif, then png, then jpg.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void BtnSprawdz_Click(object sender, EventArgs e)
        {
            var searchedElement = this.FilesList.Where(w => w.Name == this.WrittenText).FirstOrDefault();

            if (!string.IsNullOrEmpty(searchedElement.Path))
            {
                this.ShowImage(searchedElement.Path);
                return;
            }

            pictureBox.Image = null;
        }

        /// <summary>
        /// File display.
        /// </summary>
        /// <param name="fileToDisplay">Path of the file.</param>
        private void ShowImage(string fileToDisplay)
        {
            try
            {
                Image imageToDisplay = new Bitmap(fileToDisplay);
                pictureBox.Image = imageToDisplay;
            }
            catch
            {
                DialogResult result = MessageBox.Show("The crocodile has eaten this image!", "ups", MessageBoxButtons.RetryCancel);

                if (result == System.Windows.Forms.DialogResult.Retry)
                {
                    InitializeFilesSetup();
                    this.ShowImage(fileToDisplay);
                }
            }
        }
    }
}
