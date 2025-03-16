using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    public class TextManager
    {
        // Ключевые слова для подсветки
        string[] _keywordsTypes = { "int", "bool", "char", "double", "float", "void", "class" };
        string[] _keywordsOperators = { "if", "else", "for","switch", "case", "while", "return" };
        string[] _keywordsEnum = { "enum" };
        private Font _selectedFont = new Font("Verdana", 12F);
        private Font _selectedFontOutput = new Font("Verdana", 10F);
        private Color _selectedColor = Color.Black;
        private Color _colorTypes = Color.Blue;
        private Color _colorOperators = Color.DeepPink;
        private Color _colorEnum = Color.Green;

        public string[] KeywordsTypes { get => _keywordsTypes; set => _keywordsTypes = value; }
        public string[] KeywordsOperators { get => _keywordsOperators; set => _keywordsOperators = value; }
        public string[] KeywordsEnum { get => _keywordsEnum; set => _keywordsEnum = value; }
        public Font SelectedFont { get => _selectedFont; set => _selectedFont = value; }
        public Font SelectedFontOutput { get => _selectedFontOutput; set => _selectedFontOutput = value; }
        public Color SelectedColor { get => _selectedColor; set => _selectedColor=value; }
        public Color ColorTypes { get => _colorTypes; set => _colorTypes = value; }
        public Color ColorOperators { get => _colorOperators; set => _colorOperators = value; }
        public Color ColorEnum { get => _colorEnum; set => _colorEnum = value; }

        public TextManager(){}

        // Выбор шрифта
        public void SettingsFont()
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = SelectedFont;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFont = fontDialog.Font;
            }
        }
        public void SettingsFontOutput()
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = SelectedFontOutput;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFontOutput = fontDialog.Font;
            }
        }
        // Выбор цвета
        public Color SettingsColorFont(Color currentColor)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
            return currentColor;
        }
    }
}
