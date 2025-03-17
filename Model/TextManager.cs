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
        // Храним ключевые слова для каждой категории
        private readonly Dictionary<string, List<string>> _keywords = new Dictionary<string, List<string>>()
        {
            { "Keywords", new List<string> { "public", "private", "class", "void", "new" } },
            { "TypesData", new List<string> { "int", "string", "bool", "float", "double" } },
            { "Operators", new List<string> { "if", "else", "for", "while", "return" } }
        };
        private Font _selectedFont = new Font("Verdana", 12F);
        private Font _selectedFontOutput = new Font("Verdana", 10F);
        private Color _selectedColor = Color.Black;
        // Определяем категории ключевых слов и их цвета
        private Dictionary<string, Color> _keywordCategories = new Dictionary<string, Color>()
        {
            { "Keywords", Color.Blue },    // Основные ключевые слова (синий)
            { "TypesData", Color.Green },   // Типы данных (зеленый)
            { "Operators", Color.Red }      // Операторы (красный)
        };

        public Dictionary<string, List<string>> Keywords { get => _keywords; }
        public Font SelectedFont { get => _selectedFont; set => _selectedFont = value; }
        public Font SelectedFontOutput { get => _selectedFontOutput; set => _selectedFontOutput = value; }
        public Color SelectedColor { get => _selectedColor; set => _selectedColor=value; }
        public Dictionary<string, Color> KeywordCategories { get => _keywordCategories; set => _keywordCategories = value; }
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
        public void SettingsColorFont(Color currentColor)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }
    }
}
